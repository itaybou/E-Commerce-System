using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PresentationLayer.Models
{
    public class OpenStoreModel
    {

        [Required(ErrorMessage = "Please Provide Store Name", AllowEmptyStrings = false)]
        public string StoreName { get; set; }
        [Required(ErrorMessage = "Please Provide A Valid Discount policy", AllowEmptyStrings = false)]
        public string DiscountPolicy { get; set; }
        [Required(ErrorMessage = "Please Provide A Valid Purchase policy", AllowEmptyStrings = false)]
        public string PurchasePolicy { get; set; }
    }
}
