using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameForestMatch3.Core;
using GameForestMatch3.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameForestMatch3.Pages
{
    public class GamePage : BasePage
    {
        private SpriteRenderer _faderenderer;
        private GameField _field;
        private float _timer = 60f;
        private bool _gameover;

        private TextRenderer _timerText;
        private TextRenderer _scoreText;
        private Button _gameoverButton;

        public GamePage(RenderCache renderCache, Point screenSize) : base(renderCache, screenSize)
        {
            var renderer1 = AddComponent(new SpriteRenderer(renderCache, "background") { SortingLayer = SortingLayer.GetLayer("background") });
            renderer1.Rect = _screenRect.EnvelopeInThis(renderer1.Texture.Width, renderer1.Texture.Height);

            _field = AddComponent(new GameField(renderCache, 8, 8, new Rectf(
                screenSize.X - screenSize.Y + 100,
                100,
                screenSize.Y - 200,
                screenSize.Y - 200)));

            AddComponent(new Sprite9SliceRenderer(renderCache, "field")
            {
                SortingLayer = SortingLayer.GetLayer("gui_back"),
                Rect = new Rectf(30f, 100f, 300f, 140f),
                CenterRect = new Rectf(32, 32, 64, 64),
                Color = new Color(Color.White, 0.5f)
            });

            _timerText = AddComponent(new TextRenderer(renderCache, "Timer: 60")
            {
                SortingLayer = SortingLayer.GetLayer("gui"),
                Rect = new Rectf(30f, 100f, 300f, 70f),
                OrderInLayer = 1,
                Font = Resources.Get<SpriteFont>("candara")
            });

            _scoreText = AddComponent(new TextRenderer(renderCache, "Score: 000")
            {
                SortingLayer = SortingLayer.GetLayer("gui"),
                Rect = new Rectf(30f, 170f, 300f, 70f),
                OrderInLayer = 1,
                Font = Resources.Get<SpriteFont>("candara")
            });

            _faderenderer = AddComponent(new SpriteRenderer(renderCache, Resources.Get<Texture2D>("rect"))
            {
                SortingLayer = SortingLayer.GetLayer("fade"),
                Rect = new Rectf(0, 0, screenSize.X, screenSize.Y),
                Color = Color.Black
            });

            TweenFactory.Tween(_faderenderer, 1f, 0f, 1f, TweenScaleFunctions.SineEaseOut,
                p => _faderenderer.Color = new Color(_faderenderer.Color, p.CurrentValue), null);
        }

        protected override void OnUpdate(GameTime gameTime)
        {
            if (_gameover) return;

            _timer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (_timer < 0f)
            {
                _timer = 0f;
                _field.Updatable = false;
                _gameover = true;

                AddComponent(new Sprite9SliceRenderer(RenderCache, "field")
                {
                    SortingLayer = SortingLayer.GetLayer("gui_back"),
                    Rect = new Rectf(
                        (int)((_screenRect.Width - 300) / 2f),
                        (int)((_screenRect.Height - 250) / 2f),
                        300,
                        250),
                    CenterRect = new Rectf(32, 32, 64, 64),
                    Color = new Color(Color.White, 0.75f)
                });

                 AddComponent(new TextRenderer(RenderCache, "Game Over")
                {
                    SortingLayer = SortingLayer.GetLayer("gui"),
                    Rect = new Rectf(
                        (int)((_screenRect.Width - 300) / 2f),
                        (int)(_screenRect.Height / 2f - 85),
                        300,
                        70),
                    OrderInLayer = 1,
                    Font = Resources.Get<SpriteFont>("candara")
                });

                _gameoverButton = AddComponent(new Button(RenderCache, "grey")
                {
                    Text = "OK",
                    Rect = new Rectf(
                        (int)((_screenRect.Width - 200) / 2f),
                        (int)(_screenRect.Height / 2f + 25),
                        200,
                        50)
                });
                _gameoverButton.Click += GameOverClick;

            }

            _scoreText.Text = "Score: " + _field.Score.ToString("000");
            _timerText.Text = "Timer: " + _timer.ToString("00");
        }

        void GameOverClick()
        {
            _gameoverButton.Interactable = false;
            TweenFactory.Tween(_faderenderer, 0f, 1f, 0.5f, TweenScaleFunctions.SineEaseOut,
                p => _faderenderer.Color = new Color(_faderenderer.Color, p.CurrentValue),
                p1 =>
                {
                    PageManager.Push<StartPage>();
                });
        }

    }
}
