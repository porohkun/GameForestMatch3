﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using GameForestMatch3.Core;

namespace GameForestMatch3
{
    public class Button : GameObject<GameObject>
    {
        private Rectangle _rect;
        private string _font = "candara";

        public Rectangle Rect
        {
            get => _rect;
            set
            {
                _rect = value;
                _renderer.Rect = value;
                _textRenderer.Rect = value.Expand(-10);
            }
        }

        public string Text
        {
            get => _textRenderer.Text;
            set => _textRenderer.Text = value;
        }

        public string Font
        {
            get => _font;
            set
            {
                _font = value;
                _textRenderer.Font = Resources.Get<SpriteFont>(value);
            }
        }

        public ButtonState State { get; private set; }

        public event Action Click;

        private Sprite9SliceRenderer _renderer;
        private TextRenderer _textRenderer;
        private Texture2D[] _sprites;

        public Button(SpriteBatch spriteBatch, string visualStyle) : base(spriteBatch)
        {
            _sprites = new[]
            {
                Resources.Get<Texture2D>(visualStyle + "-normal") ,
                Resources.Get<Texture2D>(visualStyle + "-focused") ,
                Resources.Get<Texture2D>(visualStyle + "-pressed")
            };
            _renderer = AddComponent(new Sprite9SliceRenderer(spriteBatch, _sprites[0])
            {
                SortingLayer = SortingLayer.GetLayer("gui"),
                CenterRect = new Rectangle(16, 16, 32, 32)
            });
            _textRenderer = AddComponent(new TextRenderer(spriteBatch, "Button")
            {
                SortingLayer = SortingLayer.GetLayer("gui"),
                OrderInLayer = 1,
                Font = Resources.Get<SpriteFont>(_font)
            });
        }

        public enum ButtonState
        {
            Normal = 0,
            Focused = 1,
            Pressed = 2
        }
    }
}