using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameForestMatch3.Core
{
    public class TextRenderer : Renderer
    {
        /// <summary>
        /// A font.
        /// </summary>
        public SpriteFont Font { get; set; }

        public string Text { get; set; }

        public TextRenderer(SpriteBatch spriteBatch, string text) : base(spriteBatch)
        {
            Text = text;
            Color = Color.Black;
        }

        protected internal override void Update(GameTime gameTime)
        {

        }

        protected internal override void Draw(GameTime gameTime)
        {
            var size = Font.MeasureString(Text);
            var scale = Mathf.Min(Rect.Width / size.X, Rect.Height / size.Y);
            if (scale > 1f)
                scale = Mathf.Floor(scale);
            size = size * scale;
            var position = new Vector2(Mathf.Round(Rect.X + (Rect.Width - size.X) / 2f), Mathf.Round(Rect.Y + (Rect.Height - size.Y) / 2f));
            _spriteBatch.DrawString(Font, Text, position, Color, Rotation, RotationOrigin, scale, Effects, SortingLayer?.GetDepth(OrderInLayer) ?? 0f);
        }
    }
}
