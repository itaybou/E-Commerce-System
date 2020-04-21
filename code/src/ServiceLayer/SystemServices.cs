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
        public List<ProductInventory> getAllProducts()
        {
            return _systemManager.SearchAndFilterSystem.getAllProdcuts();
        }

        [Trace("Info")]
        // Use-case 2.5 - search and filter services
        public List<ProductInventory> searchProductsByCategory(Category category)
        {
            return _systemManager.SearchAndFilterSystem.searchProductsByCategory(category).ProductResults;
        }

        [Trace("Info")]
        public List<ProductInventory> searchProductsByName(string prodName)
        {
            return _systemManager.SearchAndFilterSystem.searchProductsByName(prodName).ProductResults;
        }

        /// <summary>
        /// searching product by keywords
        /// </summary>
        /// <param name="keywords"> List of keywords for searching</param>
        /// <returns>List of all the product matching the keywords</returns>
        [Trace("Info")]
        public List<ProductInventory> searchProductsByKeyword(List<string> keywords)
        {
            return _systemManager.SearchAndFilterSystem.searchProductsByKeyword(keywords).ProductResults;
        }

        /// <summary>
        /// apply price range filter 
        /// </summary>
        /// <param name="from"> the requested price to filter from </param>
        /// <param name="to">the requested price to filter to</param>
        /// <returns>List of all the product matching the filter</returns>
        [Trace("Info")]
        public List<ProductInventory> applyPriceRangeFilter(double from, double to)
        {
            return _systemManager.SearchAndFilterSystem.applyPriceRangeFilter(from, to);
        }

        /// <summary>
        /// apply store rating filter 
        /// </summary>
        /// <param name="from">the requested store rating to filter from</param>
        /// <param name="to">the requested store rating to filter to</param>
        /// <returns>List of all the product matching the filter</returns>
        [Trace("Info")]
        public List<ProductInventory> applyStoreRatingFilter(double from, double to)
        {
            return _systemManager.SearchAndFilterSystem.applyStoreRatingFilter(from, to);
        }

        /// <summary>
        /// apply product rating filter
        /// </summary>
        /// <param name="from">the requested product rating to filter from</param>
        /// <param name="to">the requested product rating to filter to</param>
        /// <returns>List of all the product matching the filter</returns>
        [Trace("Info")]
        public List<ProductInventory> applyProductRatingFilter(double from, double to)
        {
            return _systemManager.SearchAndFilterSystem.applyProductRatingFilter(from, to);
        }

        /// <summary>
        /// apply category filter
        /// </summary>
        /// <param name="category">the requested category to filter </param>
        /// <returns>List of all the product matching the filter</returns>
        [Trace("Info")]
        public List<ProductInventory> applyCategoryFilter(Category category)
        {
            return _systemManager.SearchAndFilterSystem.applyCategoryFilter(category);
        }

        /// <summary>
        /// cancel existing filter
        /// </summary>
        /// <param name="filter"> which filter to cancel </param>
        /// <returns>List of all the product matching after cancelling the filter</returns>
        [Trace("Info")]
        public List<ProductInventory> cancelFilter(Filters filter)
        {
            return _systemManager.SearchAndFilterSystem.cancelFilter(filter);
        }

        /// <summary>
        /// purchase process of user shopping cart 
        /// </summary>
        /// <param name="firstName"> first name of the user</param>
        /// <param name="lastName"> last name of the user </param>
        /// <param name="id"> user id  </param>
        /// <param name="creditCardNumber">user credit card number</param>
        /// <param name="expirationCreditCard"> expiration date of credit card</param>
        /// <param name="CVV"> credit card CVV number</param>
        /// <param name="address"> user address for delivery </param>
        /// <returns>List of unavailable products if there are any or null if succeeded purchase</returns>
        [Trace("Info")]
        public List<Product> purchaseUserShoppingCart(string firstName, string lastName, int id, string creditCardNumber, DateTime expirationCreditCard, int CVV, string address)
        {
            return _systemManager.purchaseUserShoppingCart(firstName, lastName, id, creditCardNumber, expirationCreditCard, CVV, address);
        }

        /// <summary>
        /// purchase process of specific products 
        /// </summary>
        /// <param name="products"> all the products the user want to buy and the stores that own the products</param>
        /// <param name="firstName">first name of the user</param>
        /// <param name="lastName">last name of the user</param>
        /// <param name="id">user id</param>
        /// <param name="creditCardNumber">user credit card number</param>
        /// <param name="expirationCreditCard">expiration date of credit card</param>
        /// <param name="CVV">credit card CVV number</param>
        /// <param name="address">user address for delivery</param>
        /// <returns>List of unavailable products if there are any or null if succeeded purchase</returns>
        [Trace("Info")]
        public List<Product> purchaseProducts(List<Tuple<Store, (Product, int)>> products, string firstName, string lastName, int id, string creditCardNumber, DateTime expirationCreditCard, int CVV, string address)
        {
            return _systemManager.purchaseProducts(products, firstName, lastName, id, creditCardNumber, expirationCreditCard, CVV, address);
        }

        /// <summary>
        /// purchase process of specific product 
        /// </summary>
        /// <param name="product"> the product the user want to buy and the store that own the product</param>
        /// <param name="firstName">first name of the user</param>
        /// <param name="lastName">last name of the user</param>
        /// <param name="id">user id</param>
        /// <param name="creditCardNumber">user credit card number</param>
        /// <param name="expirationCreditCard">expiration date of credit card</param>
        /// <param name="CVV">credit card CVV number</param>
        /// <param name="address">user address for delivery</param>
        /// <returns>List of unavailable products if there are any or null if succeeded purchase</returns>
        [Trace("Info")]
        public bool purchaseProduct(Tuple<Store, (Product, int)> product, string firstName, string lastName, int id, string creditCardNumber, DateTime expirationCreditCard, int CVV, string address)
        {
            return _systemManager.purchaseProduct(product, firstName, lastName, id, creditCardNumber, expirationCreditCard, CVV, address);
        }
    }
}