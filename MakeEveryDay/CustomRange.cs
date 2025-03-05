using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakeEveryDay
{
    internal struct CustomRange
    {
        private int min;
        private int max;

        public CustomRange(int min, int max)
        {
            this.min = min; 
            this.max = max;
        }

        public bool IsInRange(float check)
        {
            return check >= min && check <= max;
        }

        public int GetRandom()
        {
            Random rand = new Random();

            return rand.Next(min, max);
        }

        public override string ToString()
        {
            return $"{min},{max}";
        }
    }
}
