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
        public List<ProductInventory> Products { get => _products;}

        public Inventory()
        {
            _products = new List<ProductInventory>();
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

        public Product getProductById(Guid id)
        {
            foreach (ProductInventory p in _products)
            {
                foreach (Product prod in p)
                {
                    if (p.ID.Equals(id))
                    {
                        return prod;
                    }
                }
            }
            return null;
        }

        //return product(not product inventory!) id, return -1 in case of fail
        public Guid addProductInv(string productName, string description, Discount discount, PurchaseType purchaseType, double price, int quantity, Category category, List<string> keywords)
        {
            if (productName.Equals(""))
            {
                return Guid.Empty;
            }
            if (getProductByName(productName) != null) // check if the name already exist
            {
                return Guid.Empty;
            }
            var guid = Guid.NewGuid();
            ProductInventory productInventory = ProductInventory.Create(productName, description, discount, purchaseType, price, quantity, category, keywords, guid);
            if(productInventory == null)
            {
                return Guid.Empty;
            }
            _products.Add(productInventory);
            return guid;
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
                oldproductInventory.modifyName(newProductName);
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

        public bool modifyProductQuantity(string productName, Guid productID, int newQuantity)
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
        public Guid addProduct(string productInvName, Discount discount, PurchaseType purchaseType, int quantity)
        {
            ProductInventory productInventory = getProductByName(productInvName);
            if (productInventory == null) // check if the product exist
            {
                return Guid.Empty;
            }
            else
            {
                var id = productInventory.addProduct(productInventory.Name, productInventory.Description, discount, purchaseType, quantity, productInventory.Price);
                if (id != Guid.Empty){
                    return id;
                }
                else
                {
                    return Guid.Empty;
                }
            }
        }

        public bool deleteProduct(string productInvName, Guid productID)
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

        public bool modifyProductDiscountType(string productInvName, Guid productID, Discount newDiscount)
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

        public bool modifyProductPurchaseType(string productInvName, Guid productID, PurchaseType purchaseType)
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
