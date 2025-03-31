using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakeEveryDay.States
{
    internal abstract class State
    {
        public static SpriteFont DefaultGameFont;
        public static Texture2D DefaultGameTexture;

        public virtual void Enter()
        {
            throw new Exception("Enter on base state class called. Do not call this method.");
        }
        public virtual void Exit()
        {
            throw new Exception("Enter on base state class called. Do not call this method.");
        }

        public virtual State CustomUpdate(GameTime gameTime)
        {
            throw new Exception("Update on base State class called. Do not call it, it won't help you.");
        }

        public virtual void Draw(SpriteBatch sb) { }
    }
}
