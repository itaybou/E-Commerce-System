using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceSystem.Models
{
    public class PermissionType
    {
        private PermissionType(string value) { Value = value; }
        public string Value { get; set; }

        public static PermissionType AddProductInv = new PermissionType("Add Products");
        public static PermissionType DeleteProductInv = new PermissionType("Delete Products");
        public static PermissionType ModifyProduct = new PermissionType("Modify Products");
        public static PermissionType WatchAndComment = new PermissionType("Watch And Comment");
        public static PermissionType WatchPurchaseHistory = new PermissionType("View Store Purchase History");
        public static PermissionType ManagePurchasePolicy = new PermissionType("Manage Store Purchase Policy");
        public static PermissionType ManageDiscounts = new PermissionType("Manage Store Discounts");

        public static IDictionary<string, PermissionType> Descriptions()
        {
            return new Dictionary<string, PermissionType>
            {
                { AddProductInv.Value, AddProductInv },
                { DeleteProductInv.Value, DeleteProductInv },
                { ModifyProduct.Value, ModifyProduct},
                { WatchAndComment.Value, WatchAndComment},
                { WatchPurchaseHistory.Value, WatchPurchaseHistory},
                { ManagePurchasePolicy.Value, ManagePurchasePolicy},
                { ManageDiscounts.Value, ManageDiscounts}
            };
        }
    }
}
