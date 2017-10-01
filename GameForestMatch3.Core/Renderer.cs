using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameForestMatch3.Core
{
    public abstract class Renderer : GameObject
    {
        private Rectangle _rect;
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
        public Color Color { get; set; }
        /// <summary>
        /// A Rotation of this renderer.
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

        public Effect Shader { get; set; } = Resources.Get<Effect>("default");

        public SortingLayer SortingLayer;
        public int OrderInLayer;

        public Renderer(RenderCache renderCache) : base(renderCache)
        {
        }

        protected virtual void OnRectChanged()
        {

        }

        protected internal override void Draw(GameTime gameTime)
        {
            RenderCache.Cache(this);
        }

        protected internal abstract void Render(SpriteBatch spriteBatch);
    }
}
