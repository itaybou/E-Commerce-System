using ECommerceSystem.DomainLayer.StoresManagement.Discount;
using ECommerceSystem.DomainLayer.StoresManagement.PurchasePolicies;
using ECommerceSystem.DomainLayer.UserManagement;
using ECommerceSystem.Models;
using ECommerceSystem.Utilities;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Options;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using ECommerceSystem.Models.PurchasePolicyModels;
using ECommerceSystem.Models.DiscountPolicyModels;
using ECommerceSystem.CommunicationLayer;
using ECommerceSystem.Models.notifications;
using ECommerceSystem.DataAccessLayer;

namespace ECommerceSystem.DomainLayer.StoresManagement
{
    public class Store : IStoreInterface, ISupportInitialize
    {
        private static readonly Range<double> RATING_RANGE = new Range<double>(0.0, 5.0);

        public string Name { get; set; }
        public double Rating { get; set; }
        public Inventory Inventory { get; set; }
        public List<StorePurchaseModel> PurchaseHistory { get; set; }

        [BsonDictionaryOptions(DictionaryRepresentation.ArrayOfArrays)]
        public Dictionary<string, Permissions> StorePermissions { get; set; }

        public long RaterCount { get; set; }

        public OrDiscountPolicy DiscountPolicyTree { get; set; } //all store complex discounts
        public AndPurchasePolicy PurchasePolicy { get; set; } // all store purchase policies
        public XORDiscountPolicy StoreLevelDiscounts { get; set; }
        public Dictionary<Guid, DiscountPolicy> AllDiscountsMap { get; set; }
        public Dictionary<Guid, DiscountType> NotInTreeDiscounts { get; set; }
        public Dictionary<Guid, AssignOwnerAgreement> AssignerOwnerAgreement { get; set; }


        public Store(string ownerUserName, string name)
        {
            this.DiscountPolicyTree = new OrDiscountPolicy(Guid.NewGuid());
            this.PurchasePolicy = new AndPurchasePolicy(Guid.NewGuid());
            this.StorePermissions = new Dictionary<string, Permissions>();
            this.Inventory = new Inventory();
            this.Name = name;
            this.PurchaseHistory = new List<StorePurchaseModel>();
            this.StoreLevelDiscounts = new XORDiscountPolicy(Guid.NewGuid());
            this.AllDiscountsMap = new Dictionary<Guid, DiscountPolicy>();
            this.NotInTreeDiscounts = new Dictionary<Guid, DiscountType>();
            this.AssignerOwnerAgreement = new Dictionary<Guid, AssignOwnerAgreement>();
        }

        public double getTotalPrice(Dictionary<Guid, Tuple<Product, int>> productQuantities)
        {
            double totalPrice = 0;
            Dictionary<Guid, (double basePrice, int quantity, double totalPrice)> products = new Dictionary<Guid, (double, int, double)>(); //productID => basePrice, quantity, total price per product

            //make the data structure Dictionary<Guid, (double basePrice, int quantity, double totalPrice)> for calc the complexive discounts
            foreach (var prod in productQuantities)
            {
                var pair = prod.Value;
                double basePrice = pair.Item1.BasePrice;
                int quantity = pair.Item2;
                double totalPriceProd = basePrice * quantity;
                products.Add(pair.Item1.Id, (basePrice, quantity, totalPriceProd));
            }

            //calc the complexive discounts on the tree:
            DiscountPolicyTree.calculateTotalPrice(products);

            //calc the simple discounts:
            foreach(var discount in NotInTreeDiscounts)
            {
                discount.Value.calculateTotalPrice(products); 
            }

            //calc store discounts
            this.StoreLevelDiscounts.calculateTotalPrice(products);

            //sum all prices after discounts
            foreach (var prod in products)
            {
                totalPrice += prod.Value.totalPrice;
            }
            return totalPrice;
        }

        public void addOwner(string userName, Permissions permissions)
        {
            StorePermissions.Add(userName, permissions);
        }

        //*********Add, Delete, Modify Products*********

        //@pre - logged in user have permission to add product
        //return product(not product inventory!) id, return -1 in case of fail
        public Guid addProductInv(string activeUserName, string productName, string description, double price,
            int quantity, Category category, List<string> keywords, int minQuantity, int maxQuantity, string imageUrl)
        {
            Guid productID = Inventory.addProductInv(productName, description, price, quantity, category, keywords, imageUrl);

            if (minQuantity != -1 && maxQuantity != -1)
            {
                ProductQuantityPolicy productPurchasePolicy = new ProductQuantityPolicy(minQuantity, maxQuantity, productID, Guid.NewGuid());
                this.PurchasePolicy.Add(productPurchasePolicy);
                this.Inventory.Products.First().ProductList.First().PurchasePolicy = productPurchasePolicy;

            }
            return productID;
        }


