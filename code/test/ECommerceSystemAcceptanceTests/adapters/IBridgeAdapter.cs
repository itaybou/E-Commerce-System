using System.Collections.Generic;

namespace ECommerceSystemAcceptanceTests.adapters
{
    interface IBridgeAdapter
    {
        // Utility methods
        bool IsUserSubscribed(string username);
        bool IsUserLogged(string username);
        void usersCleanUp();
        void openStoreWithProducts(string storeName, string ownerName, List<string> products);
        void cancelSearchFilters();

        // Requirments
        bool register(string uname, string pswd, string fname, string lname, string email); // Requirment 2.2
        bool login(string uname, string pswd); // Requirment 2.3
        Dictionary<string, List<string>> ViewProdcutStoreInfo(); // Requirment 2.4
        List<string> SearchAndFilterProducts(string prodName, string catName, List<string> keywords, List<string> filters, double from, double to); // Requirment 2.5
    }
}
