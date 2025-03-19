using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace MakeEveryDay
{
    internal class AnimationState
    {
        private Texture2D texture;
        private int numFrames;
        private bool loops;
        private float timePerFrame;

        private int currentFrame;
        private double timeCounter;
        private bool finishedAnimation;


        public Texture2D Texture
        {
            get { return texture; }
        }
        public int NumFrames
        {
            get { return numFrames; }
        }
        public bool Loops
        {
            get { return loops; }
            set { loops = value; }
        }

        public AnimationState(Texture2D texture, int numFrames, bool loops, float fps)
        {
            this.texture = texture;
            this.numFrames = numFrames;
            this.loops = loops;
            timePerFrame = 1 / fps;
        }

        /// <summary>
        /// update function that increments the frame. If done turns out 
        /// </summary>
        /// <param name="gameTime">TIME FOR GAME!!</param>
        /// <returns>bool representing if the animation has finished. </returns>
        public bool Update(GameTime gameTime)
        {
            if (finishedAnimation)
            {
                return true;
            }

            timeCounter += gameTime.ElapsedGameTime.TotalSeconds;

            if (timeCounter>= timePerFrame)
            {
                currentFrame += 1;

                if (currentFrame > numFrames && loops)
                {
                    currentFrame = 0;

                } else if (currentFrame > numFrames)
                {
                    currentFrame = numFrames - 1;
                    return true;
                }

                timeCounter -= timePerFrame;
            }

            return false;
        }

        /// <summary>
        /// draw function for the animation state
        /// </summary>
        /// <param name="sb">spritebatch object for this frame, assumes begin has been called</param>
        /// <param name="destination">position vector for the image</param>
        /// <param name="color">color ofset for this frame</param>
        /// <param name="rotation">SPIN</param>
        /// <param name="origin">center of drawing this image</param>
        /// <param name="scale">linear scaling</param>
        /// <param name="effects">spriteefects</param>
        /// <param name="layerDepth">johnny depth</param>
        public void Draw(
            SpriteBatch sb,
            Microsoft.Xna.Framework.Vector2 destination,
            Microsoft.Xna.Framework.Color color,
            float rotation,
            Microsoft.Xna.Framework.Vector2 origin,
            float scale,
            SpriteEffects effects,
            float layerDepth)
        {
            sb.Draw(
                texture,
                destination,
                new Microsoft.Xna.Framework.Rectangle((texture.Width / numFrames) * currentFrame, 0, texture.Width / numFrames, texture.Height),
                color,
                rotation,
                origin,
                scale,
                effects,
                layerDepth);
        }
    }
}
