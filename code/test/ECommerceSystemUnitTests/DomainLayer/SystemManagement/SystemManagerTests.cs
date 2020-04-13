using NUnit.Framework;
using ECommerceSystem.DomainLayer.SystemManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceSystem.DomainLayer.StoresManagement;

namespace ECommerceSystem.DomainLayer.SystemManagement.Tests
{
    [TestFixture()]
    public class SystemManagerTests
    {
        double totalPrice;
        Dictionary<Product, int> allProducts;
        Dictionary<Store, Dictionary<Product, int>> storeProducts;
        IEnumerable<(Store, double)> storePayments;
        string firstName; string lastName;
        int id;
        string creditCardNumber;
        DateTime expirationCreditCard;
        int CVV;
        string address;

        [Test()]
        public void makePurchaseTest()
        {
            Assert.Fail();
        }

        [Test()]
        public void purchaseUserShoppingCartTest()
        {
            Assert.Fail();
        }

        [Test()]
        public void purchaseProductsTest()
        {
            Assert.Fail();
        }

        [Test()]
        public void purchaseProductTest()
        {
            Assert.Fail();
        }
    }
}