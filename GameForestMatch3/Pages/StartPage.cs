using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using GameForestMatch3.Core;
using GameForestMatch3.Pages;
using GameForestMatch3.Utils;
using Microsoft.Xna.Framework.Graphics;

namespace GameForestMatch3
{
    public class StartPage : BasePage
    {
        private Button _startButton;
        private Button _exitButton;
        private SpriteRenderer _faderenderer;

        public StartPage(RenderCache renderCache, Point screenSize) : base(renderCache, screenSize)
        {
            var renderer1 = AddComponent(new SpriteRenderer(renderCache, "background") { SortingLayer = SortingLayer.GetLayer("background") });
            renderer1.Rect = _screenRect.EnvelopeInThis(renderer1.Texture.Width, renderer1.Texture.Height);

            AddComponent(new Sprite9SliceRenderer(renderCache, "field")
            {
                SortingLayer = SortingLayer.GetLayer("gui_back"),
                Rect = new Rectf(
                    (int)((screenSize.X - 300) / 2f),
                    (int)((screenSize.Y - 250) / 2f),
                    300,
                    250),
                CenterRect = new Rectf(32, 32, 64, 64),
                Color = new Color(Color.White, 0.75f)
            });

            _startButton = AddComponent(new Button(renderCache, "grey")
            {
                Text = "Start",
                Rect = new Rectf(
                    (int)((screenSize.X - 200) / 2f),
                    (int)(screenSize.Y / 2f - 75),
                    200,
                    50),
                Interactable = false
            });
            _startButton.Click += StartButtonClick;

            _exitButton = AddComponent(new Button(renderCache, "grey")
            {
                Text = "Exit",
                Rect = new Rectf(
                    (int)((screenSize.X - 200) / 2f),
                    (int)(screenSize.Y / 2f + 25),
                    200,
                    50),
                Interactable = false
            });
            _exitButton.Click += ExitButtonClick;
            
            _faderenderer = AddComponent(new SpriteRenderer(renderCache, Resources.Get<Texture2D>("rect"))
            {
                SortingLayer = SortingLayer.GetLayer("fade"),
                Rect = new Rectf(
                    0,
                    0,
                    screenSize.X,
                    screenSize.Y),
                Color = Color.Black
            });

            TweenFactory.Tween(_faderenderer, 1f, 0f, 1f, TweenScaleFunctions.SineEaseIn,
                p => _faderenderer.Color = new Color(_faderenderer.Color, p.CurrentValue),
                p1 =>
                {
                    _startButton.Interactable = true;
                    _exitButton.Interactable = true;
                });
        }

        void StartButtonClick()
        {
            _startButton.Interactable = false;
            _exitButton.Interactable = false;
            TweenFactory.Tween(_faderenderer, 0f, 1f, 0.5f, TweenScaleFunctions.SineEaseOut,
                p => _faderenderer.Color = new Color(_faderenderer.Color, p.CurrentValue),
                p1 =>
                {
                    PageManager.Push<GamePage>();
                });
        }

        void ExitButtonClick()
        {
            Program.Close();
        }
    }
}
