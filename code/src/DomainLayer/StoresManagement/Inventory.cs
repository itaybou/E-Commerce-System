using ECommerceSystem.DataAccessLayer;
using ECommerceSystem.DomainLayer.StoresManagement.Discount;
using ECommerceSystem.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ECommerceSystem.DomainLayer.StoresManagement
{
    public class Inventory
    {
        public List<ProductInventory> Products { get; set; }

        public Inventory()
        {
            Products = new List<ProductInventory>();
        }

        public Inventory(List<ProductInventory> products)
        {
            Products = products;
        }

        //Return null if there isn`t product with name
        public ProductInventory getProductByName(string name)
        {
            foreach (ProductInventory p in Products)
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
            foreach (ProductInventory p in Products)
            {
                return p.getProducByID(id);
            }
            return null;
        }

        //return product(not product inventory!) id, return -1 in case of fail
        public Guid addProductInv(string productName, string description, double price, int quantity, Category category, List<string> keywords, string imageUrl, string storeName)
        {
            if (productName.Equals(""))
            {
                return Guid.Empty;
            }
            if (getProductByName(productName) != null) // check if the name already exist
            {
                return Guid.Empty;
            }
            var result = ProductInventory.Create(productName, description, price, quantity, category, keywords, imageUrl, storeName);
            if (result.Item1 == null)
            {
                return Guid.Empty;
            }
            Products.Add(result.Item1);
            return result.Item2; // product id
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
                Products = Products.Where(p => p.ID != product.ID).ToList();
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
            if (newPrice <= 0)
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
                productInventory.modifyPrice(newPrice);
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
        public Guid addProduct(string productInvName, int quantity)
        {
            ProductInventory productInventory = getProductByName(productInvName);
            if (productInventory == null) // check if the product exist
            {
                return Guid.Empty;
            }
            else
            {
                var id = productInventory.addProduct(quantity, productInventory.Price);
                if (id != Guid.Empty)
                {
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

        public bool modifyProductDiscountType(string productInvName, Guid productID, DiscountType newDiscount)
        {
            if (newDiscount.Percentage < 0)
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
    }
}