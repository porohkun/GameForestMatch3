using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameForestMatch3.Core
{
    public class TextRenderer : GameObject
    {
        /// <summary>
        /// A font.
        /// </summary>
        public SpriteFont Font { get; set; }
        /// <summary>
        /// The drawing bounds on screen.
        /// </summary>
        public Rectangle Rect { get; set; }
        /// <summary>
        /// A color mask.
        /// </summary>
        public Color Color { get; set; } = Color.Black;
        /// <summary>
        /// A rotation of this string.
        /// </summary>
        public float Rotation { get; set; }
        /// <summary>
        /// Center of the Rotation. 0,0 by default.
        /// </summary>
        public Vector2 RotationOrigin { get; set; }
        /// <summary>
        /// Modificators for drawing. Can be combined.
        /// </summary>
        public SpriteEffects Effects { get; set; }

        public SortingLayer SortingLayer;
        public int OrderInLayer;

        public string Text { get; set; }

        public TextRenderer(SpriteBatch spriteBatch, string text) : base(spriteBatch)
        {
            Text = text;
        }

        protected internal override void Update(GameTime gameTime)
        {

        }

        protected internal override void Draw(GameTime gameTime)
        {
            var size = Font.MeasureString(Text);
            var scale = Mathf.Min(Rect.Width / size.X, Rect.Height / size.Y);
            size = size * scale;
            var position = new Vector2(Rect.X + (Rect.Width - size.X) / 2f, Rect.Y + (Rect.Height - size.Y) / 2f);
            _spriteBatch.DrawString(Font, Text, position, Color, Rotation, RotationOrigin, scale, Effects, SortingLayer?.GetDepth(OrderInLayer) ?? 0f);
        }
    }
}
