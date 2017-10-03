using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameForestMatch3.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameForestMatch3
{
    public class ChipRenderer : Renderer
    {
        public SpriteRenderer _mainItem { get; }
        public SpriteRenderer _modifyItem { get; }

        public Chip RenderChip { get; }

        public ChipRenderer(RenderCache renderCache, Chip chip) : base(renderCache)
        {
            RenderChip = chip;
            _mainItem = new SpriteRenderer(renderCache, "item" + (int)chip.Color)
            {
                SortingLayer = SortingLayer.GetLayer("items")
            };
        }

        protected override void OnRectChanged()
        {
            base.OnRectChanged();
            _mainItem.Rect = Rect;
            if (_modifyItem != null)
                _modifyItem.Rect = Rect;
        }

        protected override void OnScaleChanged()
        {
            base.OnScaleChanged();
            _mainItem.Scale = Scale;
            if (_modifyItem != null)
                _modifyItem.Scale = Scale;
        }

        protected override void OnPositionChanged()
        {
            base.OnPositionChanged();
            _mainItem.Position = Position;
            if (_modifyItem != null)
                _modifyItem.Position = Position;
        }

        protected override void OnSizeChanged()
        {
            base.OnSizeChanged();
            _mainItem.Size = Size;
            if (_modifyItem != null)
                _modifyItem.Size = Size;
        }

        protected override void OnColorChanged()
        {
            base.OnColorChanged();
            _mainItem.Color = Color;
            if (_modifyItem != null)
                _modifyItem.Color = Color;
        }

        protected override void Update(GameTime gameTime)
        {

        }



        protected override void Draw(GameTime gameTime)
        {
            RenderCache.Cache(_mainItem);
            if (_modifyItem != null)
                RenderCache.Cache(_mainItem);
        }

        protected override void Render(SpriteBatch spriteBatch)
        {

        }
    }
}