        //@pre - logged in user have permission to modify product
        //return the new product id or -1 in case of fail
        public Guid addProduct(string loggedInUserName, string productInvName, int quantity, int minQuantity, int maxQuantity)
        {
            Guid productID = Inventory.addProduct(productInvName, quantity);
            if (minQuantity != -1 && maxQuantity != -1)
            {
                ProductQuantityPolicy productPurchasePolicy = new ProductQuantityPolicy(minQuantity, maxQuantity, productID, Guid.NewGuid());
                this.PurchasePolicy.Add(productPurchasePolicy);
                this.Inventory.getProductById(productID).PurchasePolicy = productPurchasePolicy;
            }

            return productID;
        }

        public bool deleteProductInventory(string loggedInUserName, string productInvName)
        {
            //remove all policies of the products of the productInv
            ProductInventory productInv = Inventory.getProductByName(productInvName);
            foreach (Product p in productInv.ProductList)
            {
                if (p.PurchasePolicy != null)
                    PurchasePolicy.Remove(p.PurchasePolicy.ID);
            }

            //remove all discounts of the products of the productInv
            foreach (Product p in productInv.ProductList)
            {
                if (p.Discount != null)
                {
                    if (NotInTreeDiscounts.ContainsKey(p.Discount.getID()))
                    {
                        NotInTreeDiscounts.Remove(p.Discount.getID());
                    }
                    else
                    {
                        DiscountPolicyTree.Remove(p.PurchasePolicy.ID);
                    }
                    AllDiscountsMap.Remove(p.PurchasePolicy.ID);
                }
            }

            return Inventory.deleteProductInventory(productInvName);
        }

        public bool deleteProduct(string loggedInUserName, string productInvName, Guid productID)
        {
            Product product = Inventory.getProductById(productID);
            if (product == null)
            {
                return false;
            }

            bool success = Inventory.deleteProduct(productInvName, productID);

            if (success)
            {
                if (product.PurchasePolicy != null)
                    PurchasePolicy.Remove(product.PurchasePolicy.getID());
                if (product.Discount != null)
                {
                    if (NotInTreeDiscounts.ContainsKey(product.Discount.getID()))
                    {
                        NotInTreeDiscounts.Remove(product.Discount.getID());
                    }
                    else
                    {
                        DiscountPolicyTree.Remove(product.PurchasePolicy.ID);
                    }
                    AllDiscountsMap.Remove(product.PurchasePolicy.ID);
                }
                return true;
            }
            else return false;
            
        }

        //*********Modify Products*********

        //@pre - logged in user have permission to  product
        public bool modifyProductPrice(string loggedInUserName, string productName, int newPrice)
        {
            var result =  Inventory.modifyProductPrice(productName, newPrice);
            if (result)
            {
                DataAccess.Instance.Stores.Update(this, Name, s => s.Name);
                return true;
            }
            return false;
        }

        //@pre - logged in user have permission to modify product
        //public bool modifyProductDiscountType(string loggedInUserName, string productInvName, Guid productID, DiscountType newDiscount)
        //{
        //    return Inventory.modifyProductDiscountType(productInvName, productID, newDiscount);
        //}

        //@pre - logged in user have permission to modify product
        //public bool modifyProductPurchaseType(string loggedInUserName, string productInvName, Guid productID, PurchaseType purchaseType)
        //{
        //    return Inventory.modifyProductPurchaseType(productInvName, productID, purchaseType);
        //}

        //@pre - logged in user have permission to modify product
        public bool modifyProductQuantity(string loggedInUserName, string productName, Guid productID, int newQuantity)
        {
            return Inventory.modifyProductQuantity(productName, productID, newQuantity);
        }

        //@pre - logged in user have permission to modify product
        public bool modifyProductName(string loggedInUserName, string newProductName, string oldProductName)
        {
            var result = Inventory.modifyProductName(newProductName, oldProductName);
            if(result)
            {
                DataAccess.Instance.Stores.Update(this, Name, s => s.Name);
                return true;
            }
            return false;
        }

        //*********Assign*********

