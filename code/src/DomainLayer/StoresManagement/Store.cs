using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceSystem.DomainLayer.StoresManagement.Discount;
using ECommerceSystem.DomainLayer.StoresManagement.PurchasePolicies;
using ECommerceSystem.DomainLayer.UserManagement;
using ECommerceSystem.Utilities;
using ECommerceSystem.Models;
using ECommerceSystem.Models.PurchasePolicyModels;
using ECommerceSystem.Models.DiscountPolicyModels;

namespace ECommerceSystem.DomainLayer.StoresManagement
{
    public class Store : IStoreInterface
    {

        private readonly Range<double> RATING_RANGE = new Range<double>(0.0, 5.0);


        private string _name;
        private double _rating;
        private long _raterCount;
        private OrDiscountPolicy _discountPolicyTree; //all store complex discounts
        private AndPurchasePolicy _purchasePolicy; // all store purchase policies
        private Dictionary<string, Permissions> _premmisions; // username => permissions
        private Inventory _inventory;
        private XORDiscountPolicy _storeLevelDiscounts;
        private List<StorePurchaseModel> _purchaseHistory;
        private Dictionary<Guid, DiscountPolicy> _allDiscountsMap;
        private Dictionary<Guid, DiscountType> _NotInTreeProductDiscounts;

        public Store(string ownerUserName, string name)
        {
            this._discountPolicyTree = new OrDiscountPolicy(Guid.NewGuid());
            this._purchasePolicy = new AndPurchasePolicy(Guid.NewGuid());
            this._premmisions = new Dictionary<string, Permissions>();
            this._inventory = new Inventory();
            this.Name = name;
            this._purchaseHistory = new List<StorePurchaseModel>();
            this._storeLevelDiscounts = new XORDiscountPolicy(Guid.NewGuid());
            this._allDiscountsMap = new Dictionary<Guid, DiscountPolicy>();
            this._NotInTreeProductDiscounts = new Dictionary<Guid, DiscountType>();
        }

        public double getTotalPrice(Dictionary<Product, int> productQuantities)
        {
            double totalPrice = 0;
            Dictionary<Guid, (double basePrice, int quantity, double totalPrice)> products = new Dictionary<Guid, (double, int, double)>(); //productID => basePrice, quantity, total price per product

            //make the data structure Dictionary<Guid, (double basePrice, int quantity, double totalPrice)> for calc the complexive discounts
            foreach (KeyValuePair<Product, int> pair in productQuantities)
            {
                double basePrice = pair.Key.BasePrice;
                int quantity = pair.Value;
                double totalPriceProd = basePrice * quantity;
                products.Add(pair.Key.Id, (basePrice, quantity, totalPriceProd));
            }

            //calc the complexive discounts on the tree:
            _discountPolicyTree.calculateTotalPrice(products);

            //calc the simple discounts:
            foreach(var discount in _NotInTreeProductDiscounts)
            {
                discount.Value.calculateTotalPrice(products); 
            }

            //calc store discounts
            this._storeLevelDiscounts.calculateTotalPrice(products);


            //sum all prices after discounts
            foreach (var prod in products)
            {
                totalPrice += prod.Value.totalPrice;
            }
            return totalPrice;
        }

        public string Name { get => _name; set => _name = value; }
        public double Rating { get => _rating; }
        public Inventory Inventory { get => _inventory; private set => _inventory = value; }
        public List<StorePurchaseModel> PurchaseHistory { get => _purchaseHistory; set => _purchaseHistory=value; }
        public Dictionary<string, Permissions> Premmisions { get => _premmisions; }
        public long RaterCount { get => _raterCount; set => _raterCount = value; }
        public AndPurchasePolicy PurchasePolicy { get => _purchasePolicy; set => _purchasePolicy = value; }
        public OrDiscountPolicy DiscountPolicyTree { get => _discountPolicyTree; set => _discountPolicyTree = value; }
        public XORDiscountPolicy StoreLevelDiscounts { get => _storeLevelDiscounts; set => _storeLevelDiscounts = value; }
        public Dictionary<Guid, DiscountPolicy> AllDiscountsMap { get => _allDiscountsMap; set => _allDiscountsMap = value; }
        public Dictionary<Guid, DiscountType> NotInTreeProductDiscounts { get => _NotInTreeProductDiscounts; set => _NotInTreeProductDiscounts = value; }

