using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace MakeEveryDay
{
    internal interface BlockType
    {

        public string Name
        {
            get;
            set;
        }

        public float Left
        {
            get;
        }
        public float Right
        {
            get;
        }
        public float Top
        {
            get;
        }
        public float Bottom
        {
            get;
        }

        public Microsoft.Xna.Framework.Point Size
        {
            get;
        }
        public List<int> GetCurrentMods(float playerXPosition);
    }
}