        public Permissions assignOwner(User assigner, string newOwneruserName)
        {
            Permissions per = null;
            if (StorePermissions.ContainsKey(newOwneruserName) && StorePermissions[newOwneruserName].isOwner() ) // The user of userName is already owner of this store
            {
                return null;
            }

            if (StorePermissions.ContainsKey(newOwneruserName)) // newOwneruserName is manager
            {
                StorePermissions[newOwneruserName].makeOwner();  
            }
            else
            {
                per = Permissions.CreateOwner(assigner, this);
                if (per == null) return null;
                StorePermissions.Add(newOwneruserName, per);
            }
            return per;
        }

        public AssignOwnerAgreement createOwnerAssignAgreement(User assigner, string newOwneruserName)
        {
            if (StorePermissions.ContainsKey(newOwneruserName) && StorePermissions[newOwneruserName].isOwner()) // The user of newOwneruserName is already owner of this store
            {
                return null;
            }

            //check that there isn`t open agreement for newOwneruserName
            foreach (var a in AssignerOwnerAgreement)
            {
                if (a.Value.AsigneeUserName.Equals(newOwneruserName))
                {
                    return null;
                }
            }

            HashSet<string> owners = getOWners();
            owners.Remove(assigner.Name); //the assigner allready approve with this request
            AssignOwnerAgreement agreement = new AssignOwnerAgreement(Guid.NewGuid(), assigner.Guid, newOwneruserName, owners);
            if(owners.Count != 0)
            {
                AssignerOwnerAgreement.Add(agreement.ID, agreement);
            }
            return agreement;
        }

        public bool approveAssignOwnerRequest(string approverUserName, AssignOwnerAgreement agreement)
        {
            if(agreement == null)
            {
                return false;
            }

            if (!agreement.approve(approverUserName))
            {
                return false;
            }

            if (agreement.isDone())
            {
                AssignerOwnerAgreement.Remove(agreement.ID);
            }
            return true;
        }

        public bool disapproveAssignOwnerRequest(string disapproverUserName, AssignOwnerAgreement agreement)
        {
            if (agreement == null)
            {
                return false;
            }

            if (agreement.disapprove(disapproverUserName))
            {
                AssignerOwnerAgreement.Remove(agreement.ID);
                return true;
            }
            else return false;
        }


        public AssignOwnerAgreement getAgreementByID(Guid agreementID)
        {
            if (AssignerOwnerAgreement.ContainsKey(agreementID))
            {
                return AssignerOwnerAgreement[agreementID];
            }
            else return null;
        }

        public HashSet<string> getOWners()
        {
            HashSet<string> owners = new HashSet<string>();

            foreach(var permission in StorePermissions)
            {
                if (permission.Value.isOwner())
                {
                    owners.Add(permission.Key);
                }
            }

            return owners;
        }

        public bool removeOwner(Guid activeUserID, string ownerToRemoveUserName)
        {
            if(!StorePermissions.ContainsKey(ownerToRemoveUserName) || !StorePermissions[ownerToRemoveUserName].isOwner()) //ownerToRemoveUserName isn`t owner of this store
            {
                return false;
            }

            if (!StorePermissions[ownerToRemoveUserName].AssignedBy.Guid.Equals(activeUserID)) //active useer isn`t the user who assign ownerToRemoveUserName
            {
                return false;
            }

            return StorePermissions.Remove(ownerToRemoveUserName);
        }


        public Permissions assignManager(User loggedInUser, string newManageruserName)
        {
            if (StorePermissions.ContainsKey(newManageruserName)) // The user of userName is already owner/manager of this store
            {
                return null;
            }

            Permissions per = UserManagement.Permissions.CreateManager(loggedInUser, this);
            if (per == null) return null;
            StorePermissions.Add(newManageruserName, per);

            return per;
        }


        public bool removeManager(User loggedInUser, string managerUserName)
        {
            if (!StorePermissions.ContainsKey(managerUserName) || StorePermissions[managerUserName].isOwner()) // The user of userName isn`t manager of this store or he is owner of this store
            {
                return false;
            }

            if (!StorePermissions[managerUserName].AssignedBy.Name.Equals(loggedInUser.Name)) //check that the logged in the user who assigned userName
            {
                return false;
            }

            StorePermissions.Remove(managerUserName);

            return true;
        }

