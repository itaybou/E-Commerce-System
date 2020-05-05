using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceSystem.Models
{
    public class StoreModel
    {
        private string _name;
        private double _rating;
        private long _raterCount;

        public StoreModel(string name, double rating, long raterCount)
        {
            _name = name;
            _rating = rating;
            _raterCount = raterCount;
        }

        public string Name { get => _name; set => _name = value; }
        public double Rating { get => _rating; set => _rating = value; }
        public long RaterCount { get => _raterCount; set => _raterCount = value; }
    }
}