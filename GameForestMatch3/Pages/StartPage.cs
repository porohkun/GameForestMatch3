using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using GameForestMatch3.Core;
using GameForestMatch3.Utils;
using Microsoft.Xna.Framework.Graphics;

namespace GameForestMatch3
{
    public class StartPage : BasePage
    {
        private SpriteRenderer _faderenderer;

        public StartPage(SpriteBatch spriteBatch, Point screenSize) : base(spriteBatch, screenSize)
        {
            var renderer1 = AddComponent(new SpriteRenderer(spriteBatch, "background") { SortingLayer = SortingLayer.GetLayer("background") });
            renderer1.Rect = _screenRect.EnvelopeInThis(renderer1.Texture.Width, renderer1.Texture.Height);

            AddComponent(new SpriteRenderer(spriteBatch, "field")
            {
                SortingLayer = SortingLayer.GetLayer("gui_back"),
                Rect = new Rectangle()
                {
                    X = (int)((screenSize.X - 300) / 2f),
                    Y = (int)((screenSize.Y - 250) / 2f),
                    Width = 300,
                    Height = 250
                },
                Color = new Color(Color.White, 0.75f)
            });

            var button1 = AddComponent(new Button(spriteBatch, "grey")
            {
                Text = "Start",
                Rect = new Rectangle()
                {
                    X = (int)((screenSize.X - 200) / 2f),
                    Y = (int)(screenSize.Y / 2f - 75),
                    Width = 200,
                    Height = 50
                },
                Interactable = false
            });
            button1.Click += StartButtonClick;

            var button2 = AddComponent(new Button(spriteBatch, "grey")
            {
                Text = "Exit",
                Rect = new Rectangle()
                {
                    X = (int)((screenSize.X - 200) / 2f),
                    Y = (int)(screenSize.Y / 2f + 25),
                    Width = 200,
                    Height = 50
                },
                Interactable = false
            });
            button2.Click += ExitButtonClick;

            _faderenderer = AddComponent(new SpriteRenderer(spriteBatch, Resources.Get<Texture2D>("rect"))
            {
                SortingLayer = SortingLayer.GetLayer("fade"),
                Rect = new Rectangle()
                {
                    X = 0,
                    Y = 0,
                    Width = screenSize.X,
                    Height = screenSize.Y
                },
                Color = Color.Black
            });

            TweenFactory.Tween(_faderenderer, 1f, 0f, 1f, TweenScaleFunctions.Linear,
                p=> _faderenderer.Color = new Color(_faderenderer.Color, p.CurrentValue),
                p1 =>
                {
                    button1.Interactable = true;
                    button2.Interactable = true;
                });
        }
        
        void StartButtonClick()
        {
            TweenFactory.Tween(_faderenderer, 0f, 1f, 0.5f, TweenScaleFunctions.Linear,
                p => _faderenderer.Color = new Color(_faderenderer.Color, p.CurrentValue),
                p1 =>
                {
                    TweenFactory.Tween(_faderenderer, 1f, 0f, 0.5f, TweenScaleFunctions.Linear,
                        p2 => _faderenderer.Color = new Color(_faderenderer.Color, p2.CurrentValue),
                        p3 =>
                        {
                            
                        });
                });
        }

        void ExitButtonClick()
        {
            Program.Close();
        }
    }
}
