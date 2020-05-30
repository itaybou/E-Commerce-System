using ECommerceSystem.DomainLayer.StoresManagement;
using ECommerceSystem.DomainLayer.UserManagement;
using NUnit.Framework;
using System.Collections.Generic;

namespace ECommerceSystem.DataAccessLayer
{
    public interface ITransactions
    {
        void OpenStoreTransaction(User owner, Store opened);

        void AddProductDiscountTransaction(Product product, Store store);

        void RemoveProductInventoryTransaction(List<Product> products, Store store);

        void RemoveProductTransaction(Product product, Store store);

        void AddProductPurchasePolicyTransaction(Product product, Store store);

        void ApplyRolePermissionsTransaction(User manager, Store store);
        void PurchaseTransaction(User user, ICollection<(Store, double, IDictionary<Product, int>)> storeProducts);

        void AssignOwnerTransaction(User assigner, User assignee, Store store);
    }
}