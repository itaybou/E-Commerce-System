using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceSystem.DomainLayer.StoresManagement
{
    public class Inventory : IEnumerable<ProductInventory>
    {
        private List<ProductInventory> _products;
        private long _productIDCounter;
        public List<ProductInventory> Products { get => _products;}

        public Inventory()
        {
            _products = new List<ProductInventory>();
            _productIDCounter = 0;
        }
        
        //Return null if there isn`t product with name
        public ProductInventory getProductByName(string name)
        {
            foreach (ProductInventory p in _products)
            {
                if (p.Name.Equals(name))
                {
                    return p;
                }
            }
            return null;
        }

        //return product(not product inventory!) id, return -1 in case of fail
        public long addProductInv(string productName, string description, Discount discount, PurchaseType purchaseType, double price, int quantity, Category category, List<string> keywords, long productInvID)
        {
            if (productName.Equals(""))
            {
                return -1;
            }
            if (getProductByName(productName) != null) // check if the name already exist
            {
                return -1;
            }
            ProductInventory productInventory = ProductInventory.Create(productName, description, discount, purchaseType, price, quantity, category, keywords, ++_productIDCounter, productInvID);
            _products.Add(productInventory);
            return _productIDCounter;
        }

        public bool deleteProductInventory(string productName)
        {
            ProductInventory product = getProductByName(productName);
            if (product == null) // check if the name already exist
            {
                return false;
            }
            else
            {
                _products.Remove(product);
                return true;
            }
        }

        public bool modifyProductName(string newProductName, string oldProductName)
        {
            if (newProductName.Equals(""))
            {
                return false;
            }
            ProductInventory oldproductInventory = getProductByName(oldProductName);
            ProductInventory newproductInventory = getProductByName(newProductName);
            if (oldproductInventory == null || newproductInventory != null) // check that oldProductName exist and newProductName isnt exist
            {
                return false;
            }
            else
            {
                oldproductInventory.Name = newProductName;
                return true;
            }
        }

        public bool modifyProductPrice(string productName, int newPrice)
        {
            if(newPrice <= 0)
            {
                return false;
            }

            ProductInventory productInventory = getProductByName(productName);
            if (productInventory == null) // check if the product exist
            {
                return false;
            }
            else
            {
                productInventory.Price = newPrice;
                return true;
            }
        }

        public bool modifyProductQuantity(string productName, long productID, int newQuantity)
        {
            ProductInventory productInventory = getProductByName(productName);
            if (productInventory == null) // check if the product exist
            {
                return false;
            }
            else
            {
                return productInventory.modifyProductQuantity(productID, newQuantity);
            }
        }

        //return the new product id or -1 in case of fail
        public long addProduct(string productInvName, Discount discount, PurchaseType purchaseType, int quantity)
        {
            ProductInventory productInventory = getProductByName(productInvName);
            if (productInventory == null) // check if the product exist
            {
                return -1;
            }
            else
            {
                if(productInventory.addProduct(discount, purchaseType, quantity, productInventory.Price, ++_productIDCounter)){
                    return _productIDCounter;
                }
                else
                {
                    _productIDCounter--;
                    return -1;
                }
            }
        }

        public bool deleteProduct(string productInvName, long productID)
        {
            ProductInventory productInventory = getProductByName(productInvName);
            if (productInventory == null) // check if the product exist
            {
                return false;
            }
            else
            {
                return productInventory.deleteProduct(productID);
            }
        }

        public bool modifyProductDiscountType(string productInvName, long productID, Discount newDiscount)
        {
            if(newDiscount.Percentage < 0)
            {
                return false;
            }

            ProductInventory productInventory = getProductByName(productInvName);
            if (productInventory == null) // check if the product exist
            {
                return false;
            }
            else
            {
                return productInventory.modifyProductDiscountType(productID, newDiscount);
            }
        }

        public bool modifyProductPurchaseType(string productInvName, long productID, PurchaseType purchaseType)
        {
            ProductInventory productInventory = getProductByName(productInvName);
            if (productInventory == null) // check if the product exist
            {
                return false;
            }
            else
            {
                return productInventory.modifyProductPurchaseType(productID, purchaseType);
            }
        }

        public IEnumerator<ProductInventory> GetEnumerator()
        {
            foreach(var product in _products)
            {
                yield return product;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