        public void addOwner(string userName, Permissions permissions)
        {
            _premmisions.Add(userName, permissions);
        }

        //*********Add, Delete, Modify Products*********


        //@pre - logged in user have permission to add product
        //return product(not product inventory!) id, return -1 in case of fail
        public Guid addProductInv(string activeUserName, string productName, string description,  double price,
            int quantity, Category category, List<string> keywords, int minQuantity, int maxQuantity)
        {

            Guid productID = _inventory.addProductInv(productName, description,  price, quantity, category, keywords);

            if (minQuantity != -1 && maxQuantity != -1)
            {
                ProductQuantityPolicy productPurchasePolicy = new ProductQuantityPolicy(minQuantity, maxQuantity, productID, Guid.NewGuid());
                this._purchasePolicy.Add(productPurchasePolicy);
                this.Inventory.Products.First().ProductList.First().PurchasePolicy = productPurchasePolicy;
            }
            return productID;
        }


        //@pre - logged in user have permission to modify product
        //return the new product id or -1 in case of fail
        public Guid addProduct(string loggedInUserName, string productInvName, int quantity, int minQuantity, int maxQuantity)
        {
            Guid productID = _inventory.addProduct(productInvName, quantity);
            if(minQuantity != -1 && maxQuantity != -1)
            {
                ProductQuantityPolicy productPurchasePolicy = new ProductQuantityPolicy(minQuantity, maxQuantity, productID, Guid.NewGuid());
                this._purchasePolicy.Add(productPurchasePolicy);
                this.Inventory.getProductById(productID).PurchasePolicy = productPurchasePolicy;
            }

            return productID;
        }


        public bool deleteProductInventory(string loggedInUserName, string productInvName)
        {

            //remove all policies of the products of the productInv
            ProductInventory productInv = _inventory.getProductByName(productInvName);
            foreach(Product p in productInv.ProductList)
            {
                if(p.PurchasePolicy != null)
                     _purchasePolicy.Remove(p.PurchasePolicy.ID);
            }

            //remove all discounts of the products of the productInv
            foreach (Product p in productInv.ProductList)
            {
                if(p.Discount != null)
                {
                    if (_NotInTreeProductDiscounts.ContainsKey(p.Discount.getID()))
                    {
                        _NotInTreeProductDiscounts.Remove(p.Discount.getID());
                    }
                    else
                    {
                        _discountPolicyTree.Remove(p.PurchasePolicy.ID);
                    }
                    _allDiscountsMap.Remove(p.PurchasePolicy.ID);
                }
            }

            return _inventory.deleteProductInventory(productInvName);
        }

        public bool deleteProduct(string loggedInUserName, string productInvName, Guid productID)
        {
            Product product = Inventory.getProductById(productID);
            if (product == null)
            {
                return false;
            }

            bool success = _inventory.deleteProduct(productInvName, productID);

            if (success)
            {
                if (product.PurchasePolicy != null)
                    _purchasePolicy.Remove(product.PurchasePolicy.getID());
                if (product.Discount != null)
                {
                    if (_NotInTreeProductDiscounts.ContainsKey(product.Discount.getID()))
                    {
                        _NotInTreeProductDiscounts.Remove(product.Discount.getID());
                    }
                    else
                    {
                        _discountPolicyTree.Remove(product.PurchasePolicy.ID);
                    }
                    _allDiscountsMap.Remove(product.PurchasePolicy.ID);
                }
                return true;
            }
            else return false;
            
        }