        // @Pre - loggedInUserName is the user who assign managerUserName
        //        managerUserName is manager in this store
        public bool editPermissions(string managerUserName, List<PermissionType> permissions, string loggedInUserName)
        {
            if (!StorePermissions.ContainsKey(managerUserName) || StorePermissions[managerUserName].isOwner()) //The managerUserName isn`t manager
            {
                return false;
            }

            if (!StorePermissions[managerUserName].AssignedBy.Name.Equals(loggedInUserName)) // The loggedInUserName isn`t the owner who assign managerUserName
            {
                return false;
            }

            StorePermissions[managerUserName].edit(permissions);
            return true;
        }

        public Tuple<Store, List<ProductInventory>> getStoreInfo()
        {
            return new Tuple<Store, List<ProductInventory>>(this, Inventory.Products);
        }

        public void rateStore(double rating)
        {
            ++RaterCount;
            rating = RATING_RANGE.inRange(rating) ? rating :
                     rating < RATING_RANGE.min ? RATING_RANGE.min : RATING_RANGE.max;
            Rating = ((Rating * (RaterCount - 1)) + rating) / RaterCount;
        }

        public void logPurchase(StorePurchaseModel purchase)
        {
            PurchaseHistory.Add(purchase);
        }

        public Permissions getPermissionByName(string userName)
        {
            if (StorePermissions.ContainsKey(userName))
            {
                return StorePermissions[userName];
            }
            else return null;
        }

        public List<StorePurchaseModel> purchaseHistory()
        {
            return this.PurchaseHistory;
        }

        public Permissions getUsernamePermissions(string name)
        {
            return StorePermissions.ContainsKey(name) ? StorePermissions[name] : null;
        }

        public IDictionary<PermissionType, bool> getUsernamePermissionTypes(string name)
        {
            return StorePermissions.ContainsKey(name) ? StorePermissions[name].PermissionTypes : new Dictionary<PermissionType, bool>();
        }

        public bool canBuy(IDictionary<Guid, int> products, double totalPrice, string address)
        {
            return this.PurchasePolicy.canBuy(products, totalPrice, address);
        }

        //*********Manage Purchase Policy  --   REQUIREMENT 4.2*********

        public Guid addDayOffPolicy(List<DayOfWeek> daysOff)
        {
            Guid newID = Guid.NewGuid();
            this.PurchasePolicy.Add(new DaysOffPolicy(daysOff, newID));
            return newID;
        }

        public Guid addLocationPolicy(List<string> banLocations)
        {
            Guid newID = Guid.NewGuid();
            this.PurchasePolicy.Add(new LocationPolicy(banLocations, newID));
            return newID;
        }

        public Guid addMinPriceStorePolicy(double minPrice)
        {
            Guid newID = Guid.NewGuid();
            this.PurchasePolicy.Add(new MinPricePerStorePolicy(minPrice, newID));
            return newID;
        }

        public Guid addAndPurchasePolicy(Guid ID1, Guid ID2)
        {
            Guid newID = Guid.NewGuid();
            PurchasePolicy purchasePolicy1 = PurchasePolicy.getByID(ID1);
            PurchasePolicy purchasePolicy2 = PurchasePolicy.getByID(ID2);

            if (purchasePolicy1 == null || purchasePolicy2 == null)
            {
                return Guid.Empty;
            }

            removeFromFirstLevelPurchasePolicyTree(ID1, ID2);

            //add the new AND policy to the first level of the tree
            List<PurchasePolicy> newAndchildren = new List<PurchasePolicy> { purchasePolicy1, purchasePolicy2 };
            this.PurchasePolicy.Add(new AndPurchasePolicy(newAndchildren, newID));
            return newID;
        }

        public Guid addOrPurchasePolicy(Guid ID1, Guid ID2)
        {
            Guid newID = Guid.NewGuid();
            PurchasePolicy purchasePolicy1 = PurchasePolicy.getByID(ID1);
            PurchasePolicy purchasePolicy2 = PurchasePolicy.getByID(ID2);

            if (purchasePolicy1 == null || purchasePolicy2 == null)
            {
                return Guid.Empty;
            }

            removeFromFirstLevelPurchasePolicyTree(ID1, ID2);

            //add the new OR policy to the first level of the tree
            List<PurchasePolicy> newOrchildren = new List<PurchasePolicy> { purchasePolicy1, purchasePolicy2 };
            this.PurchasePolicy.Add(new OrPurchasePolicy(newOrchildren, newID));
            return newID;
        }

