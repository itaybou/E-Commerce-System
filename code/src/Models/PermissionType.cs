using NUnit.Framework;
using System;
using System.Collections.Generic;
using ECommerceSystem.Utilities.extensions;
using ECommerceSystem.Utilities.attributes;

namespace ECommerceSystem.Models
{
    public enum PermissionType
    {
        [StringValue("Add Products")]
        AddProductInv,
        [StringValue("Delete Products")]
        DeleteProductInv,
        [StringValue("Modify Products")]
        ModifyProduct,
        [StringValue("Watch And Comment")]
        WatchAndComment,
        [StringValue("View Store Purchase History")]
        WatchPurchaseHistory,
        [StringValue("Manage Store Purchase Policy")]
        ManagePurchasePolicy,
        [StringValue("Manage Store Discounts")]
        ManageDiscounts
    }
}