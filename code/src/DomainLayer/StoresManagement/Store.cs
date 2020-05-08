using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceSystem.DomainLayer.StoresManagement.Discount;
using ECommerceSystem.DomainLayer.StoresManagement.PurchasePolicies;
using ECommerceSystem.DomainLayer.UserManagement;
using ECommerceSystem.DomainLayer.Utilities;
using ECommerceSystem.Models;

namespace ECommerceSystem.DomainLayer.StoresManagement
{
    public class Store : IStoreInterface
    {

        private readonly Range<double> RATING_RANGE = new Range<double>(0.0, 5.0);


        private string _name;
        private double _rating;
        private long _raterCount;
        private OrDiscountPolicy _discountPolicy; //all store complex discounts
        private AndPurchasePolicy _purchasePolicy; // all store purchase policies
        private Dictionary<string, Permissions> _premmisions; // username => permissions
        private Inventory _inventory;
        private XORDiscountPolicy _storeLevelDiscounts;
        private List<StorePurchaseModel> _purchaseHistory;
        private Dictionary<Guid, DiscountPolicy> _discountsMap;

        public Store(string ownerUserName, string name)
        {
            this._discountPolicy = new OrDiscountPolicy(Guid.NewGuid());
            this._purchasePolicy = new AndPurchasePolicy(Guid.NewGuid());
            this._premmisions = new Dictionary<string, Permissions>();
            this._inventory = new Inventory();
            this.Name = name;
            this._purchaseHistory = new List<StorePurchaseModel>();
            this._storeLevelDiscounts = new XORDiscountPolicy(Guid.NewGuid());
            this._discountsMap = new Dictionary<Guid, DiscountPolicy>();
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
            _discountPolicy.calculateTotalPrice(products);

            //calc the simple discounts:
            foreach(var p in productQuantities)
            {
                if (p.Key.Discount != null && !p.Key.Discount.IsInComposite)
                {
                    p.Key.Discount.calculateTotalPrice(products);
                }
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


        public void addOwner(string userName, Permissions permissions)
        {
            _premmisions.Add(userName, permissions);
        }

        //*********Add, Delete, Modify Products*********


        //@pre - logged in user have permission to add product
        //return product(not product inventory!) id, return -1 in case of fail
        public Guid addProductInv(string activeUserName, string productName, string description, PurchaseType purchaseType, double price,
            int quantity, Category category, List<string> keywords, int minQuantity, int maxQuantity)
        {

            Guid productID = _inventory.addProductInv(productName, description, purchaseType, price, quantity, category, keywords);

            if (minQuantity != -1 && maxQuantity != -1)
            {
                ProductQuantityPolicy productPurchasePolicy = new ProductQuantityPolicy(minQuantity, maxQuantity, productID, Guid.NewGuid());
                this._purchasePolicy.Add(productPurchasePolicy);
                this.Inventory.getProductById(productID).PurchasePolicy = productPurchasePolicy;
            }
            return productID;
        }

        public void reduceProductQuantity(Product prod, int reduceBy) // lock
        {
            lock (prod)
            {
                prod.Quantity -= reduceBy;  // Store reduce product quantity
                if (prod.Quantity.Equals(0))
                    Inventory.deleteProduct(prod.Name, prod.Id);
            }
        }

        //@pre - logged in user have permission to modify product
        //return the new product id or -1 in case of fail
        public Guid addProduct(string loggedInUserName, string productInvName, PurchaseType purchaseType, int quantity, int minQuantity, int maxQuantity)
        {
            Guid productID = _inventory.addProduct(productInvName, purchaseType, quantity);
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
                _purchasePolicy.Remove(p.PurchasePolicy.ID);
            }

            //remove all discounts of the products of the productInv
            foreach (Product p in productInv.ProductList)
            {
                if (p.Discount.IsInComposite)
                {
                    _discountPolicy.Remove(p.PurchasePolicy.ID);
                }
            }

            return _inventory.deleteProductInventory(productInvName);
        }

        public bool deleteProduct(string loggedInUserName, string productInvName, Guid productID)
        {
            Product product = Inventory.getProductById(productID);
            _purchasePolicy.Remove(product.PurchasePolicy.getID());
            _discountPolicy.Remove(product.Discount.getID());
            return _inventory.deleteProduct(productInvName, productID);
        }


        //*********Modify Products*********


        //@pre - logged in user have permission to  product
        public bool modifyProductPrice(string loggedInUserName, string productName, int newPrice)
        {
            return _inventory.modifyProductPrice(productName, newPrice);
        }

        //@pre - logged in user have permission to modify product
        public bool modifyProductDiscountType(string loggedInUserName, string productInvName, Guid productID, DiscountType newDiscount)
        {
            return _inventory.modifyProductDiscountType(productInvName, productID, newDiscount);
        }

        //@pre - logged in user have permission to modify product
        public bool modifyProductPurchaseType(string loggedInUserName, string productInvName, Guid productID, PurchaseType purchaseType)
        {
            return _inventory.modifyProductPurchaseType(productInvName, productID, purchaseType);
        }

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
            _purchasePolicy.Remove(policyID);
            
        }


