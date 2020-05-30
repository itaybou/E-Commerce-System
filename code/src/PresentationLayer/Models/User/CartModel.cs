using ECommerceSystem.Models;

namespace PresentationLayer.Models.User
{
    public class CartModel
    {
        public ShoppingCartModel UserCart { get; set; }

        public CartModel(ShoppingCartModel model)
        {
            UserCart = model;
        }
    }
}