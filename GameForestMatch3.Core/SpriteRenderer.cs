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
                    OriginRect = _texture.Bounds;
                    OnTextureChanged();
                }
            }
        }
        public SpriteRenderer(RenderCache renderCache, string textureName) : base(renderCache)
        {
            Texture = Resources.Get<Texture2D>(textureName);
            Color = Color.White;
        }

        public SpriteRenderer(RenderCache renderCache, Texture2D texture) : base(renderCache)
        {
            Texture = texture;
            Color = Color.White;
        }

        public SpriteRenderer(RenderCache renderCache) : base(renderCache)
        {
            Color = Color.White;
        }

        protected virtual void OnTextureChanged()
        {

        }
        
        protected internal override void Update(GameTime gameTime)
        {

        }

        protected internal override void Render(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Rect.Position, (Rectangle)OriginRect, Color , Rotation, RotationOrigin, Scale, Effects, LayerDepth);
        }
    }
}
