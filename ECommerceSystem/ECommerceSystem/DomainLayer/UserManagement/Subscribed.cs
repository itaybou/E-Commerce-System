using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceSystem.DomainLayer.StoresManagement;

namespace ECommerceSystem.DomainLayer.UserManagement
{
    class Subscribed : IUserState
    {
        private List<Store> _storesOwned;
        private List<Store> _storesManaged;
        

        public void addOwnStore(Store store)
        {
            _storesOwned.Add(store);
        }

        internal void addManagerStore(Store store)
        {
            _storesManaged.Add(store);
        }
    }
}
