using ECommerceSystem.DataAccessLayer;
using ECommerceSystem.Models;
using ECommerceSystemAcceptanceTests.adapters;
using NUnit.Framework;
using System.Collections.Generic;

namespace ECommerceSystemAcceptanceTests.store_owner_requirments
{
    //section 4 requirements set up

    [TestFixture()]
    internal abstract class StoreOwnerTests
    {
        
        
        protected IBridgeAdapter _bridge;

        //product details:
        protected string _productName, _producInvName, _producInvName2, _description, _discontType, _purchaseType, _storeName, _imageURL;

        protected double _price;
        protected int _quantity, _discountPercentage, _minQuantity, _maxQuantity;
        protected Category _category;
        protected List<string> _keywords;

        //users details:
        protected string _ownerUserName;

        protected string _userName;
        protected string _managerUserName; // with default permissions
        protected string _pswd, _fname, _lname, _email;

        //store details:
        protected string _discountPolicy;

        protected string _purchasePolicy;


        [OneTimeSetUp]
        public void oneTimeSetup()
        {
            DataAccess.Instance.SetTestContext();
            _bridge = Driver.getAcceptanceBridge();
            

            //init product details:
            _productName = "Iphone";
            _producInvName = "Iphone6";
            _producInvName = "Iphone5";
            _description = "descroption";
            _discontType = "visible";
            _purchaseType = "immediate";
            _storeName = "storeName";
            _imageURL = "";
            _price = 100;
            _discountPercentage = 20;
            _quantity = 5;
            _minQuantity = -1;
            _maxQuantity = -1;
            _category = Category.CELLPHONES;
            _keywords = new List<string>();
            _keywords.Add("phone");

            //store details:
            _discountPolicy = "discount";
            _purchasePolicy = "purchase";

            //init users:
            _ownerUserName = "owner";
            _managerUserName = "manager";
            _userName = "user";

            _pswd = "h3Llo0";
            _fname = "fname";
            _lname = "lname";
            _email = "email@gmail.com";

            _bridge.register(_ownerUserName, _pswd, _fname, _lname, _email);
            _bridge.register(_userName, _pswd, _fname, _lname, _email);
            _bridge.register(_managerUserName, _pswd, _fname, _lname, _email);

            //init store:
            _bridge.login(_ownerUserName, _pswd);
            _bridge.openStore(_storeName);
            _bridge.assignManager(_managerUserName, _storeName);
            _bridge.logout();
        }

        [SetUp]
        public void setUp()
        {
            _bridge.initSessions();
            
        }

        [OneTimeTearDown]
        public void oneTimeTearDown()
        {
            DataAccess.Instance.DropTestDatabase();
            _bridge.initSessions();

        }
    }
}