using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameForestMatch3.Core
{
    public class Sprite9SliceRenderer : SpriteRenderer
    {
        private Rectf _centerRect = Rectf.Empty;
        private Rectf[,] _destinationRects = new Rectf[3, 3];
        private Rectf[,] _sourceRects = new Rectf[3, 3];

        public Rectf CenterRect
        {
            get => _centerRect;
            set
            {
                if (Texture == null) return;
                if (_centerRect == value) return;
                if (value.Width + value.X > Texture.Bounds.Width)
                    throw new ArgumentException();
                if (value.Height + value.Y > Texture.Bounds.Height)
                    throw new ArgumentException();
                _centerRect = value;
                OnCenterRectChanged();
            }
        }

        public Sprite9SliceRenderer(RenderCache renderCache, string textureName) : base(renderCache, textureName) { }
        public Sprite9SliceRenderer(RenderCache renderCache, Texture2D texture) : base(renderCache, texture) { }
        public Sprite9SliceRenderer(RenderCache renderCache) : base(renderCache) { }

        protected virtual void OnCenterRectChanged()
        {
            Recalculate9Rects();
        }

        protected override void OnTextureChanged()
        {
            Recalculate9Rects();
        }

        protected override void OnRectChanged()
        {
            base.OnRectChanged();
            Recalculate9Rects();
        }

        private void Recalculate9Rects()
        {
            if (CenterRect == Rectf.Empty)
                return;

            var tex = Texture.Bounds.Size;
            var left = CenterRect.X;
            var right = tex.X - CenterRect.Right;
            var top = CenterRect.Y;
            var bottom = tex.Y - CenterRect.Bottom;
            var width = CenterRect.Width;
            var height = CenterRect.Height;

            for (int x = 0; x < 3; x++)
                for (int y = 0; y < 3; y++)
                {
                    _destinationRects[x, y] = new Rectf(
                        x == 0 ? (Rect.X) : (x == 1 ? (Rect.X + left) : (Rect.X + Rect.Width - right)),
                        y == 0 ? (Rect.Y) : (y == 1 ? (Rect.Y + top) : (Rect.Bottom - bottom)),
                        x == 0 ? (left) : (x == 1 ? (Rect.Width - left - right) : (right)),
                        y == 0 ? (top) : (y == 1 ? (Rect.Height - top - bottom) : (bottom)));
                    _sourceRects[x, y] = new Rectf(
                        x == 0 ? (0) : (x == 1 ? (left) : (left + width)),
                        y == 0 ? (0) : (y == 1 ? (top) : (top + height)),
                        x == 0 ? (left) : (x == 1 ? (width) : (right)),
                        y == 0 ? (top) : (y == 1 ? (height) : (bottom)));
                }
        }

        protected internal override void Render(SpriteBatch spriteBatch)
        {
            if (CenterRect != Rectf.Empty)
                for (int x = 0; x < 3; x++)
                    for (int y = 0; y < 3; y++)
                    {
                        var rect = _destinationRects[x, y];
                        var origin = _sourceRects[x, y];
                        spriteBatch.Draw(Texture, rect.Position, (Rectangle)origin, Color,
                            Rotation, RotationOrigin, rect.ScaleFrom(origin), Effects, LayerDepth);
                    }
        }
    }
}
