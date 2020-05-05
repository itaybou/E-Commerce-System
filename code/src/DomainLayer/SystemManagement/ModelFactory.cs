using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceSystem.DomainLayer.StoresManagement;
using ECommerceSystem.DomainLayer.SystemManagement;
using ECommerceSystem.DomainLayer.UserManagement;

namespace ECommerceSystem.Models
{
    public static class ModelFactory
    {
        public static StoreModel CreateStore(Store s)
        {
            return new StoreModel(s.Name, s.Rating, s.RaterCount);
        }

        public static ProductModel CreateProduct(Product p)
        {
            return new ProductModel(p.Id, p.Name, p.Description, p.Quantity, p.BasePrice, p.CalculateDiscount());
        }

        public static SearchResultModel CreateSearchResult(List<ProductInventory> prods, List<string> suggestions)
        {
            var products = new List<ProductModel>();
            prods.ForEach(prod => prod.ProductList.ForEach(p => products.Add(new ProductModel(p.Id, p.Name, p.Description, p.Quantity, p.BasePrice, p.CalculateDiscount()))));
            return new SearchResultModel(products, suggestions);
        }

        public static StorePurchaseModel CreateStorePurchase(StorePurchase purchase)
        {
            return new StorePurchaseModel(purchase.User.Name(), purchase.TotalPrice, purchase.ProductsPurchased.Select(p => CreateProduct(p)).ToList());
        }

        public static ShoppingCartModel CreateShoppingCart(UserShoppingCart cart)
        {
            var cartModel = new Dictionary<StoreModel, ICollection<(ProductModel, int)>>();
            cart.StoreCarts.ForEach(s => cartModel.Add(CreateStore(s.store), s.Products.ToList().Select(p => (CreateProduct(p.Key), p.Value)).ToList()));
            return new ShoppingCartModel(cartModel);
        }

        public static UserPurchaseModel CreateUserPurchase(UserPurchase purcahse)
        {
            return new UserPurchaseModel(purcahse.PurchaseDate, purcahse.TotalPrice, purcahse.ProductsPurchased.Select(p => CreateProduct(p)).ToList(),
                purcahse.PaymentShippingMethod.FirstName, purcahse.PaymentShippingMethod.LastName, purcahse.PaymentShippingMethod.Id,
                purcahse.PaymentShippingMethod.CreditCardNumber, purcahse.PaymentShippingMethod.ExpirationCreditCard,
                purcahse.PaymentShippingMethod.CVV, purcahse.PaymentShippingMethod.Address);
        }
    }
}
