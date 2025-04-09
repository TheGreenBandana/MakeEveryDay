using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MakeEveryDay
{
    internal class GameObject
    {
        public static Texture2D gameObjectDefaultTexture;

        // Fields

        private Microsoft.Xna.Framework.Vector2 position;
        private Microsoft.Xna.Framework.Point size;
        private Texture2D sprite;

        // color automatically applied to object when drawn (see Draw() overloads)
        private Microsoft.Xna.Framework.Color? presetColor;

        private float? presetDrawLayer;

        // Properties - basic

        public Microsoft.Xna.Framework.Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }
        public Microsoft.Xna.Framework.Point Size
        {
            get { return size; }
            set { size = value; }
        }
        public Texture2D Sprite
        {
            get { return sprite; }
            // set { sprite = value; } If you really need this then you can use it. But like why tho?
        }
        public Microsoft.Xna.Framework.Color? PresetColor
        {
            get { return presetColor; }
            set { presetColor = value; }
        }
        public float? PresetDrawLayer
        {
            get { return presetDrawLayer; }
            set { presetDrawLayer = value; }
        }

        // Properties - fancier

        /// <summary>
        /// Returns the values associated with the location and size of the game object in an Monogame Rectangle object. 
        /// Position floating point values are converted to ints.
        /// </summary>
        public Microsoft.Xna.Framework.Rectangle AsRectangle
        {
            get {
                return new Microsoft.Xna.Framework.Rectangle(position.ToPoint(), size);
            }
            set
            {
                position = value.Location.ToVector2();
                size = value.Size;
            }
        }

        /// <summary>
        /// Returns the rectangle of the object, scaled to fit the internal width of the screen
        /// </summary>
        public Microsoft.Xna.Framework.Rectangle ScaledRectangle
        {
            get
            {
                float scaleFactor = Game1.ScreenSize.X / Game1.Width;
                Microsoft.Xna.Framework.Rectangle asRectangle = AsRectangle;
                return new(
                    (int)(asRectangle.X * scaleFactor),
                    (int)(asRectangle.Y + (scaleFactor * (Game1.Width - Game1.ScreenSize.X) * (asRectangle.Y - Game1.BridgePosition)
                        / (Game1.ScreenSize.Y * -1 * (Game1.ScreenSize.X / Game1.ScreenSize.Y)))),
                    Math.Clamp((int)(asRectangle.Width * scaleFactor), 1, int.MaxValue),
                    Math.Clamp((int)(asRectangle.Height * scaleFactor), 1, int.MaxValue));
            }
        }

        // Dimensions
        public int Width
        {
            get { return size.X; }
            set { size.X = value; }
        }
        public int Height
        {
            get { return size.Y; }
            set { size.Y = value; }
        }

        // Locations
        public float Top
        {
            get { return position.Y; }
        }
        public float Bottom
        {
            get { return position.Y + size.Y; }
        }
        public float Left
        {
            get { return position.X; }
        }
        public float Right
        {
            get { return position.X + size.X; }
        }

        // Constructors
        /// <summary>
        /// Default constructor makes most basic possible game object
        /// </summary>
        public GameObject()
        {
            this.sprite = gameObjectDefaultTexture;
            size = sprite.Bounds.Size;
            position = Microsoft.Xna.Framework.Vector2.Zero;
            presetColor = null;
        }

        /// <summary>
        /// Creates a game object at point (0,0) with provided sprite. Size is default size for texture object
        /// </summary>
        /// <param name="sprite">Texture2D to create the game object with</param>
        public GameObject(Texture2D sprite) : this()
        {
            this.sprite = sprite;
        }

        /// <summary>
        /// Creates a game object with provided sprite and position. Size is default size for texture object 
        /// </summary>
        /// <param name="sprite">Texture2D to create the game object with</param>
        /// <param name="position">initial position for the game object to start</param>
        public GameObject(Texture2D sprite, Microsoft.Xna.Framework.Vector2 position)
            : this(sprite)
        {
            this.position = position;
        }

        /// <summary>
        /// Constructor that takes a sprite, a position, and a preset color. Draws sprite with default size.
        /// </summary>
        /// <param name="sprite">sprite of the GameObject</param>
        /// <param name="position">Vector position of the GameObject</param>
        /// <param name="presetColor">color with which to draw the GameObject</param>
        public GameObject(Texture2D sprite, Microsoft.Xna.Framework.Vector2 position, Microsoft.Xna.Framework.Color? presetColor, float? presetDrawLayer)
            : this(sprite, position)
        {
            this.presetColor = presetColor;
            this.presetDrawLayer = presetDrawLayer;
        }

        /// <summary>
        /// Low-key the only constructor you're probably gonna need. 
        /// </summary>
        /// <param name="sprite">sprite to give the GameObject</param>
        /// <param name="position">Vector2 position at which to place the object</param>
        /// <param name="size">size to give the object</param>
        public GameObject(Texture2D sprite, Microsoft.Xna.Framework.Vector2 position, Microsoft.Xna.Framework.Point size)
            : this(sprite, position)
        {
            this.size = size;
        }

        /// <summary>
        /// Another Constructor, nothing to write home about. I should go to bed.
        /// </summary>
        /// <param name="sprite">sprite to give the GameObject</param>
        /// <param name="position">Vector position to place the GameObject</param>
        /// <param name="size">Point size to give the GameObject</param>
        /// <param name="presetColor">color preset to give the GameObject</param>
        public GameObject(Texture2D sprite, Microsoft.Xna.Framework.Vector2 position, Microsoft.Xna.Framework.Point size, Microsoft.Xna.Framework.Color? presetColor, float? presetDrawLayer)
            : this(sprite, position, presetColor, presetDrawLayer) 
        {
            this.size = size;
        }

        /// <summary>
        /// Constructor that takes a rectangle. Because you're sick. Disgusting integer lover. 
        /// </summary>
        /// <param name="sprite">sprite to give the GameObject</param>
        /// <param name="position">*vomits* ReCtAnGlE *vomits again* to represent the position and dimensions of the GameObject *shits*</param>
        public GameObject(Texture2D sprite, Microsoft.Xna.Framework.Rectangle position) 
            : this(sprite, position.Location.ToVector2(), position.Size) { }

        /// <summary>
        /// Constructor that takes a rectangle and a color preset. See above overload for my thoughts on rectangles.
        /// </summary>
        /// <param name="sprite"></param>
        /// <param name="position"></param>
        /// <param name="presetColor"></param>
        public GameObject(Texture2D sprite, Microsoft.Xna.Framework.Rectangle position, Microsoft.Xna.Framework.Color? presetColor, float? presetDrawLayer)
            : this(sprite, position.Location.ToVector2(), position.Size, presetColor, presetDrawLayer) { }

        /// <summary>
        /// Constructor that takes a Vector2 for size instead of a point. Information dosen't get stored any differently, your size vector is cast to a point under the hood.
        /// </summary>
        /// <param name="sprite">Sprite with which the GameObject is bestowed, and can use to reconquer the holy land from the heathen occupants. Deus Vult!</param>
        /// <param name="position">Vector position to give the GameObject</param>
        /// <param name="size">Vector size to give the GameObject</param>
        public GameObject(Texture2D sprite, Microsoft.Xna.Framework.Vector2 position, Microsoft.Xna.Framework.Vector2 size)
            : this(sprite, position, size.ToPoint()) { }

        /// <summary>
        /// Constructor that takes a Vector2 for size and a color preset.
        /// </summary>
        /// <param name="sprite"></param>
        /// <param name="position"></param>
        /// <param name="size"></param>
        /// <param name="presetColor"></param>
        public GameObject(Texture2D sprite, Microsoft.Xna.Framework.Vector2 position, Microsoft.Xna.Framework.Vector2 size, Microsoft.Xna.Framework.Color? presetColor, float? presetDrawLayer)
            : this(sprite, position, size.ToPoint(), presetColor, presetDrawLayer) { }

        // Methods

        /// <summary>
        /// Update function to call. No body.
        /// </summary>
        /// <param name="gameTime">GameTime object for the current frame</param>
        internal virtual void Update(GameTime gameTime) { }

        /// <summary>
        /// Basic draw method for the object. Assumes Begin() has already been called on the SpriteBatch
        /// </summary>
        /// <param name="sb">SpriteBatch object being drawn to, assumes begin has been called</param>
        internal virtual void Draw(SpriteBatch sb)
        {
            Microsoft.Xna.Framework.Rectangle drawingRectangle = ScaledRectangle;
            bool hovering = false;
            if (this is Block)
            {
                Block block = (Block)this;
                if (block.MouseHovering)
                    drawingRectangle = block.HoveredRectangle;
            }

            Tuple<Microsoft.Xna.Framework.Color, float> ColorAndLayer = DrawColorAndLayerHelper(null, null);
            sb.Draw(
                CheckToggleKyle(sprite),
                drawingRectangle,
                null,
                ColorAndLayer.Item1,
                0,
                Microsoft.Xna.Framework.Vector2.Zero,
                SpriteEffects.None,
                ColorAndLayer.Item2);
        }

        /// <summary>
        /// UNSCALED - Basic draw method for the object. Assumes Begin() has already been called on the SpriteBatch
        /// </summary>
        /// <param name="sb">SpriteBatch object being drawn to, assumes begin has been called</param>
        internal virtual void DrawUnscaled(SpriteBatch sb)
        {
            Tuple<Microsoft.Xna.Framework.Color, float> ColorAndLayer = DrawColorAndLayerHelper(null, null);
            sb.Draw(
                CheckToggleKyle(sprite),
                AsRectangle,
                null,
                ColorAndLayer.Item1,
                0,
                Microsoft.Xna.Framework.Vector2.Zero,
                SpriteEffects.None,
                ColorAndLayer.Item2);
        }

        /// <summary>
        /// Overloaded draw method for GameObjects
        /// </summary>
        /// <param name="sb">SpriteBatch object being drawn to, assumes begin has been called</param>
        /// <param name="colorOverwrite">Custom color to draw the object in, overrides PresetColor property</param>
        /// <param name="rotation">Rotation to give the object. Measured in whatever the regular spriteBatch.Draw() method uses</param>
        /// <param name="origin">"Center" of the object from which the position is measured when drawing</param>
        /// <param name="scale">Multiplier to the scale of the object when drawn</param>
        /// <param name="effects">Simple transformations to give to the sprite when drawn</param>
        /// <param name="layerDepthOverwrite">Layer the object is drawn to</param>
        /// <exception cref="Exception"></exception>
        internal virtual void Draw(
            SpriteBatch sb, 
            Microsoft.Xna.Framework.Color? colorOverwrite,
            float rotation, 
            Microsoft.Xna.Framework.Vector2 origin,
            float scale,
            SpriteEffects effects,
            float? layerDepthOverwrite)
        {

            Tuple<Microsoft.Xna.Framework.Color, float> ColorAndLayer = DrawColorAndLayerHelper(colorOverwrite, layerDepthOverwrite);

            sb.Draw(
                CheckToggleKyle(sprite),
                position,
                null,
                ColorAndLayer.Item1,
                rotation,
                origin,
                scale,
                effects,
                ColorAndLayer.Item2);
        }

        internal virtual void Draw(
            SpriteBatch sb,
            Microsoft.Xna.Framework.Color? colorOverwrite,
            float rotation,
            Microsoft.Xna.Framework.Vector2 origin,
            SpriteEffects effects,
            float? layerDepthOverwrite)
        {

            Tuple<Microsoft.Xna.Framework.Color, float> ColorAndLayer = DrawColorAndLayerHelper(colorOverwrite, layerDepthOverwrite);

            sb.Draw(
                CheckToggleKyle(sprite),
                ScaledRectangle,
                null,
                ColorAndLayer.Item1,
                rotation,
                origin,
                effects,
                ColorAndLayer.Item2);
        }

        /// <summary>
        /// Helper function for the more complex Draw methods, determines the proper color and layerDepth values depending on what was passed in/ is preset in the object
        /// </summary>
        /// <param name="colorOverwrite"></param>
        /// <param name="layerDepthOverwrite"></param>
        /// <returns></returns>
        private Tuple<Microsoft.Xna.Framework.Color,float> DrawColorAndLayerHelper(
            Microsoft.Xna.Framework.Color? colorOverwrite, 
            float? layerDepthOverwrite)
        {

            // Set two variables equal to the presets for the values
            Microsoft.Xna.Framework.Color? chosenColor = presetColor;
            float? chosenLayerDepth = presetDrawLayer;

            // if there are overwrites, set them to the overwrites
            if (colorOverwrite != null) chosenColor = colorOverwrite;
            if (layerDepthOverwrite != null) chosenLayerDepth = layerDepthOverwrite;

            // if those variables are still null, give them sensible default values
            if (chosenColor == null) chosenColor = Microsoft.Xna.Framework.Color.White;
            if (chosenLayerDepth == null) chosenLayerDepth = .5f;

            // Make a tuple to return the final values of the color and layer
            return new Tuple<Microsoft.Xna.Framework.Color, float>((Microsoft.Xna.Framework.Color)chosenColor, (float)chosenLayerDepth);
        }

        /// <summary>
        /// Pass in textures going into the draw method in here to check if kyle mode is enabled
        /// </summary>
        /// <param name="instanceDefaultTexture">the texture which appears for the game object normally</param>
        /// <returns>texture that should be drawn</returns>
        private Texture2D CheckToggleKyle(Texture2D instanceDefaultTexture)
        {
            if (!Game1.toggleKyle)
            {
                return instanceDefaultTexture;
            } else
            {
                return gameObjectDefaultTexture;
            }
        }
    }
}
