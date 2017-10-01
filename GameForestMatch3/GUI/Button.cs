using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using GameForestMatch3.Core;
using Microsoft.Xna.Framework.Input;

namespace GameForestMatch3
{
    public class Button : GameObject<GameObject>
    {
        private Rectangle _rect;
        private string _font = "candara";
        private ButtonState _state = ButtonState.Normal;

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

        public ButtonState State
        {
            get => _state;
            set
            {
                if (_state == value) return;
                _state = value;
                _renderer.Texture = _textures[(int)value];
            }
        }

        public bool Interactable { get; set; } = true;

        public event Action Click;

        private Sprite9SliceRenderer _renderer;
        private TextRenderer _textRenderer;
        private Texture2D[] _textures;

        public Button(RenderCache renderCache, string visualStyle) : base(renderCache)
        {
            _textures = new[]
            {
                Resources.Get<Texture2D>(visualStyle + "-normal") ,
                Resources.Get<Texture2D>(visualStyle + "-focused") ,
                Resources.Get<Texture2D>(visualStyle + "-pressed")
            };
            _renderer = AddComponent(new Sprite9SliceRenderer(renderCache, _textures[0])
            {
                SortingLayer = SortingLayer.GetLayer("gui"),
                CenterRect = new Rectangle(16, 16, 32, 32)
            });
            _textRenderer = AddComponent(new TextRenderer(renderCache, "Button")
            {
                SortingLayer = SortingLayer.GetLayer("gui"),
                OrderInLayer = 1,
                Font = Resources.Get<SpriteFont>(_font)
            });
        }

        protected override void OnUpdate(GameTime gameTime)
        {
            if (!Interactable) return;
            var mouseState = Mouse.GetState();
            if (Rect.Contains(mouseState.X, mouseState.Y))
            {
                if (mouseState.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Released)
                {
                    if (State == ButtonState.Pressed)
                        Click?.Invoke();
                    State = ButtonState.Focused;
                }
                else
                    State = ButtonState.Pressed;
            }
            else
                State = ButtonState.Normal;
        }

        public enum ButtonState
        {
            Normal = 0,
            Focused = 1,
            Pressed = 2
        }
    }
}
