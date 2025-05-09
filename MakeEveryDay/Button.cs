﻿using System;
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
        public Button(Texture2D texture, Rectangle collision) : base(texture, collision) { }
        public bool IsPressed()
        {
            if (MouseUtils.IsJustReleased() && AsRectangle.Contains(MouseUtils.CurrentState.Position))
            {
                return true;
            }
            return false;
        }
        internal override void Draw(SpriteBatch sb)
        {
            if (!IsPressed()) 
            {
                if (AsRectangle.Contains(MouseUtils.CurrentState.Position))
                {
                    sb.Draw(Sprite, AsRectangle, Color.LightGray);
                }
                else
                {
                    sb.Draw(Sprite, AsRectangle, Color.White);
                }
            }
        }
        internal override void Draw(SpriteBatch sb, Color? colorOverwrite, float rotation, Vector2 origin, SpriteEffects effects, float? layerDepthOverwrite)
        {
            if (!IsPressed())
            {
                if (AsRectangle.Contains(MouseUtils.CurrentState.Position))
                {
                    sb.Draw(Sprite, AsRectangle, new Rectangle(0, 0, Sprite.Width, Sprite.Height), Color.LightGray, rotation, origin, effects, 1);
                }
                else
                {
                    sb.Draw(Sprite, AsRectangle, new Rectangle(0, 0, Sprite.Width, Sprite.Height), Color.White, rotation, origin, effects, 1);
                }
            }
        }
    }
}