        //*********Modify Products*********


        //@pre - logged in user have permission to  product
        public bool modifyProductPrice(string loggedInUserName, string productName, int newPrice)
        {
            return _inventory.modifyProductPrice(productName, newPrice);
        }

        //@pre - logged in user have permission to modify product
        //public bool modifyProductDiscountType(string loggedInUserName, string productInvName, Guid productID, DiscountType newDiscount)
        //{
        //    return _inventory.modifyProductDiscountType(productInvName, productID, newDiscount);
        //}

        //@pre - logged in user have permission to modify product
        //public bool modifyProductPurchaseType(string loggedInUserName, string productInvName, Guid productID, PurchaseType purchaseType)
        //{
        //    return _inventory.modifyProductPurchaseType(productInvName, productID, purchaseType);
        //}

        //@pre - logged in user have permission to modify product
        public bool modifyProductQuantity(string loggedInUserName, string productName, Guid productID, int newQuantity)
        {
            return _inventory.modifyProductQuantity(productName, productID, newQuantity);
        }

        //@pre - logged in user have permission to modify product
        public bool modifyProductName(string loggedInUserName, string newProductName, string oldProductName)
        {
            return _inventory.modifyProductName(newProductName, oldProductName);
        }

        //*********Assign*********

        public Permissions assignOwner(User loggedInUser, string newOwneruserName)
        {
            Permissions per = null;
            if (_premmisions.ContainsKey(newOwneruserName) && _premmisions[newOwneruserName].isOwner() ) // The user of userName is already owner of this store
            {
                return null;
            }

            if (_premmisions.ContainsKey(newOwneruserName))
            {
                _premmisions[newOwneruserName].makeOwner();
                
            }
            else
            {
                per = Permissions.CreateOwner(loggedInUser, this);
                if (per == null) return null;
                _premmisions.Add(newOwneruserName, per);
            }
            return per;
        }

        public Permissions assignManager(User loggedInUser, string newManageruserName)
        {

            if (_premmisions.ContainsKey(newManageruserName)) // The user of userName is already owner/manager of this store
            {
                return null;
            }

            Permissions per = Permissions.CreateManager(loggedInUser, this);
            if (per == null) return null; 
            _premmisions.Add(newManageruserName, per);

            return per;
        }

        public bool removeManager(User loggedInUser, string managerUserName)
        {

            if (!_premmisions.ContainsKey(managerUserName) || _premmisions[managerUserName].isOwner()) // The user of userName isn`t manager of this store or he is owner of this store
            {
                return false;
            }

            if (!_premmisions[managerUserName].AssignedBy.Name().Equals(loggedInUser.Name())) //check that the logged in the user who assigned userName
            {
                return false;
            }

            _premmisions.Remove(managerUserName);
            
            return true;
        }


        // @Pre - loggedInUserName is the user who assign managerUserName
        //        managerUserName is manager in this store
        public bool editPermissions(string managerUserName, List<PermissionType> permissions, string loggedInUserName)
        {
            if(!_premmisions.ContainsKey(managerUserName) || _premmisions[managerUserName].isOwner()) //The managerUserName isn`t manager
            {
                return false;
            }

            if (!_premmisions[managerUserName].AssignedBy.Name().Equals(loggedInUserName)) // The loggedInUserName isn`t the owner who assign managerUserName
            {
                return false;
            }

            _premmisions[managerUserName].edit(permissions);
            return true;
        }

        public Tuple<Store, List<Product>> getStoreInfo()
        {
            var prods = _inventory.SelectMany(p => p).ToList();
            return new Tuple<Store, List<Product>>(this, prods);
        }

        public void rateStore(double rating)
        {
            ++_raterCount;
            rating = RATING_RANGE.inRange(rating) ? rating :
                     rating < RATING_RANGE.min ? RATING_RANGE.min : RATING_RANGE.max;
            _rating = ((_rating * (_raterCount - 1)) + rating) / _raterCount;
        }

