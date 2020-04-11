using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceSystem.DomainLayer.StoresManagement
{
    class ProductInventory : IEnumerable<Product>
    {
        private long _ID;
        private string _name;
        private string _description;
        private Category _category;
        private double _price;
        private HashSet<string> _keywords;
        private List<Product> _productInventory;

        public string Name { get => _name; set => _name = value; }
        public Category Category { get => _category; set => _category = value; }
        public List<string> Keywords { get => _keywords.ToList(); }

        public double Price {
            get => _price;
            set
            {
                _price = value;
                _productInventory.ForEach(p => p.BasePrice = _price);
            }
        }

        private ProductInventory(string name, string description, double price, Category category, long ID, List<string> keywords)
        {
            this._name = name;
            this._category = category;
            this._price = price;
            this._description = description;
            this._productInventory = new List<Product>();
            this._ID = ID;
            this._keywords = new HashSet<string>();
            keywords.ForEach(k => _keywords.Add(k));
        }

        public static ProductInventory Create(string productName, string description, Discount discount, PurchaseType purchaseType, 
            double price, int quantity, Category category, List<string> keywords, long productIDCounter, long productInvID)
        {
            ProductInventory productInventory = new ProductInventory(productName, description, price, category, productInvID, keywords);
            Product newProduct = new Product(discount, purchaseType, quantity, price, productIDCounter);
            productInventory._productInventory.Add(newProduct);
            return productInventory;
        }

        private Product getProducByID(long id)
        {
            foreach(Product p in _productInventory)
            {
                if(p.Id == id)
                {
                    return p;
                }
            }
            return null;
        }

        public bool modifyProductQuantity(int productID, int newQuantity)
        {
            if(newQuantity <= 0)
            {
                return false;
            }

            Product product = getProducByID(productID);
            if(product == null)
            {
                return false;
            }
            product.Quantity = newQuantity;
            return true;
        }

        public bool deleteProduct(int productID)
        {
            Product product = getProducByID(productID);
            if (product == null)
            {
                return false;
            }
            _productInventory.Remove(product);
            return true;
        }

        public bool addProduct(Discount discount, PurchaseType purchaseType, int quantity, double price, long id)
        {
            if(quantity <= 0)
            {
                return false;
            }
            _productInventory.Add(new Product(discount, purchaseType, quantity, price, id));
            return true;
        }

        public bool modifyProductDiscountType(int productID, Discount newDiscount)
        {
            if(newDiscount == null)
            {
                return false;
            }

            Product product = getProducByID(productID);
            if (product == null)
            {
                return false;
            }
            product.Discount = newDiscount;
            return true;
        }

        public bool modifyProductPurchaseType(int productID, PurchaseType purchaseType)
        {
            if (purchaseType == null)
            {
                return false;
            }

            Product product = getProducByID(productID);
            if (product == null)
            {
                return false;
            }
            product.PurchaseType = purchaseType;
            return true;
        }

        public IEnumerator<Product> GetEnumerator()
        {
            foreach(var product in _productInventory)
            {
                yield return product;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
