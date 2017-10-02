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
        
        public GamePage(RenderCache renderCache, Point screenSize) : base(renderCache, screenSize)
        {
            var renderer1 = AddComponent(new SpriteRenderer(renderCache, "background") { SortingLayer = SortingLayer.GetLayer("background") });
            renderer1.Rect = _screenRect.EnvelopeInThis(renderer1.Texture.Width, renderer1.Texture.Height);

            AddComponent(new SpriteRenderer(renderCache, Resources.Get<Texture2D>("item1"))
            {
                SortingLayer = SortingLayer.GetLayer("gui"),
                Rect = new Rectangle()
                {
                    X = 300,
                    Y = 300,
                    Width = 128,
                    Height = 128
                }
            });

            AddComponent(new SpriteRenderer(renderCache, Resources.Get<Texture2D>("item1"))
            {
                SortingLayer = SortingLayer.GetLayer("gui"),
                OrderInLayer = -1,
                Rect = new Rectangle()
                {
                    X = 350,
                    Y = 300,
                    Width = 128,
                    Height = 128
                },
                Shader = Resources.Get<Effect>("glow")
            });

            _faderenderer = AddComponent(new SpriteRenderer(renderCache, Resources.Get<Texture2D>("rect"))
            {
                SortingLayer = SortingLayer.GetLayer("fade"),
                Rect = new Rectf(0, 0, screenSize.X, screenSize.Y),
                Color = Color.Black
            });

            TweenFactory.Tween(_faderenderer, 1f, 0f, 1f, TweenScaleFunctions.SineEaseOut,
                p => _faderenderer.Color = new Color(_faderenderer.Color, p.CurrentValue),
                p1 =>
                {
                    
                });
            Resources.Get<Effect>("glow").Parameters["param1"].SetValue(2f);
        }
    }
}
