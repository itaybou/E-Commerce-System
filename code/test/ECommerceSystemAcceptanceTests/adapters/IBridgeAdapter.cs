using System.Collections.Generic;

namespace ECommerceSystemAcceptanceTests.adapters
{
    interface IBridgeAdapter
    {
        // Utility methods
        bool IsUserSubscribed(string username);
        bool IsUserLogged(string username);
        void usersCleanUp();
        void storesCleanUp();
        void openStoreWithProducts(string storeName, string ownerName, List<string> products);
        void cancelSearchFilters();
        Dictionary<string, Dictionary<long, int>> getUserCartDetails();


        // Requirments
        bool register(string uname, string pswd, string fname, string lname, string email); // Requirment 2.2
        bool login(string uname, string pswd); // Requirment 2.3
        Dictionary<string, List<string>> ViewProdcutStoreInfo(); // Requirment 2.4
        List<string> SearchAndFilterProducts(string prodName, string catName, List<string> keywords, List<string> filters, double from, double to); // Requirment 2.5
        Dictionary<string, Dictionary<long, int>> AddTocart(long prodID, int quantity); // Requirment 2.6
        Dictionary<string, Dictionary<long, int>> ViewUserCart(); //Requirment 2.7
        bool RemoveFromCart(long prodID); //Requirment 2.7.1
        bool ChangeProductCartQuantity(long prodID, int quantity); //Requirment 2.7.2

        bool logout(); // Requirment 3.1
    }
}
