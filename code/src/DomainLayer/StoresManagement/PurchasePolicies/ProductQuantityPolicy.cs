﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceSystem.DomainLayer.StoresManagement.PurchasePolicies
{
    public class ProductQuantityPolicy : PurchasePolicy
    {
        int _min;
        int _max;
        Guid _productID;
        Guid _ID;

        public ProductQuantityPolicy(int min, int max, Guid productID, Guid ID)
        {
            this._min = min;
            this._max = max;
            this._productID = productID;
            this._ID = ID;
        }

        public Guid ID { get => _ID; set => _ID = value; }

        public bool canBuy(IDictionary<Guid, int> products, double totalPrice, string address)
        {
            if (!products.ContainsKey(_productID)) return true; //the product isn`t exist in the current purchase

            int quantity = products[_productID];
            return quantity >= _min && quantity <= _max;
        }

        public Guid getID()
        {
            return _ID;
        }
    }
}