        //*********Manage discounts  --   REQUIREMENT 4.2*********

        public Guid addVisibleDiscount(Guid productID, float percentage, DateTime expDate)
        {
            Guid newID = Guid.NewGuid();
            VisibleDiscount newDiscount = new VisibleDiscount(percentage, expDate, newID, productID);
            Product prod = _inventory.getProductById(productID); 
            prod.Discount = newDiscount; //add the new discount to the product 
            _discountsMap.Add(newID, newDiscount);
            return newID;
        }

        public Guid addCondiotionalProcuctDiscount(Guid productID, float percentage, DateTime expDate, int minQuantityForDiscount)
        {
            Guid newID = Guid.NewGuid();
            ConditionalProductDiscount newDiscount = new ConditionalProductDiscount(percentage, expDate, newID, productID, minQuantityForDiscount);
            Product prod = _inventory.getProductById(productID);
            prod.Discount = newDiscount; //add the new discount to the product (override if exist old one)
            _discountsMap.Add(newID, newDiscount);
            return newID;
        }

        public Guid addConditionalStoreDiscount(float percentage, DateTime expDate, int minPriceForDiscount)
        {
            Guid newID = Guid.NewGuid();
            ConditionalStoreDiscount newDiscount = new ConditionalStoreDiscount(minPriceForDiscount, expDate, percentage, newID);
            _storeLevelDiscounts.Children.Add(newDiscount);
            _discountsMap.Add(newID, newDiscount);
            return newID;
        }

        
        private bool containsStoreLevelDiscount(List<Guid> iDS)
        {
            foreach(Guid id in iDS)
            {
                DiscountPolicy discountPolicy = _discountPolicy.getByID(id);
                if(discountPolicy == null)
                {
                    return false;
                }
                if(discountPolicy is ConditionalStoreDiscount)
                {
                    return false;
                }
            }
            return true;
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
            foreach(Guid id in IDs)
            {
                if(_discountPolicy.getByID(id) != null) // the discount already exist in the tree
                {
                    return Guid.Empty;
                }

                DiscountPolicy dis = _discountsMap[id];
                newChildren.Add(dis);
                if (dis is DiscountType)
                {
                    ((DiscountType)dis).IsInComposite = true;
                }
            }

            AndDiscountPolicy newDiscount = new AndDiscountPolicy(newID, newChildren);
            _discountsMap.Add(newID, newDiscount);
            _discountPolicy.Children.Add(newDiscount);
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

                if (_discountPolicy.getByID(id) != null) // the discount already exist in the tree
                {
                    return Guid.Empty;
                }

                DiscountPolicy dis = _discountsMap[id];
                newChildren.Add(dis);
                if (dis is DiscountType)
                {
                    ((DiscountType)dis).IsInComposite = true;
                }
            }

            OrDiscountPolicy newDiscount = new OrDiscountPolicy(newID, newChildren);
            _discountPolicy.Children.Add(newDiscount);
            _discountsMap.Add(newID, newDiscount);
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
                if (_discountPolicy.getByID(id) != null) // the discount already exist in the tree
                {
                    return Guid.Empty;
                }

                DiscountPolicy dis = _discountsMap[id];
                newChildren.Add(dis);
                if (dis is DiscountType)
                {
                    ((DiscountType)dis).IsInComposite = true;
                }
            }

            XORDiscountPolicy newDiscount = new XORDiscountPolicy(newID, newChildren);
            _discountPolicy.Children.Add(newDiscount);
            _discountsMap.Add(newID, newDiscount);
            return newID;
        }


        public bool removeProductDiscount(Guid discountID, Guid productID)
        {
            Product prod = _inventory.getProductById(productID);

            if(prod == null)
            {
                return false;
            }

            if (prod.Discount.IsInComposite)
            {
                _discountPolicy.Remove(discountID);
            }

            _discountsMap.Remove(discountID);
            prod.Discount = null;
            return true;
        }

        private void deepRemoveCompositeDiscount(CompositeDicountPolicy discount)
        {
            foreach(DiscountPolicy d in discount.Children)
            {
                if(d is DiscountType)
                {
                    ((DiscountType)d).IsInComposite = false;
                }
                else //d is composite
                {
                    this._discountsMap.Remove(d.getID());
                    deepRemoveCompositeDiscount((CompositeDicountPolicy)d);
                }
            }

        }

        public bool removeCompositeDiscount(Guid discountID)
        {
            CompositeDicountPolicy toRemove = (CompositeDicountPolicy)(_discountPolicy.getByID(discountID));

            if (toRemove == null) //discount id isn`t exist
            {
                return false;
            }

            _discountPolicy.Remove(discountID);
            deepRemoveCompositeDiscount(toRemove); // turn off "isInTree" flags of internal basic discounts, and remove from the discounts map the internal composite discounts
            return true;
        }

        public bool removeStoreLevelDiscount(Guid discountID)
        {
            if (!_discountsMap.ContainsKey(discountID))
            {
                return false;
            }

            this._discountsMap.Remove(discountID);
            this._storeLevelDiscounts.Remove(discountID);
            return true;
        }
    }
}
