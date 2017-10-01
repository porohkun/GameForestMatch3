using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameForestMatch3.Core
{
    public class SpriteRenderer : Renderer
    {
        private Texture2D _texture;
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
        public SpriteRenderer(SpriteBatch spriteBatch, string textureName) : base(spriteBatch)
        {
            Texture = Resources.Get<Texture2D>(textureName);
            Color = Color.White;
        }

        public SpriteRenderer(SpriteBatch spriteBatch, Texture2D texture) : base(spriteBatch)
        {
            Texture = texture;
            Color = Color.White;
        }

        public SpriteRenderer(SpriteBatch spriteBatch) : base(spriteBatch)
        {
            Color = Color.White;
        }

        protected virtual void OnTextureChanged()
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
