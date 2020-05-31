using ECommerceSystem.Utilities.attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceSystem.Models
{
    public enum CompositeType
    {
        [StringValue("And")]
        And,
        [StringValue("Or")]
        Or,
        [StringValue("Or But Only One Of")]
        Xor
    }
}
