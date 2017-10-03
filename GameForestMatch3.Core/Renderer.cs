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
        protected Vector2 _position;
        protected Vector2 _scale;
        protected Vector2 _size;
        protected Color _color;
        protected float _rotation;

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

        public Vector2 Position
        {
            get => _position;
            set
            {
                if (value != _position)
                {
                    _position = value;
                    OnPositionChanged();
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

        public Vector2 Size
        {
            get => _size;
            set
            {
                if (value != _size)
                {
                    _size = value;
                    OnSizeChanged();
                }
            }
        }
        /// <summary>
        /// A Color mask.
        /// </summary>
        /// 
        public Color Color
        {
            get => _color;
            set
            {
                if (value != _color)
                {
                    _color = value;
                    OnColorChanged();
                }
            }
        }
        /// <summary>
        /// A Rotation of this renderer.
        /// </summary>
        public float Rotation
        {
            get => _rotation;
            set
            {
                if (!Mathf.Approximately(value, _rotation))
                {
                    _rotation = value;
                    OnRotationChanged();
                }
            }
        }
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
            _scale = _rect.ScaleFrom(OriginRect);
            _position = _rect.Position + _rect.Size / 2f;
            _size = _rect.Size;
        }

        protected virtual void OnPositionChanged()
        {
            _rect = new Rectf(_position - _rect.Size / 2f, _rect.Size);
        }

        protected virtual void OnScaleChanged()
        {
            var w = OriginRect.Width * _scale.X;
            var h = OriginRect.Height * _scale.Y;
            _rect = new Rectf(
                _position.X - w / 2f,
                _position.Y - h / 2f,
                w,
                h);
            _size = _rect.Size;
        }

        protected virtual void OnSizeChanged()
        {
            _rect = new Rectf(
                _position.X - _size.X / 2f,
                _position.Y - _size.Y / 2f,
                _size.X,
                _size.Y);
            _scale = _rect.ScaleFrom(OriginRect);
        }

        protected virtual void OnColorChanged()
        {

        }

        protected virtual void OnRotationChanged()
        {

        }

        protected internal override void Draw(GameTime gameTime)
        {
            RenderCache.Cache(this);
        }

        protected internal abstract void Render(SpriteBatch spriteBatch);
    }
}
