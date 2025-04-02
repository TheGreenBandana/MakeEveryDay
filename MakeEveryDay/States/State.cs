using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakeEveryDay.States
{
    internal abstract class State
    {
        public static SpriteFont DefaultGameFont;
        public static Texture2D DefaultGameTexture;

        public static Game1 game1Reference;

        public virtual void Enter()
        {
            
        }
        public virtual void Exit()
        {

        }

        public virtual State CustomUpdate(GameTime gameTime)
        {
            throw new Exception("Update on base State class called. Do not call it, it won't help you.");
        }

        public virtual void Draw(SpriteBatch sb) { }
    }
}
