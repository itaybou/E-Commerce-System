using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceSystem.Models;

namespace ECommerceSystem.DomainLayer.StoresManagement.PurchasePolicies
{
    public interface PurchasePolicy
    {
        bool canBuy(IDictionary<Guid, int> products, double totalPrice, string address);
        Guid getID();

    }
}