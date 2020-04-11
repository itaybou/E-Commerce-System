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
    }
}
