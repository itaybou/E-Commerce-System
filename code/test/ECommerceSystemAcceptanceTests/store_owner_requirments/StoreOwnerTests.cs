using System;
using System.Collections.Generic;
using System.Linq;
using ECommerceSystem.DomainLayer.StoresManagement;
using ECommerceSystemAcceptanceTests.adapters;
using NUnit.Framework;

namespace ECommerceSystemAcceptanceTests.store_owner_requirments
{
    //section 4 requirements set up

    [TestFixture()]
    abstract class StoreOwnerTests
    {
        protected IBridgeAdapter _bridge;

        //product details:
        protected string _productName, _description, _discontType, _purchaseType, _storeName;
        protected double _price;
        protected int _quantity, _discountPercentage;
        protected Category _category;
        protected List<string> _keywords;

        //users details:
        protected string _ownerUserName;
        protected string _userName;
        protected string _managerUserName; // with default permissions
        protected string _pswd, _fname, _lname, _email;

        //store details:
        protected DiscountPolicy _discountPolicy;
        protected PurchasePolicy _purchasePolicy;

        [OneTimeSetUp]
        public void oneTimeSetup()
        {
            _bridge = Driver.getAcceptanceBridge();

            //init product details:
            _productName = "Iphone";
            _description = "descroption";
            _discontType = "visible";
            _purchaseType = "immediate";
            _storeName = "storeName";
            _price = 100;
            _discountPercentage = 20;
            _quantity = 5;
            _category = Category.CELLPHONES;
            _keywords = new List<string>();
            _keywords.Add("phone");

            //store details:
            _discountPolicy = new DiscountPolicy();
            _purchasePolicy = new PurchasePolicy();

            //init users:
            _pswd = "123456";
            _fname = "fname";
            _lname = "lname";
            _email = "email@gmail.com";

            _bridge.register(_ownerUserName, _pswd, _fname, _lname, _email);
            _bridge.register(_userName, _pswd, _fname, _lname, _email);
            _bridge.register(_managerUserName, _pswd, _fname, _lname, _email);

            //init store:
            _bridge.login(_ownerUserName, _pswd);
            _bridge.openStore(_storeName, _discountPolicy, _purchasePolicy);
            _bridge.assignManager(_managerUserName, _storeName);
            _bridge.logout();
        }
    }
}
