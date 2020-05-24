using NUnit.Framework;
using ECommerceSystem.DomainLayer.StoresManagement.PurchasePolicies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceSystem.Models;

namespace ECommerceSystem.DomainLayer.StoresManagement.PurchasePolicies.Tests
{

    [TestFixture()]
    public class ProductQuantityPolicyTests
    {
        Product product1;
        Product product2;
        Guid product1ID;
        Guid product2ID;

        ProductQuantityPolicy product1QuantityPolicy;
        ProductQuantityPolicy product2QuantityPolicy;

        [OneTimeSetUp]
        public void setUpFixture()
        {
            product1ID = Guid.NewGuid();
            product2ID = Guid.NewGuid();
            product1 = new Product("Iphone", "", 20, 100, product1ID);
            product2 = new Product("Galaxy", "", 20, 100, product2ID);
            product1QuantityPolicy = new ProductQuantityPolicy(0, 10, product1ID, Guid.NewGuid());
            product2QuantityPolicy = new ProductQuantityPolicy(2, 4, product2ID, Guid.NewGuid());

        }


        [Test()]
        public void canBuySuccessTest()
        {
           
            IDictionary<Guid, int> purchaseProductsQuantity = new Dictionary<Guid, int>();
            purchaseProductsQuantity.Add(product1ID, 5);
            purchaseProductsQuantity.Add(product2ID, 2);

            Assert.IsTrue(product1QuantityPolicy.canBuy(purchaseProductsQuantity, 0, null));
            Assert.IsTrue(product2QuantityPolicy.canBuy(purchaseProductsQuantity, 0, null));

        }

        [Test()]
        public void canBuyFailTest()
        {
            
            IDictionary<Guid, int> purchaseProductsQuantity1 = new Dictionary<Guid, int>();
            purchaseProductsQuantity1.Add(product1ID, 11);
            purchaseProductsQuantity1.Add(product2ID, 3);

            Assert.IsFalse(product1QuantityPolicy.canBuy(purchaseProductsQuantity1, 0, null));
            Assert.IsTrue(product2QuantityPolicy.canBuy(purchaseProductsQuantity1, 0, null));



            IDictionary<Guid, int> purchaseProductsQuantity2 = new Dictionary<Guid, int>();
            purchaseProductsQuantity2.Add(product1ID, 5);
            purchaseProductsQuantity2.Add(product2ID, 5);

            Assert.IsTrue(product1QuantityPolicy.canBuy(purchaseProductsQuantity2, 0, null));
            Assert.IsFalse(product2QuantityPolicy.canBuy(purchaseProductsQuantity2, 0, null));



            IDictionary<Guid, int> purchaseProductsQuantity3 = new Dictionary<Guid, int>();
            purchaseProductsQuantity3.Add(product1ID, 5);
            purchaseProductsQuantity3.Add(product2ID, 1);

            Assert.IsTrue(product1QuantityPolicy.canBuy(purchaseProductsQuantity3, 0, null));
            Assert.IsFalse(product2QuantityPolicy.canBuy(purchaseProductsQuantity3, 0, null));
        }


    }
}