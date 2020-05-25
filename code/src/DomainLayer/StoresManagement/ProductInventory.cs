using ECommerceSystem.DomainLayer.StoresManagement.Discount;
using ECommerceSystem.Models;
using ECommerceSystem.Utilities;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ECommerceSystem.DomainLayer.StoresManagement
{
    public class ProductInventory : IEnumerable<Product>
    {
        [BsonIgnore]
        private static readonly Range<double> RATING_RANGE = new Range<double>(0.0, 5.0);

        [BsonId]
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Category Category { get; set; }
        public double Rating { get; set; }
        public long RaterCount { get; set; }
        public HashSet<string> Keywords { get; set; }
        public List<Product> ProductList { get; set; }

        public double Price
        {
            get => Price;
            set
            {
                Price = value;
                ProductList.ForEach(p => p.BasePrice = Price);
            }
        }

        public ProductInventory(string name, string description, double price, Category category, List<string> keywords, Guid guid)
        {
            this.Name = name;
            this.Category = category;
            this.Price = price;
            this.Description = description;
            this.ProductList = new List<Product>();
            this.ID = guid;
            this.Keywords = new HashSet<string>();
            keywords.ForEach(k => Keywords.Add(k));
            RaterCount = 0;
            Rating = 0;
        }

        public static ProductInventory Create(string productName, string description,
            double price, int quantity, Category category, List<string> keywords)
        {
            if (price < 0 || quantity < 0)
            {
                return null;
            }
            var productInvGuid = GenerateId();
            var productGuid = GenerateId();
            ProductInventory productInventory = new ProductInventory(productName, description, price, category, keywords, productInvGuid);
            Product newProduct = new Product(productName, description, quantity, price, productGuid);
            productInventory.ProductList.Add(newProduct);
            return productInventory;
        }

        public Product getProducByID(Guid id)
        {
            foreach (Product p in ProductList)
            {
                if (p.Id.Equals(id))
                {
                    return p;
                }
            }
            return null;
        }

        public void modifyName(string newProductName)
        {
            this.Name = newProductName;
            foreach (Product p in ProductList)
            {
                p.Name = newProductName;
            }
        }

        public bool modifyProductQuantity(Guid productID, int newQuantity)
        {
            if (newQuantity <= 0)
            {
                return false;
            }

            Product product = getProducByID(productID);
            if (product == null)
            {
                return false;
            }
            lock (product)
            {
                product.Quantity = newQuantity;
            }
            return true;
        }

        public bool deleteProduct(Guid productID)
        {
            Product product = getProducByID(productID);
            if (product == null)
            {
                return false;
            }
            ProductList.Remove(product);
            return true;
        }

        public Guid addProduct(int quantity, double price)
        {
            if (quantity <= 0 || price <= 0)
            {
                return Guid.Empty;
            }
            var guid = GenerateId();
            ProductList.Add(new Product(Name, Description, quantity, price, guid));
            return guid;
        }

        public bool modifyProductDiscountType(Guid productID, DiscountType newDiscount)
        {
            if (newDiscount == null)
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

        public bool modifyProductPurchaseType(Guid productID, PurchaseType purchaseType)
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

        public void rateProduct(double rating)
        {
            ++RaterCount;
            rating = RATING_RANGE.inRange(rating) ? rating :
                     rating < RATING_RANGE.min ? RATING_RANGE.min : RATING_RANGE.max;
            Rating = ((Rating * (RaterCount - 1)) + rating) / RaterCount;
        }

        private static Guid GenerateId()
        {
            return Guid.NewGuid();
        }

        public IEnumerator<Product> GetEnumerator()
        {
            foreach (var product in ProductList)
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