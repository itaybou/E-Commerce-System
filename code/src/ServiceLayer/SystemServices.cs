using ECommerceSystem.DomainLayer.SystemManagement;
using ECommerceSystem.DomainLayer.SystemManagement.logger;
using ECommerceSystem.Utilities;
using ECommerceSystem.Models;
using System;
using System.Collections.Generic;
using ECommerceSystem.CommunicationLayer.sessions;

namespace ECommerceSystem.ServiceLayer
{
    public class SystemServices
    {
        private SystemManager _systemManager;
        private ISessionController _sessions;

        public SystemServices()
        {
            _systemManager = SystemManager.Instance;
            _sessions = SessionController.Instance;
        }

        [Trace("Info")]
        // Use-case 2.5 - search and filter services
        public SearchResultModel getAllProducts(string category, Range<double> priceFilter, Range<double> storeRatingFilter, Range<double> productRatingFilter)
        {
            return _systemManager.SearchAndFilterSystem.getProductSearchResults(null, null, category, priceFilter, storeRatingFilter, productRatingFilter);
        }

        [Trace("Info")]
        // Use-case 2.5 - search and filter services
        public SearchResultModel searchProductsByCategory(string category, Range<double> priceFilter, Range<double> storeRatingFilter, Range<double> productRatingFilter)
        {
            return _systemManager.SearchAndFilterSystem.getProductSearchResults(null, null, category, priceFilter, storeRatingFilter, productRatingFilter);
        }

        [Trace("Info")]
        public SearchResultModel searchProductsByName(string prodName, string category, Range<double> priceFilter, Range<double> storeRatingFilter, Range<double> productRatingFilter)
        {
            return _systemManager.SearchAndFilterSystem.getProductSearchResults(prodName, null, category, priceFilter, storeRatingFilter, productRatingFilter);
        }

        /// <summary>
        /// searching product by keywords
        /// </summary>
        /// <param name="keywords"> List of keywords for searching</param>
        /// <returns>List of all the product matching the keywords</returns>
        [Trace("Info")]
        public SearchResultModel searchProductsByKeyword(List<string> keywords, string category, Range<double> priceFilter, Range<double> storeRatingFilter, Range<double> productRatingFilter)
        {
            return _systemManager.SearchAndFilterSystem.getProductSearchResults(null, keywords, category, priceFilter, storeRatingFilter, productRatingFilter);
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
        public ICollection<ProductModel> purchaseUserShoppingCart(Guid sessionID, string firstName, string lastName, int id, string creditCardNumber, DateTime expirationCreditCard, int CVV, string address)
        {
            var userID = _sessions.ResolveSession(sessionID);
            return _systemManager.purchaseUserShoppingCart(userID, firstName, lastName, id, creditCardNumber, expirationCreditCard, CVV, address);
        }

        ///// <summary>
        ///// purchase process of specific product 
        ///// </summary>
        ///// <param name="product"> the product the user want to buy and the store that own the product</param>
        ///// <param name="firstName">first name of the user</param>
        ///// <param name="lastName">last name of the user</param>
        ///// <param name="id">user id</param>
        ///// <param name="creditCardNumber">user credit card number</param>
        ///// <param name="expirationCreditCard">expiration date of credit card</param>
        ///// <param name="CVV">credit card CVV number</param>
        ///// <param name="address">user address for delivery</param>
        ///// <returns>List of unavailable products if there are any or null if succeeded purchase</returns>
        //[Trace("Info")]
        //public bool purchaseProduct(Tuple<string, (Guid, int)> product, string firstName, string lastName, int id, string creditCardNumber, DateTime expirationCreditCard, int CVV, string address)
        //{
        //    return _systemManager.purchaseProduct(product, firstName, lastName, id, creditCardNumber, expirationCreditCard, CVV, address);
        //}


        ///// <summary>
        ///// purchase process of specific products 
        ///// </summary>
        ///// <param name="products"> all the products the user want to buy and the stores that own the products</param>
        ///// <param name="firstName">first name of the user</param>
        ///// <param name="lastName">last name of the user</param>
        ///// <param name="id">user id</param>
        ///// <param name="creditCardNumber">user credit card number</param>
        ///// <param name="expirationCreditCard">expiration date of credit card</param>
        ///// <param name="CVV">credit card CVV number</param>
        ///// <param name="address">user address for delivery</param>
        ///// <returns>List of unavailable products if there are any or null if succeeded purchase</returns>
        //[Trace("Info")]
        //public List<Product> purchaseProducts(List<Tuple<Store, (Product, int)>> products, string firstName, string lastName, int id, string creditCardNumber, DateTime expirationCreditCard, int CVV, string address)
        //{
        //    return _systemManager.purchaseProducts(products, firstName, lastName, id, creditCardNumber, expirationCreditCard, CVV, address);
        //}
    }
}