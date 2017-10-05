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
        private int index;
        public int Index
        {
            get => index; set
            {
                index = value;
            }
        }
        public SpriteRenderer _mainItem { get; }
        public SpriteRenderer _modifyItem { get; }

        public Chip RenderChip { get; }

        public ChipRenderer(RenderCache renderCache, Chip chip, ChipBonus bonus) : base(renderCache)
        {
            RenderChip = chip;
            _mainItem = new SpriteRenderer(renderCache, "item" + (int)chip.Color)
            {
                SortingLayer = SortingLayer.GetLayer("items"),
            };
            if (bonus != ChipBonus.Zero && bonus != ChipBonus.None)
                _modifyItem = new SpriteRenderer(renderCache, bonus == ChipBonus.Bomb ? "bomb2" : (bonus == ChipBonus.HorLine ? "hor-line2" : "ver-line2"))
                {
                    SortingLayer = SortingLayer.GetLayer("items"),
                    OrderInLayer = 1
                };
            Rect = _mainItem.Rect;
            Scale = _mainItem.Scale;
            Size = _mainItem.Size;
        }

        protected override void OnRectChanged()
        {
            base.OnRectChanged();
            _mainItem.Rect = Rect;
            if (_modifyItem != null)
                _modifyItem.Rect = Rect;
            _scale = _mainItem.Scale;
            _position = _mainItem.Position;
            _size = _mainItem.Size;
        }

        protected override void OnScaleChanged()
        {
            base.OnScaleChanged();
            _mainItem.Scale = Scale;
            if (_modifyItem != null)
                _modifyItem.Scale = Scale;
            _rect = _mainItem.Rect;
            _size = _mainItem.Size;
        }

        protected override void OnPositionChanged()
        {
            base.OnPositionChanged();
            _mainItem.Position = Position;
            if (_modifyItem != null)
                _modifyItem.Position = Position;
            _rect = _mainItem.Rect;
        }

        protected override void OnSizeChanged()
        {
            base.OnSizeChanged();
            _mainItem.Size = Size;
            if (_modifyItem != null)
                _modifyItem.Size = Size;
            _rect = _mainItem.Rect;
            _scale = _mainItem.Scale;
        }

        protected override void OnColorChanged()
        {
            base.OnColorChanged();
            _mainItem.Color = Color;
            if (_modifyItem != null)
                _modifyItem.Color = Color;
        }

        protected override void OnShaderChanged()
        {
            base.OnShaderChanged();
            _mainItem.Shader = Shader;
            if (_modifyItem != null)
                _modifyItem.Shader = Shader;
        }

        protected override void OnOrderChanged()
        {
            base.OnOrderChanged();
            _mainItem.SortingLayer = SortingLayer;
            _mainItem.OrderInLayer = OrderInLayer;
            if (_modifyItem != null)
            {
                _modifyItem.SortingLayer = SortingLayer;
                _modifyItem.OrderInLayer = OrderInLayer;
            }
        }

        protected override void Update(GameTime gameTime)
        {

        }



        protected override void Draw(GameTime gameTime)
        {
            RenderCache.Cache(_mainItem);
            if (_modifyItem != null)
                RenderCache.Cache(_modifyItem);
        }

        protected override void Render(SpriteBatch spriteBatch)
        {

        }
    }
}
