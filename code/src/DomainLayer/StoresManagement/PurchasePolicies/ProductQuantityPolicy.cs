using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceSystem.DataAccessLayer;
using ECommerceSystem.Models.PurchasePolicyModels;
using MongoDB.Bson.Serialization.Attributes;

namespace ECommerceSystem.DomainLayer.StoresManagement.PurchasePolicies
{
    public class ProductQuantityPolicy : PurchasePolicy
    {
        [BsonId]
        public Guid ID { get; set; }
        public int Min { get; set; }
        public int Max { get; set; }
        public Guid ProductID { get; set; }

        public ProductQuantityPolicy(int min, int max, Guid productID, Guid Id)
        {
            this.Min = min;
            this.Max = max;
            ProductID = productID;
            ID = Id;
        }

        public bool canBuy(IDictionary<Guid, int> products, double totalPrice, string address)
        {
            if (!products.ContainsKey(ProductID)) return true; //the product isn`t exist in the current purchase

            int quantity = products[ProductID];
            return quantity >= Min && quantity <= Max;
        }

        public PurchasePolicyModel CreateModel()
        {
            var product = DataAccess.Instance.Products.GetByIdOrNull(ProductID, p => p.Id);
            return new ProductQuantityPolicyModel(this.ID, product.Name, this.Min, this.Max);
        }

        public Guid getID()
        {
            return ID;
        }
    }
}