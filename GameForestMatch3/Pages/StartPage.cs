using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using GameForestMatch3.Core;
using Microsoft.Xna.Framework.Graphics;

namespace GameForestMatch3
{
    public class StartPage : BasePage
    {
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
                }
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
                }
            });
            button2.Click += ExitButtonClick;
        }

        void StartButtonClick()
        {

        }

        void ExitButtonClick()
        {
            Program.Close();
        }
    }
}
