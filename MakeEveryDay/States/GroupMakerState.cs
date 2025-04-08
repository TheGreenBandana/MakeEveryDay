using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakeEveryDay.States
{
    internal class GroupMakerState : State
    {
        private BlockGroup mainGroup;
        private List<Block> blocks;

        public override void Enter()
        {
            mainGroup = new BlockGroup("test",new Vector2(200, 400));
            blocks = new List<Block>();
        }

        public override State CustomUpdate(GameTime gameTime)
        {
            if (MouseUtils.KeyJustPressed(Microsoft.Xna.Framework.Input.Keys.Enter))
            {
                blocks.Add(new Block("TestBlock", new Vector2(50, 50), 50));
            }

            return null;
        }

        public override void Draw(SpriteBatch sb)
        {
            mainGroup.Draw(sb);
            foreach(Block block in blocks)
            {
                block.Draw(sb);
            }
        }
    }
}
