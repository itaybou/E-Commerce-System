using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceSystem.Utilities.interfaces
{
    public interface ICloneable<T> : ICloneable where T : ICloneable<T>
    {
        new T Clone();
    }
}
