using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PresentationLayer.Models.PurchasePolicy
{
    public class AddPurchasePolicyModel
    {
        public List<DayOfWeek> DaysOff { get; set; }
        public string BannedLocations { get; set; }
        public double MinPrice { get; set; }
    }
}
