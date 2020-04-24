using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceSystem.Models
{
    public class ShoppingCartModel
    {
        private Dictionary<StoreModel, ICollection<(ProductModel, int)>> cart;

        public ShoppingCartModel(Dictionary<StoreModel, ICollection<(ProductModel, int)>> cart)
        {
            this.cart = cart;
        }

        public Dictionary<StoreModel, ICollection<(ProductModel, int)>> Cart { get => cart; set => cart = value; }
    }
}