        public void logPurchase(StorePurchaseModel purchase)
        {
            _purchaseHistory.Add(purchase);
        }

        public Permissions getPermissionByName(string userName)
        {
            if (_premmisions.ContainsKey(userName))
            {
                return _premmisions[userName];
            }
            else return null;
        }

        public List<StorePurchaseModel> purchaseHistory()
        {
            return this.PurchaseHistory;        
        }

        public Permissions getUsernamePermissions(string name)
        {
            return _premmisions.ContainsKey(name) ? _premmisions[name] : null;
        }

        public IDictionary<PermissionType, bool> getUsernamePermissionTypes(string name)
        {
            return _premmisions.ContainsKey(name) ? _premmisions[name].PermissionTypes : new Dictionary<PermissionType, bool>();
        }

        public bool canBuy(IDictionary<Guid, int> products, double totalPrice, string address)
        {
            return this._purchasePolicy.canBuy(products, totalPrice, address);
        }


        //*********Manage Purchase Policy  --   REQUIREMENT 4.2*********

        public Guid addDayOffPolicy(List<DayOfWeek> daysOff)
        {
            Guid newID = Guid.NewGuid();
            this._purchasePolicy.Add(new DaysOffPolicy(daysOff, newID));
            return newID;
        }

        public Guid addLocationPolicy(List<string> banLocations)
        {
            Guid newID = Guid.NewGuid();
            this._purchasePolicy.Add(new LocationPolicy(banLocations, newID));
            return newID;
        }

        public Guid addMinPriceStorePolicy(double minPrice)
        {
            Guid newID = Guid.NewGuid();
            this._purchasePolicy.Add(new MinPricePerStorePolicy(minPrice, newID));
            return newID;
        }

        public Guid addAndPurchasePolicy(Guid ID1, Guid ID2)
        {
            Guid newID = Guid.NewGuid();
            PurchasePolicy purchasePolicy1 = _purchasePolicy.getByID(ID1);
            PurchasePolicy purchasePolicy2 = _purchasePolicy.getByID(ID2);

            if (purchasePolicy1 == null || purchasePolicy2 == null)
            {
                return Guid.Empty;
            }

            removeFromFirstLevelPurchasePolicyTree(ID1, ID2);

            //add the new AND policy to the first level of the tree
            List<PurchasePolicy> newAndchildren = new List<PurchasePolicy> { purchasePolicy1, purchasePolicy2 };
            this._purchasePolicy.Add(new AndPurchasePolicy(newAndchildren, newID));
            return newID;
        }

        public Guid addOrPurchasePolicy(Guid ID1, Guid ID2)
        {
            Guid newID = Guid.NewGuid();
            PurchasePolicy purchasePolicy1 = _purchasePolicy.getByID(ID1);
            PurchasePolicy purchasePolicy2 = _purchasePolicy.getByID(ID2);

            if (purchasePolicy1 == null || purchasePolicy2 == null)
            {
                return Guid.Empty;
            }

            removeFromFirstLevelPurchasePolicyTree(ID1, ID2);

            //add the new OR policy to the first level of the tree
            List<PurchasePolicy> newOrchildren = new List<PurchasePolicy> { purchasePolicy1, purchasePolicy2 };
            this._purchasePolicy.Add(new OrPurchasePolicy(newOrchildren, newID));
            return newID;
        }

        public Guid addXorPurchasePolicy(Guid ID1, Guid ID2)
        {
            Guid newID = Guid.NewGuid();
            PurchasePolicy purchasePolicy1 = _purchasePolicy.getByID(ID1);
            PurchasePolicy purchasePolicy2 = _purchasePolicy.getByID(ID2);

            if (purchasePolicy1 == null || purchasePolicy2 == null)
            {
                return Guid.Empty;
            }

            removeFromFirstLevelPurchasePolicyTree(ID1, ID2);

            //add the new OR policy to the first level of the tree
            List<PurchasePolicy> newXorchildren = new List<PurchasePolicy> { purchasePolicy1, purchasePolicy2 };
            this._purchasePolicy.Add(new XORPurchasePolicy(newXorchildren, newID));
            return newID;
        }