        public Guid addXorPurchasePolicy(Guid ID1, Guid ID2)
        {
            Guid newID = Guid.NewGuid();
            PurchasePolicy purchasePolicy1 = PurchasePolicy.getByID(ID1);
            PurchasePolicy purchasePolicy2 = PurchasePolicy.getByID(ID2);

            if (purchasePolicy1 == null || purchasePolicy2 == null)
            {
                return Guid.Empty;
            }

            removeFromFirstLevelPurchasePolicyTree(ID1, ID2);

            //add the new OR policy to the first level of the tree
            List<PurchasePolicy> newXorchildren = new List<PurchasePolicy> { purchasePolicy1, purchasePolicy2 };
            this.PurchasePolicy.Add(new XORPurchasePolicy(newXorchildren, newID));
            return newID;
        }

        private void removeFromFirstLevelPurchasePolicyTree(Guid ID1, Guid ID2)
        {
            List<PurchasePolicy> newChildren = new List<PurchasePolicy>();
            //remove id1, id2 from first level of policy tree
            foreach (PurchasePolicy p in PurchasePolicy.Children)
            {
                if (!p.getID().Equals(ID1) && !p.getID().Equals(ID2))
                {
                    newChildren.Add(p);
                }
            }
            PurchasePolicy.Children = newChildren;
        }

        public void removePurchasePolicy(Guid policyID)
        {
            PurchasePolicy toRemove = PurchasePolicy.getByID(policyID);
            if(toRemove == null)
            {
                return;
            }
            if(toRemove is CompositePurchasePolicy)
            {
                foreach (PurchasePolicy p in ((CompositePurchasePolicy)toRemove).Children)
                {
                    PurchasePolicy.Children.Add(p);
                }

            }
            PurchasePolicy.Remove(policyID);
        }

        public List<PurchasePolicyModel> getAllPurchasePolicyByStoreName()
        {
            List<PurchasePolicyModel> allPurchasePolicies = new List<PurchasePolicyModel>();
            getAllPurchasePolicyByStoreNameRec(PurchasePolicy, allPurchasePolicies);
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

            Product prod = Inventory.getProductById(productID); 
            if(prod == null || prod.Discount != null)
            {
                return Guid.Empty;
            }

            Guid newID = Guid.NewGuid();
            VisibleDiscount newDiscount = new VisibleDiscount(percentage, expDate, newID, productID);
            prod.Discount = newDiscount; //add the new discount to the product 
            AllDiscountsMap.Add(newID, newDiscount);
            NotInTreeDiscounts.Add(newID, newDiscount);
            return newID;
        }

        public Guid addCondiotionalProcuctDiscount(Guid productID, float percentage, DateTime expDate, int minQuantityForDiscount)
        {
            if (percentage < 0 || expDate.CompareTo(DateTime.Now) <= 0 || minQuantityForDiscount < 0)
            {
                return Guid.Empty;
            }

            Product prod = Inventory.getProductById(productID);
            if (prod == null || prod.Discount != null)
            {
                return Guid.Empty;
            }

            Guid newID = Guid.NewGuid();
            ConditionalProductDiscount newDiscount = new ConditionalProductDiscount(percentage, expDate, newID, productID, minQuantityForDiscount);
            prod.Discount = newDiscount; //add the new discount to the product (override if exist old one)
            AllDiscountsMap.Add(newID, newDiscount);
            NotInTreeDiscounts.Add(newID, newDiscount);
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
            StoreLevelDiscounts.Children.Add(newDiscount);
            AllDiscountsMap.Add(newID, newDiscount);
            return newID;
        }

        private bool containsStoreLevelDiscount(List<Guid> iDS)
        {
            foreach (Guid id in iDS)
            {

                if (!AllDiscountsMap.ContainsKey(id))
                {
                    return true;
                }

                DiscountPolicy discountPolicy = AllDiscountsMap[id];

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
                if (!AllDiscountsMap.ContainsKey(id))
                {
                    return Guid.Empty;
                }

                if (NotInTreeDiscounts.ContainsKey(id))
                {
                    NotInTreeDiscounts.Remove(id);
                }
                else // the discount already exist in the tree
                {
                    DiscountPolicyTree.Remove(id); // cant exist more then one time in the tree
                }

                DiscountPolicy dis = AllDiscountsMap[id];
                newChildren.Add(dis);

            }

            AndDiscountPolicy newDiscount = new AndDiscountPolicy(newID, newChildren);
            AllDiscountsMap.Add(newID, newDiscount);
            DiscountPolicyTree.Children.Add(newDiscount);
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
                if (!AllDiscountsMap.ContainsKey(id))
                {
                    return Guid.Empty;
                }

                if (NotInTreeDiscounts.ContainsKey(id)) 
                {
                    NotInTreeDiscounts.Remove(id);
                }
                else // the discount already exist in the tree
                {
                    DiscountPolicyTree.Remove(id); // cant exist more then one time in the tree
                }

                DiscountPolicy dis = AllDiscountsMap[id];
                newChildren.Add(dis);

            }

