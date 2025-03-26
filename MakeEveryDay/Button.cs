using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
namespace MakeEveryDay
{
    internal class Button : GameObject
    {
        public Button(Texture2D texture, Rectangle collision) : base(texture, collision)
        {
        }
        public bool IsPressed()
        {
            if (MouseUtils.IsJustReleased() && AsRectangle.Contains(MouseUtils.OffsetMousePosition(MouseUtils.CurrentState.Position)))
            {
                return true;
            }
            return false;
        }
        internal override void Draw(SpriteBatch sb)
        {
            if (!IsPressed()) 
            {
                sb.Draw(Sprite,AsRectangle,Color.White);
            }
        }

    }
}
