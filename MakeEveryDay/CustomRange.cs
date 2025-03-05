using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakeEveryDay
{
    internal struct CustomRange
    {
        // Fields
        private int min;
        private int max;

        // Properties
        public int Min { get => min; set => min = value < 0 || value > max ? 0 : value; }
        public int Max { get => max; set => max = value < 0 || value < min ? int.MaxValue : max; }

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
        /// Checks to see if a value is within the range.
        /// </summary>
        /// <param name="check">The value to be checked for within the range.</param>
        /// <returns>Whether the value is within the range.</returns>
        public bool IsInRange(float check)
        {
            return check >= min && check < max;
        }

        /// <summary>
        /// Gets a random integer from within the range.
        /// </summary>
        /// <returns>The random integer from within the range.</returns>
        public int GetRandom()
        {
            Random rand = new Random();

            return rand.Next(min, max);
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