        private void removeFromFirstLevelPurchasePolicyTree(Guid ID1, Guid ID2)
        {
            List<PurchasePolicy> newChildren = new List<PurchasePolicy>();
            //remove id1, id2 from first level of policy tree
            foreach (PurchasePolicy p in _purchasePolicy.Children)
            {
                if (!p.getID().Equals(ID1) && !p.getID().Equals(ID2))
                {
                    newChildren.Add(p);
                }
            }
            _purchasePolicy.Children = newChildren;
        }


        public void removePurchasePolicy(Guid policyID)
        {
            PurchasePolicy toRemove = _purchasePolicy.getByID(policyID);
            if(toRemove == null)
            {
                return;
            }
            if(toRemove is CompositePurchasePolicy)
            {
                foreach (PurchasePolicy p in ((CompositePurchasePolicy)toRemove).Children)
                {
                    _purchasePolicy.Children.Add(p);
                }

            }
            _purchasePolicy.Remove(policyID);
        }

        public List<PurchasePolicyModel> getAllPurchasePolicyByStoreName()
        {
            List<PurchasePolicyModel> allPurchasePolicies = new List<PurchasePolicyModel>();
            getAllPurchasePolicyByStoreNameRec(_purchasePolicy, allPurchasePolicies);
            return allPurchasePolicies;
        }

        private void getAllPurchasePolicyByStoreNameRec(PurchasePolicy root, List<PurchasePolicyModel> allPurchasePolicies)
        {
            allPurchasePolicies.Add(root.CreateModel());

            if(root is CompositePurchasePolicy)
            {
                foreach(PurchasePolicy p in ((CompositePurchasePolicy)root).Children)
                {
                    getAllPurchasePolicyByStoreNameRec(p, allPurchasePolicies);
                }
            }
        }

        //*********Manage discounts  --   REQUIREMENT 4.2*********

        public Guid addVisibleDiscount(Guid productID, float percentage, DateTime expDate)
        {
            if(percentage < 0 || expDate.CompareTo(DateTime.Now) <= 0)
            {
                return Guid.Empty;
            }

            Product prod = _inventory.getProductById(productID); 
            if(prod == null || prod.Discount != null)
            {
                return Guid.Empty;
            }

            Guid newID = Guid.NewGuid();
            VisibleDiscount newDiscount = new VisibleDiscount(percentage, expDate, newID, productID);
            prod.Discount = newDiscount; //add the new discount to the product 
            _allDiscountsMap.Add(newID, newDiscount);
            _NotInTreeProductDiscounts.Add(newID, newDiscount);
            return newID;
        }

        public Guid addCondiotionalProcuctDiscount(Guid productID, float percentage, DateTime expDate, int minQuantityForDiscount)
        {
            if (percentage < 0 || expDate.CompareTo(DateTime.Now) <= 0 || minQuantityForDiscount < 0)
            {
                return Guid.Empty;
            }

            Product prod = _inventory.getProductById(productID);
            if (prod == null || prod.Discount != null)
            {
                return Guid.Empty;
            }

            Guid newID = Guid.NewGuid();
            ConditionalProductDiscount newDiscount = new ConditionalProductDiscount(percentage, expDate, newID, productID, minQuantityForDiscount);
            prod.Discount = newDiscount; //add the new discount to the product (override if exist old one)
            _allDiscountsMap.Add(newID, newDiscount);
            _NotInTreeProductDiscounts.Add(newID, newDiscount);
            return newID;
        }

