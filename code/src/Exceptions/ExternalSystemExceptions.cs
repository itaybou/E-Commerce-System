using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceSystem.Exceptions
{
    public class ExternalSystemException : Exception
    {
        public ExternalSystemException(string message) : base(message) { }
    }
}
