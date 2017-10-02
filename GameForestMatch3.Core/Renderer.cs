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
        protected Rectf _rect;
        protected Vector2 _scale;
        /// <summary>
        /// The drawing bounds on screen.
        /// </summary>
        public Rectf Rect
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

        public Vector2 Scale
        {
            get => _scale;
            set
            {
                if (value != _scale)
                {
                    _scale = value;
                    OnScaleChanged();
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
        protected internal float LayerDepth => SortingLayer?.GetDepth(OrderInLayer) ?? 0f;

        protected Rectf OriginRect;

        public Renderer(RenderCache renderCache) : base(renderCache)
        {
        }

        protected virtual void OnRectChanged()
        {
            _scale = Rect.ScaleFrom(OriginRect);
        }

        protected virtual void OnScaleChanged()
        {
            var w = OriginRect.Width * Scale.X;
            var h = OriginRect.Height * Scale.Y;
            _rect = new Rectf(
                OriginRect.X - (w - OriginRect.Width) / 2f,
                OriginRect.Y - (h - OriginRect.Height) / 2f,
                w,
                h);
            _scale = new Vector2(Rect.Width / OriginRect.Width, Rect.Height / OriginRect.Height);
        }

        protected internal override void Draw(GameTime gameTime)
        {
            RenderCache.Cache(this);
        }

        protected internal abstract void Render(SpriteBatch spriteBatch);
    }
}