        public Guid addConditionalStoreDiscount(float percentage, DateTime expDate, int minPriceForDiscount)
        {
            if (percentage < 0 || expDate.CompareTo(DateTime.Now) <= 0 || minPriceForDiscount < 0)
            {
                return Guid.Empty;
            }

            Guid newID = Guid.NewGuid();
            ConditionalStoreDiscount newDiscount = new ConditionalStoreDiscount(minPriceForDiscount, expDate, percentage, newID);
            _storeLevelDiscounts.Children.Add(newDiscount);
            _allDiscountsMap.Add(newID, newDiscount);
            return newID;
        }

        
        private bool containsStoreLevelDiscount(List<Guid> iDS)
        {
            foreach(Guid id in iDS)
            {

                if (!_allDiscountsMap.ContainsKey(id))
                {
                    return true;
                }

                DiscountPolicy discountPolicy = _allDiscountsMap[id];

                if(discountPolicy is ConditionalStoreDiscount)
                {
                    return true;
                }
            }
            return false;
        }


        public Guid addAndDiscountPolicy(List<Guid> IDs)
        {
            if (containsStoreLevelDiscount(IDs)) //dont allow to compose store level discount
            {
                return Guid.Empty;
            }

            Guid newID = Guid.NewGuid();
            List<DiscountPolicy> newChildren = new List<DiscountPolicy>();

            //turn on the isInTree flags of the basic product discounts
            foreach (Guid id in IDs)
            {
                if (!_allDiscountsMap.ContainsKey(id))
                {
                    return Guid.Empty;
                }

                if (_NotInTreeProductDiscounts.ContainsKey(id))
                {
                    _NotInTreeProductDiscounts.Remove(id);
                }
                else // the discount already exist in the tree
                {
                    _discountPolicyTree.Remove(id); // cant exist more then one time in the tree
                }

                DiscountPolicy dis = _allDiscountsMap[id];
                newChildren.Add(dis);

            }

            AndDiscountPolicy newDiscount = new AndDiscountPolicy(newID, newChildren);
            _allDiscountsMap.Add(newID, newDiscount);
            _discountPolicyTree.Children.Add(newDiscount);
            return newID;
        }


        public Guid addOrDiscountPolicy(List<Guid> IDs)
        {
            if (containsStoreLevelDiscount(IDs)) //dont allow to compose store level discount
            {
                return Guid.Empty;
            }

            Guid newID = Guid.NewGuid();
            List<DiscountPolicy> newChildren = new List<DiscountPolicy>();

            //turn on the isInTree flags of the basic product discounts
            foreach (Guid id in IDs)
            {
                if (!_allDiscountsMap.ContainsKey(id))
                {
                    return Guid.Empty;
                }

                if (_NotInTreeProductDiscounts.ContainsKey(id)) 
                {
                    _NotInTreeProductDiscounts.Remove(id);
                }
                else // the discount already exist in the tree
                {
                    _discountPolicyTree.Remove(id); // cant exist more then one time in the tree
                }

                DiscountPolicy dis = _allDiscountsMap[id];
                newChildren.Add(dis);

            }

            OrDiscountPolicy newDiscount = new OrDiscountPolicy(newID, newChildren);
            _discountPolicyTree.Children.Add(newDiscount);
            _allDiscountsMap.Add(newID, newDiscount);
            return newID;
        }

        public Guid addXorDiscountPolicy(List<Guid> IDs)
        {
            if (containsStoreLevelDiscount(IDs)) //dont allow to compose store level discount
            {
                return Guid.Empty;
            }

            Guid newID = Guid.NewGuid();
            List<DiscountPolicy> newChildren = new List<DiscountPolicy>();

            //turn on the isInTree flags of the basic product discounts
            foreach (Guid id in IDs)
            {
                if (!_allDiscountsMap.ContainsKey(id))
                {
                    return Guid.Empty;
                }

                if (_NotInTreeProductDiscounts.ContainsKey(id))
                {
                    _NotInTreeProductDiscounts.Remove(id);
                }
                else // the discount already exist in the tree
                {
                    _discountPolicyTree.Remove(id); // cant exist more then one time in the tree
                }

                DiscountPolicy dis = _allDiscountsMap[id];
                newChildren.Add(dis);

            }

            XORDiscountPolicy newDiscount = new XORDiscountPolicy(newID, newChildren);
            _discountPolicyTree.Children.Add(newDiscount);
            _allDiscountsMap.Add(newID, newDiscount);
            return newID;
        }


