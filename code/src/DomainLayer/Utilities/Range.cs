using ECommerceSystem.DomainLayer.SystemManagement.logger;
using System;
using System.Diagnostics;

namespace ECommerceSystem.DomainLayer.Utilities
{
    public class Range<T> where T : IComparable<T>
    {
        /// <summary>Minimum value of the range.</summary>
        public T min { get; set; }

        /// <summary>Maximum value of the range.</summary>
        public T max { get; set; }

        public Range(T min, T max)
        {
            if (min.CompareTo(max) <= 0)
            {
                this.min = min;
                this.max = max;
            }
            else throw new ArgumentException();
        }

        [Trace("Debug")]
        public bool inRange(T value)
        {
            return value.CompareTo(min) >= 0 && value.CompareTo(max) <= 0;
        }
    }
}