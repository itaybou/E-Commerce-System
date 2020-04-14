using ECommerceSystem.DomainLayer.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceSystem.DomainLayer.StoresManagement
{
    public class ProductInventory : IEnumerable<Product>
    {
        private readonly Range<double> RATING_RANGE = new Range<double>(0.0, 5.0);

        private long _ID;
        private string _name;
        private string _description;
        private Category _category;
        private double _price;
        private double _rating;
        private long _raterCount;
        private HashSet<string> _keywords;
        private List<Product> _productInventory;

        public Category Category { get => _category;}
        public List<string> Keywords { get => _keywords.ToList(); }
        public double Rating { get => _rating; }
        public List<Product> ProductList{ get => _productInventory;}
        public long ID { get => _ID; }
        public string Description { get => _description; }
        public double Price1 { get => _price; }
        public long RaterCount { get => _raterCount; }
        public HashSet<string> Keywords1 { get => _keywords; }
        public string Name { get => _name; set => _name = value; }


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
            if(price < 0 || quantity < 0 || discount == null || purchaseType == null)
            {
                return null;
            }

            ProductInventory productInventory = new ProductInventory(productName, description, price, category, productInvID, keywords);
            Product newProduct = new Product(discount, purchaseType, quantity, price, productIDCounter);
            productInventory._productInventory.Add(newProduct);
            return productInventory;
        }

        public Product getProducByID(long id)
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
            if(quantity <= 0 || discount == null || purchaseType == null || price <= 0 || id < 0 || getProducByID(id) != null)
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
            if (purchaseType == null )
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
        
        public void rateProduct(double rating)
        {
            ++_raterCount;
            rating = RATING_RANGE.inRange(rating) ? rating :
                     rating < RATING_RANGE.min ? RATING_RANGE.min : RATING_RANGE.max;
            _rating = ((_rating * (_raterCount-1)) + rating) / _raterCount;
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
