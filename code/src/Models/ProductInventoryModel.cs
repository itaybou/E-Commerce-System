using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceSystem.Models
{
    public class ProductInventoryModel
    {
        public Guid ID { get; set; }

        public string Name { get; set; }

        public double Price { get; set; }

        public string Description { get; set; }

        public Category Category { get; set; }

        public double Rating { get; set; }

        public long RaterCount { get; set; }

        public List<string> Keywords { get; set; }

        public string ImageURL { get; set; }

        public string StoreName { get; set; }

        public ProductInventoryModel()
        {

        }

        public ProductInventoryModel(Guid id, string name, double price, string description, Category category, double rating, 
            long raters, HashSet<string> keywords, string imageUrl, string storeName)
        {
            ID = id;
            Name = name;
            Price = price;
            Description = description;
            Rating = rating;
            Category = category;
            RaterCount = raters;
            Keywords = keywords.ToList();
            ImageURL = imageUrl;
            StoreName = storeName;
        }
    }
}
