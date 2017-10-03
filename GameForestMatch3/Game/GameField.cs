using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameForestMatch3.Core;
using GameForestMatch3.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameForestMatch3
{
    public class GameField : GameObject<Renderer>
    {
        private Vector2 _position;
        private Vector2 _step { get; }
        public int Width { get; }
        public int Height { get; }
        public Cell[,] Cells { get; }

        public GameField(RenderCache renderCache, int width, int height, Rectf rect) : base(renderCache)
        {
            Width = width;
            Height = height;
            _position = rect.Position;
            _step = new Vector2(rect.Width / width, rect.Height / height);
            Cells = new Cell[Width, Height];

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    AddComponent(new Sprite9SliceRenderer(renderCache, Resources.Get<Texture2D>("field"))
                    {
                        SortingLayer = SortingLayer.GetLayer("field"),
                        Position = GetPosition(x, y),
                        Size = _step,
                        CenterRect = new Rectf(32, 32, 64, 64),
                        Color = new Color(Color.White, 0.7f)
                    });

                    Cells[x, y] = new Cell();
                }
                FillColumn(x);
            }
        }

        private void FillColumn(int x)
        {
            var haveChips = 0;
            var needChips = 0;
            for (int y = Width - 1; y >= 0; y--)
            {
                var chip = Cells[x, y].CurrentChip;
                if (chip == null)
                {
                    needChips++;
                }
                else
                {
                    if (haveChips < y)
                    {
                        Cells[x, y].CurrentChip = null;
                        Cells[x, haveChips].CurrentChip = chip;
                        TweenFactory.Tween(chip, chip.Renderer.Position, GetPosition(x, Height - haveChips - 1), (y - haveChips) / 5f,
                            TweenScaleFunctions.QuadraticEaseIn, v =>
                             {
                                 chip.Renderer.Position = v.CurrentValue;
                             }, null);
                    }
                    haveChips++;
                }
            }
            for (int y = haveChips; y < haveChips + needChips; y++)
            {
                var chip = new Chip(this, RenderCache);
                Cells[x, haveChips].CurrentChip = chip;
                chip.Renderer.Position = GetPosition(x, -1 - y + haveChips);
                chip.Renderer.Size = _step;
                new FallEffect(chip.Renderer, _position.Y - _step.Y, _position.Y, GetPosition(x, Height - y - 1)).Play(() =>
                    {
                        new AfterFallEffect(chip.Renderer).Play();
                    });
                //TweenFactory.Tween(chip, GetPosition(x, -1 - y + haveChips), GetPosition(x, Height - y - 1), needChips/5f,
                //    TweenScaleFunctions.QuadraticEaseIn, v =>
                //    {
                //        chip.Renderer.Position = v.CurrentValue;
                //    }, e =>
                //    {
                //        new AfterFallEffect(chip.Renderer).Play();
                //    });
            }
        }

        private Vector2 GetPosition(int x, int y)
        {
            return _position + _step.Scale(x + 0.5f, y + 0.5f);
        }
    }
}
