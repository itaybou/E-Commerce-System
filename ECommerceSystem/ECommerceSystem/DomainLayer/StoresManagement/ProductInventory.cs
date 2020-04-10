using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceSystem.DomainLayer.StoresManagement
{
    class ProductInventory
    {
        private string _name;
        private string _description;
        private List<Product> _productInventory;

        public List<Product> Products { get => _productInventory; set => _productInventory = value; }
        public string Name { get => _name; set => _name = value; }

    }
}