            OrDiscountPolicy newDiscount = new OrDiscountPolicy(newID, newChildren);
            DiscountPolicyTree.Children.Add(newDiscount);
            AllDiscountsMap.Add(newID, newDiscount);
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
                if (!AllDiscountsMap.ContainsKey(id))
                {
                    return Guid.Empty;
                }

                if (NotInTreeDiscounts.ContainsKey(id))
                {
                    NotInTreeDiscounts.Remove(id);
                }
                else // the discount already exist in the tree
                {
                    DiscountPolicyTree.Remove(id); // cant exist more then one time in the tree
                }

                DiscountPolicy dis = AllDiscountsMap[id];
                newChildren.Add(dis);

            }

            XORDiscountPolicy newDiscount = new XORDiscountPolicy(newID, newChildren);
            DiscountPolicyTree.Children.Add(newDiscount);
            AllDiscountsMap.Add(newID, newDiscount);
            return newID;
        }

        public bool removeProductDiscount(Guid discountID, Guid productID)
        {
            Product prod = Inventory.getProductById(productID);

            if(prod == null || prod.Discount == null || prod.Discount.getID() != discountID)
            {
                return false;
            }

            if (!AllDiscountsMap.ContainsKey(discountID) || !(AllDiscountsMap[discountID] is ProductDiscount))
            {
                return false;
            }

            if (NotInTreeDiscounts.ContainsKey(discountID)) //not in tree
            {
                NotInTreeDiscounts.Remove(discountID);
            }
            else
            {
                DiscountPolicyTree.Remove(discountID);
            }

            AllDiscountsMap.Remove(discountID);
            prod.Discount = null;
            return true;
        }

        private void deepRemoveCompositeDiscount(CompositeDiscountPolicy discount)
        {
            AllDiscountsMap.Remove(discount.getID());
            foreach(DiscountPolicy d in discount.Children)
            {
                if (d is DiscountType)
                {
                    NotInTreeDiscounts.Add(d.getID(), (DiscountType)d);
                }
                else //d is composite
                {
                    deepRemoveCompositeDiscount((CompositeDiscountPolicy)d);
                }
            }
        }

        public bool removeCompositeDiscount(Guid discountID)
        {

            if (!AllDiscountsMap.ContainsKey(discountID))
            {
                return false;
            }
        

            DiscountPolicy toRemove = AllDiscountsMap[discountID];

            if(!(toRemove is CompositeDiscountPolicy))
            {
                return false;
            }


            DiscountPolicyTree.Remove(discountID);
            deepRemoveCompositeDiscount((CompositeDiscountPolicy)toRemove); // add product and store discounts back to _NotInTree map
            return true;
        }

        public bool removeStoreLevelDiscount(Guid discountID)
        {
            if (!AllDiscountsMap.ContainsKey(discountID) || !(AllDiscountsMap[discountID] is ConditionalStoreDiscount))
            {
                return false;
            }

            this.AllDiscountsMap.Remove(discountID);
            this.StoreLevelDiscounts.Remove(discountID);
            return true;
        }

        public List<DiscountPolicyModel> getAllStoreLevelDiscounts()
        {
            List<DiscountPolicyModel> allStoreLevelDiscountPolicies = new List<DiscountPolicyModel>();
            getAllDiscountsFromTree(StoreLevelDiscounts, allStoreLevelDiscountPolicies);
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
            getAllDiscountsFromTree(DiscountPolicyTree, allDicsountsModels);

            //add all the discounts that not exist in the tree
            foreach(var d in NotInTreeDiscounts)
            {
                allDicsountsModels.Add(d.Value.CreateModel());
            }
            return allDicsountsModels;
        }

        public string StoreName()
        {
            return Name;
        }

        public void BeginInit()
        {
            return;
        }

        public void EndInit()
        {
            StorePermissions.Values.ToList().ForEach(p => {
                if (p.Store == null)
                    p.Store = this;
            });
        }
    }
}