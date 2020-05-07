using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        private DiscountPolicy _discountPolicy;
        private PurchasePolicy _purchasePolicy;
        private Dictionary<string, Permissions> _premmisions; // username => permissions
        private Inventory _inventory;

        private List<StorePurchaseModel> _purchaseHistory;


        public Store(DiscountPolicy discountPolicy, PurchasePolicy purchasePolicy, string ownerUserName, string name)
        {
            this._discountPolicy = discountPolicy;
            this._purchasePolicy = purchasePolicy;
            this._premmisions = new Dictionary<string, Permissions>();
            this._inventory = new Inventory();
            this.Name = name;
            this._purchaseHistory = new List<StorePurchaseModel>();
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
        public Guid addProductInv(string activeUserName, string productName, string description, Discount discount, PurchaseType purchaseType, double price,
            int quantity, Category category, List<string> keywords)
        {
            return _inventory.addProductInv(productName, description, discount, purchaseType, price, quantity, category, keywords);
        }

        internal void reduceProductQuantity(Product prod, int reduceBy)
        {
            prod.Quantity -= reduceBy;  // Store reduce product quantity
            if (prod.Quantity.Equals(0))
                Inventory.deleteProduct(prod.Name, prod.Id);
        }

        //@pre - logged in user have permission to modify product
        //return the new product id or -1 in case of fail
        public Guid addProduct(string loggedInUserName, string productInvName, Discount discount, PurchaseType purchaseType, int quantity)
        {
            return _inventory.addProduct(productInvName, discount, purchaseType, quantity);
        }


        //@pre - logged in user have permission to delete product
        public bool deleteProductInventory(string loggedInUserName, string productInvName)
        {
            return _inventory.deleteProductInventory(productInvName);
        }

        //@pre - logged in user have permission to modify product
        public bool deleteProduct(string loggedInUserName, string productInvName, Guid productID)
        {
            return _inventory.deleteProduct(productInvName, productID);
        }


        //*********Modify Products*********


        //@pre - logged in user have permission to modify product
        public bool modifyProductPrice(string loggedInUserName, string productName, int newPrice)
        {
            return _inventory.modifyProductPrice(productName, newPrice);
        }

        //@pre - logged in user have permission to modify product
        public bool modifyProductDiscountType(string loggedInUserName, string productInvName, Guid productID, Discount newDiscount)
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
    }
}
