using ECommerceSystem.DomainLayer.StoresManagement;
using ECommerceSystem.DomainLayer.SystemManagement;
using ECommerceSystem.DomainLayer.SystemManagement.logger;
using System;
using System.Collections.Generic;

namespace ECommerceSystem.ServiceLayer
{
    public class SystemServices
    {
        private SystemManager _systemManager;

        public SystemServices()
        {
            _systemManager = SystemManager.Instance;
        }

        [Trace("Info")]
        // Use-case 2.5 - search and filter services
        public List<ProductInventory> searchProductsByCategory(Category category)
        {
            return _systemManager.SearchAndFilterSystem.searchProductsByCategory(category);
        }

        [Trace("Info")]
        public List<ProductInventory> searchProductsByName(string prodName)
        {
            return _systemManager.SearchAndFilterSystem.searchProductsByName(prodName);
        }

        [Trace("Info")]
        public List<ProductInventory> searchProductsByKeyword(List<string> keywords)
        {
            return _systemManager.SearchAndFilterSystem.searchProductsByKeyword(keywords);
        }

        [Trace("Info")]
        private List<ProductInventory> applyPriceRangeFilter(double from, double to)
        {
            return _systemManager.SearchAndFilterSystem.applyPriceRangeFilter(from, to);
        }

        [Trace("Info")]
        private List<ProductInventory> applyStoreRatingFilter(double from, double to)
        {
            return _systemManager.SearchAndFilterSystem.applyStoreRatingFilter(from, to);
        }

        [Trace("Info")]
        private List<ProductInventory> applyProductRatingFilter(double from, double to)
        {
            return _systemManager.SearchAndFilterSystem.applyProductRatingFilter(from, to);
        }

        [Trace("Info")]
        private List<ProductInventory> applyCategoryFilter(Category category)
        {
            return _systemManager.SearchAndFilterSystem.applyCategoryFilter(category);
        }

        [Trace("Info")]
        private List<ProductInventory> cancelFilter(Filters filter)
        {
            return _systemManager.SearchAndFilterSystem.cancelFilter(filter);
        }

        [Trace("Info")]
        public List<Product> purchaseUserShoppingCart(string firstName, string lastName, int id, string creditCardNumber, DateTime expirationCreditCard, int CVV, string address)
        {
            return _systemManager.purchaseUserShoppingCart(firstName, lastName, id, creditCardNumber, expirationCreditCard, CVV, address);
        }

        [Trace("Info")]
        public List<Product> purchaseProducts(List<Tuple<Store, (Product, int)>> products, string firstName, string lastName, int id, string creditCardNumber, DateTime expirationCreditCard, int CVV, string address)
        {
            return _systemManager.purchaseProducts(products, firstName, lastName, id, creditCardNumber, expirationCreditCard, CVV, address);
        }

        [Trace("Info")]
        public bool purchaseProduct(Tuple<Store, (Product, int)> product, string firstName, string lastName, int id, string creditCardNumber, DateTime expirationCreditCard, int CVV, string address)
        {
            return _systemManager.purchaseProduct(product, firstName, lastName, id, creditCardNumber, expirationCreditCard, CVV, address);
        }
    }
}