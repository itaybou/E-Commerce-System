using System;
using System.Collections.Generic;
using System.Linq;
using ECommerceSystem.DomainLayer.StoresManagement;
using ECommerceSystem.DomainLayer.UserManagement;
using NUnit.Framework;

namespace ECommerceSystemRegressionTests
{
    [TestFixture()]
    public class RegressionTests
    {
        private UsersManagement _usersManagement;
        private StoreManagement _storeManagement;

        [OneTimeSetUp]
        public void setUp()
        {
            _usersManagement = UsersManagement.Instance;
            _storeManagement = StoreManagement.Instance;
        }

        [TestCase()]
        public void AtLeastOneSystemAdmin() // 1
        {
            Assert.True(_usersManagement.UserCarts.Keys.ToList().Exists(u => u.isSystemAdmin()));
        }

        [TestCase()]
        public void StoreOwnerManagerMustBeSubscribed() // 2
        {
            var users = _usersManagement.UserCarts.Keys.ToList();
            _storeManagement.Stores.ForEach(s => s.Premmisions.ToList()
                .ForEach(p =>
                {
                    if (p.Value.isOwner())
                    {
                        Assert.True(users.Find(u => u.Name().Equals(p.Key)).isSubscribed());
                    }
                }));
        }

        [TestCase()]
        public void ActiveStoreAtLeastOneOwner() // 4
        {
            _storeManagement.Stores.ForEach(s => Assert.True(s.Premmisions.ToList().Exists(p => p.Value.isOwner())));
        }

        [TestCase()]
        public void OnlyOneStoreShoppingCartPerStore() // 6
        {
            var userCarts = _usersManagement.UserCarts.Values.Concat(new List<UserShoppingCart>() { {_usersManagement.UserCarts.Values.First() }}).ToList();
            userCarts.ForEach(uCart => {
                var stores = uCart.StoreCarts.Select(s => s.store);
                Assert.True(stores.Distinct().Count() == stores.Count());
            });
        }


        [TestCase()]
        public void UserHasAccessOnlyToHisShoppingCart() // 7
        {
            var distinctUsers = _usersManagement.UserCarts.Keys.Distinct();
            var distinctUserCarts = _usersManagement.UserCarts.Values.Distinct();
            Assert.AreEqual(distinctUsers.Count(), distinctUserCarts.Count());
        }
    }
}
