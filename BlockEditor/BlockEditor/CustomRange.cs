using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockEditor
{
    internal struct CustomRange
    {
        // Fields
        private int min;
        private int max;

        // Properties
        public int Min { get => min; set => min = value < 0 || value > max ? 0 : value; }
        public int Max { get => max; set => max = value < 0 || value < min ? int.MaxValue : max; }
        public static CustomRange Infinite => new CustomRange(-1, -1);

        /// <summary>
        /// Creates a range between 2 values.
        /// </summary>
        /// <param name="min">The minimum value for the range (inclusive). If minimum < 0 or minimum > maximum, it defaults to 0.</param>
        /// <param name="max">The maximum value for the range (exclusive). If maximum < 0 or maximum < minimyum, it defults to the maximum integer value.</param>
        public CustomRange(int min, int max)
        {
            this.min = min < 0 || min > max ? 0 : min;
            this.max = max < 0 || max < min ? int.MaxValue : max;
        }

        /// <summary>
        /// Converts the range into a string.
        /// </summary>
        /// <returns>The range as a string, denoted as "{min},{max}"</returns>
        public override string ToString()
        {
            return $"{min},{max}";
        }
    }
}
