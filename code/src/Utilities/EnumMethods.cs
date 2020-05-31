using System;
using System.Collections.Generic;
using System.Linq;

namespace ECommerceSystem.Utilities
{
    public static class EnumMethods
    {
        public static List<string> GetValues(Type enumType)
        {
            if (!typeof(Enum).IsAssignableFrom(enumType))
                throw new ArgumentException("enumType should describe enum");

            var names = Enum.GetNames(enumType).Cast<object>();
            var values = Enum.GetValues(enumType).Cast<int>();

            return names.Zip(values, (name, value) => string.Format("{0}", name))
                        .ToList();
        }
    }
}