using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakeEveryDay
{
    internal interface IDragable
    {
        public bool IsClicked { get; set; }

        //Wait to work on this until scaling branch is merged


        public void Draw(SpriteBatch sb);

    }
}