        public bool removeProductDiscount(Guid discountID, Guid productID)
        {
            Product prod = _inventory.getProductById(productID);

            if(prod == null || prod.Discount == null || prod.Discount.getID() != discountID)
            {
                return false;
            }

            if (!_allDiscountsMap.ContainsKey(discountID) || !(_allDiscountsMap[discountID] is ProductDiscount))
            {
                return false;
            }

            if (_NotInTreeProductDiscounts.ContainsKey(discountID)) //not in tree
            {
                _NotInTreeProductDiscounts.Remove(discountID);
            }
            else
            {
                _discountPolicyTree.Remove(discountID);
            }

            _allDiscountsMap.Remove(discountID);
            prod.Discount = null;
            return true;
        }

        private void deepRemoveCompositeDiscount(CompositeDiscountPolicy discount)
        {
            _allDiscountsMap.Remove(discount.getID());
            foreach(DiscountPolicy d in discount.Children)
            {
                if(d is DiscountType)
                {
                    _NotInTreeProductDiscounts.Add(d.getID(), (DiscountType)d);
                }
                else //d is composite
                {
                    deepRemoveCompositeDiscount((CompositeDiscountPolicy)d);
                }
            }

        }

        public bool removeCompositeDiscount(Guid discountID)
        {

            if (!_allDiscountsMap.ContainsKey(discountID))
            {
                return false;
            }
        

            DiscountPolicy toRemove = _allDiscountsMap[discountID];

            if(!(toRemove is CompositeDiscountPolicy))
            {
                return false;
            }


            _discountPolicyTree.Remove(discountID);
            deepRemoveCompositeDiscount((CompositeDiscountPolicy)toRemove); // add product and store discounts back to _NotInTree map
            return true;
        }

        public bool removeStoreLevelDiscount(Guid discountID)
        {
            if (!_allDiscountsMap.ContainsKey(discountID) || !(_allDiscountsMap[discountID] is ConditionalStoreDiscount))
            {
                return false;
            }

            this._allDiscountsMap.Remove(discountID);
            this._storeLevelDiscounts.Remove(discountID);
            return true;
        }

        public List<DiscountPolicyModel> getAllStoreLevelDiscounts()
        {
            List<DiscountPolicyModel> allStoreLevelDiscountPolicies = new List<DiscountPolicyModel>();
            getAllDiscountsFromTree(_storeLevelDiscounts, allStoreLevelDiscountPolicies);
            return allStoreLevelDiscountPolicies;
        }

        private void getAllDiscountsFromTree(DiscountPolicy root, List<DiscountPolicyModel> allStoreLevelDiscountPolicies)
        {
            allStoreLevelDiscountPolicies.Add(root.CreateModel());

            if (root is CompositeDiscountPolicy)
            {
                foreach (DiscountPolicy d in ((CompositeDiscountPolicy)root).Children)
                {
                    getAllDiscountsFromTree(d, allStoreLevelDiscountPolicies);
                }
            }
        }

        public List<DiscountPolicyModel> getAllDiscountsForCompose()
        {
            List<DiscountPolicyModel> allDicsountsModels = new List<DiscountPolicyModel>(); //without store level discount
            getAllDiscountsFromTree(_discountPolicyTree, allDicsountsModels);

            //add all the discounts that not exist in the tree
            foreach(var d in _NotInTreeProductDiscounts)
            {
                allDicsountsModels.Add(d.Value.CreateModel());
            }
            return allDicsountsModels;
        }

        public string StoreName()
        {
            return Name;
        }

    }
}
