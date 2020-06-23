using ECommerceSystem.DataAccessLayer;
using ECommerceSystem.DataAccessLayer.serializers;
using ECommerceSystem.DomainLayer.StoresManagement.Discount;
using ECommerceSystem.Models;
using ECommerceSystem.Utilities;
using ECommerceSystemõ.Utilities;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ECommerceSystem.DomainLayer.StoresManagement
{
    public class ProductInventory
    {
        [BsonIgnore]
        private static readonly Range<double> RATING_RANGE = new Range<double>(0.0, 5.0);

        [BsonId]
        public Guid ID { get; set; }
        [Required]
        [BsonElement("name")]
        public string Name { get; set; }
        [Required]
        [BsonElement("price")]
        public double Price { get; set; }
        [Required]
        [BsonElement("description")]
        public string Description { get; set; }
        [Required]
        [BsonElement("category")]
        public Category Category { get; set; }
        [Required]
        [BsonElement("rating")]
        public double Rating { get; set; }
        [Required]
        [BsonElement("raters")]
        public long RaterCount { get; set; }
        [Required]
        [BsonElement("keywords")]
        public HashSet<string> Keywords { get; set; }
        [Required]
        [BsonElement("products")]
        [BsonSerializer(typeof(ProductListSerializer))]
        public List<Product> ProductList { get; set; }
        [Required]
        [BsonElement("image")]
        public string ImageUrl { get; set; }
        [Required]
        [BsonElement("store")]
        public string StoreName { get; set; }

        public ProductInventory(string name, string description, double price, Category category, List<string> keywords, Guid guid, string imageUrl, string storeName)
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
            ImageUrl = imageUrl;
            StoreName = storeName;
        }

        public static (ProductInventory, Guid) Create(string productName, string description,
            double price, int quantity, Category category, List<string> keywords, string imageUrl, string storeName)
        {
            if (price < 0 || quantity < 0)
            {
                return (null, Guid.Empty);
            }
            var productInvGuid = GenerateId();
            var productGuid = GenerateId();
            ProductInventory productInventory = new ProductInventory(productName, description, price, category, keywords, productInvGuid, imageUrl, storeName);
            Product newProduct = new Product(productName, description, quantity, price, productGuid);
            productInventory.ProductList.Add(newProduct);
            DataAccess.Instance.Products.Insert(newProduct);
            return (productInventory, newProduct.Id);
        }

        public Product getProducByID(Guid id)
        {
            return DataAccess.Instance.Products.GetByIdOrNull(id, p => p.Id);
        }

        public void modifyPrice(double newPrice)
        {
            Price = newPrice;
            ProductList.ForEach(p => {
                p.BasePrice = newPrice;
                DataAccess.Instance.Products.Update(p, p.Id, x => x.Id);
            });
        }

        public void modifyName(string newProductName)
        {
            this.Name = newProductName;
            foreach (Product p in ProductList)
            {
                p.Name = newProductName;
                DataAccess.Instance.Products.Update(p, p.Id, x => x.Id);
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
                DataAccess.Instance.Products.Update(product, product.Id, p => p.Id);
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
            ProductList = ProductList.Where(p => p.Id != product.Id).ToList();
            DataAccess.Instance.Products.Remove(product, product.Id, p => p.Id);
            return true;
        }

        public Guid addProduct(int quantity, double price)
        {
            if (quantity <= 0 || price <= 0)
            {
                return Guid.Empty;
            }
            var guid = GenerateId();
            var newProduct = new Product(Name, Description, quantity, price, guid);
            ProductList.Add(newProduct);
            DataAccess.Instance.Products.Insert(newProduct);
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
            DataAccess.Instance.Products.Update(product, product.Id, p => p.Id);
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
            DataAccess.Instance.Products.Update(product, product.Id, p => p.Id);
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
    }
}