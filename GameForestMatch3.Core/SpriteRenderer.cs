using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameForestMatch3.Core
{
    public class SpriteRenderer : GameObject
    {
        private Texture2D _texture;
        private Rectangle _rect;
        /// <summary>
        /// A texture.
        /// </summary>
        public Texture2D Texture
        {
            get => _texture;
            set
            {
                if (value != _texture)
                {
                    _texture = value;
                    OnTextureChanged();
                }
            }
        }
        /// <summary>
        /// The drawing bounds on screen.
        /// </summary>
        public Rectangle Rect
        {
            get => _rect;
            set
            {
                if (value != _rect)
                {
                    _rect = value;
                    OnRectChanged();
                }
            }
        }
        /// <summary>
        /// A Color mask.
        /// </summary>
        /// 
        public Color Color { get; set; } = Color.White;
        /// <summary>
        /// A Rotation of this sprite.
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

        public SpriteRenderer(SpriteBatch spriteBatch, string textureName) : base(spriteBatch)
        {
            Texture = Resources.Get<Texture2D>(textureName);
        }

        public SpriteRenderer(SpriteBatch spriteBatch, Texture2D texture) : base(spriteBatch)
        {
            Texture = texture;
        }

        public SpriteRenderer(SpriteBatch spriteBatch) : base(spriteBatch)
        {

        }

        protected virtual void OnTextureChanged()
        {

        }

        protected virtual void OnRectChanged()
        {

        }

        protected internal override void Update(GameTime gameTime)
        {

        }

        protected internal override void Draw(GameTime gameTime)
        {
            _spriteBatch.Draw(Texture, Rect, null, Color, Rotation, RotationOrigin, Effects, SortingLayer?.GetDepth(OrderInLayer) ?? 0f);
        }

    }
}
