using System.Collections.Generic;

namespace ECommerceSystem.Models
{
    public class ShoppingCartModel
    {
        private Dictionary<StoreModel, ICollection<(ProductModel, int)>> cart;
        public double FinalPrice { get; set; }

        public ShoppingCartModel()
        {

        }

        public ShoppingCartModel(Dictionary<StoreModel, ICollection<(ProductModel, int)>> cart, double finalPrice)
        {
            this.cart = cart;
            FinalPrice = finalPrice;
        }

        public Dictionary<StoreModel, ICollection<(ProductModel, int)>> Cart { get => cart; set => cart = value; }
    }
}