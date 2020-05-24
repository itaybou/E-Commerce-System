using ECommerceSystem.Models;

namespace PresentationLayer.Models.User
{
    public class CartModel
    {
        public ShoppingCartModel UserCart { get; set; }

        //public CartModel(ShoppingCartModel cart)
        //{
        //    _userCart = cart;
        //}

        public CartModel(ShoppingCartModel model)
        {
            //var dict = new Dictionary<StoreModel, ICollection<(ProductModel, int)>>();
            //dict.Add(new StoreModel("My Store", 5.0, 30), new List<(ProductModel, int)>
            //{
            //    { (new ProductModel(Guid.NewGuid(), "Product1", "this is a long description", 5, 30.5, 25.5), 5) },
            //    { (new ProductModel(new Guid(), "Product2", "this is a long description", 5, 30.5, 25.5), 5) },
            //    { (new ProductModel(new Guid(), "Product3", "this is a long description", 5, 30.5, 25.5), 5) },
            //    { (new ProductModel(new Guid(), "Product4", "this is a long description", 5, 30.5, 25.5), 5) },
            //    { (new ProductModel(new Guid(), "Product4", "this is a long description", 5, 30.5, 25.5), 5) },
            //    { (new ProductModel(new Guid(), "Product5", "this is a long description", 5, 30.5, 25.5), 5) },
            //    { (new ProductModel(new Guid(), "Product6", "this is a long description", 5, 30.5, 25.5), 5) },
            //    { (new ProductModel(new Guid(), "Product7", "this is a long description", 5, 30.5, 25.5), 5) },
            //    { (new ProductModel(new Guid(), "Product8", "this is a long description", 5, 30.5, 25.5), 5) },
            //    { (new ProductModel(new Guid(), "Product9", "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Interdum posuere lorem ipsum dolor. Diam sollicitudin tempor id eu nisl nunc mi. Accumsan sit amet nulla facilisi morbi tempus iaculis urna.", 5, 30.5, 25.5), 5) },
            //});
            //dict.Add(new StoreModel("My Store2", 5.0, 30), new List<(ProductModel, int)>
            //{
            //    { (new ProductModel(new Guid(), "Product1", "this is a long description", 5, 30.5, 25.5), 5) },
            //    { (new ProductModel(new Guid(), "Product2", "this is a long description", 5, 30.5, 25.5), 5) },
            //    { (new ProductModel(new Guid(), "Product3", "this is a long description", 5, 30.5, 25.5), 5) },
            //    { (new ProductModel(new Guid(), "Product4", "this is a long description", 5, 30.5, 25.5), 5) },
            //    { (new ProductModel(new Guid(), "Product4", "this is a long description", 3, 30.5, 25.5), 5) },
            //    { (new ProductModel(new Guid(), "Product5", "this is a long description", 5, 30.5, 25.5), 5) },
            //    { (new ProductModel(new Guid(), "Product6", "this is a long description", 5, 30.5, 25.5), 5) },
            //    { (new ProductModel(new Guid(), "Product7", "this is a long description", 5, 30.5, 25.5), 5) },
            //    { (new ProductModel(new Guid(), "Product8", "this is a long description", 5, 30.5, 25.5), 5) },
            //    { (new ProductModel(new Guid(), "Product9", "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Interdum posuere lorem ipsum dolor. Diam sollicitudin tempor id eu nisl nunc mi. Accumsan sit amet nulla facilisi morbi tempus iaculis urna.", 5, 30.5, 25.5), 5) },
            //});
            //dict.Add(new StoreModel("My Store3", 5.0, 30), new List<(ProductModel, int)>
            //{
            //    { (new ProductModel(new Guid(), "Product1", "this is a long description", 5, 30.5, 25.5), 5) },
            //    { (new ProductModel(new Guid(), "Product2", "this is a long description", 5, 30.5, 25.5), 5) },
            //    { (new ProductModel(new Guid(), "Product3", "this is a long description", 5, 30.5, 25.5), 5) },
            //    { (new ProductModel(new Guid(), "Product4", "this is a long description", 3, 30.5, 25.5), 5) },
            //    { (new ProductModel(new Guid(), "Product4", "this is a long description", 5, 30.5, 25.5), 5) },
            //    { (new ProductModel(new Guid(), "Product5", "this is a long description", 5, 30.5, 25.5), 5) },
            //    { (new ProductModel(new Guid(), "Product6", "this is a long description", 5, 30.5, 25.5), 5) },
            //    { (new ProductModel(new Guid(), "Product7", "this is a long description", 5, 30.5, 25.5), 5) },
            //    { (new ProductModel(new Guid(), "Product8", "this is a long description", 5, 30.5, 25.5), 5) },
            //    { (new ProductModel(new Guid(), "Product9", "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Interdum posuere lorem ipsum dolor. Diam sollicitudin tempor id eu nisl nunc mi. Accumsan sit amet nulla facilisi morbi tempus iaculis urna.", 5, 30.5, 25.5), 5) },
            //});
            //UserCart = new ShoppingCartModel(dict);
            UserCart = model;
        }
    }
}