using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceSystem.DomainLayer.UserManagement;
using ECommerceSystem.DomainLayer.StoresManagement;

namespace ECommerceSystem.DomainLayer.SystemManagement
{
    class SystemManager
    {
        private static readonly Lazy<SystemManager> lazy = new Lazy<SystemManager>(() => new SystemManager());
        public static SystemManager Instance => lazy.Value;

        private UsersManagement _userManagement;
        private StoreManagement _storeManagement;


        private SystemManager()
        {
            _userManagement = UsersManagement.Instance;
            _storeManagement = StoreManagement.Instance;
        }

        public List<ProductInventory> getAllProdcuts()
        {
            return _storeManagement.getAllStoresProdcutInventories();
        }

        public List<ProductInventory> searchProductsByCategory(Category category)
        {
            return getAllProdcuts().FindAll(p => p.Category.Equals(category));
        }

        public List<ProductInventory> searchProductsByName(string prodName)
        {
            return getAllProdcuts().FindAll(p => p.Name.Equals(prodName));
        }

        public List<ProductInventory> searchProductsByKeyword(List<string> keywords)
        {
            return getAllProdcuts().FindAll(p => p.Keywords.Intersect(keywords).Any());
        }
    }
}
