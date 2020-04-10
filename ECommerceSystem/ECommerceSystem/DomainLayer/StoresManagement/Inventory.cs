using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceSystem.DomainLayer.StoresManagement
{
    class Inventory
    {
        private List<ProductInventory> _products;

        public List<ProductInventory> ProductInventory { get => _products; set => _products = value; }
    }
}
