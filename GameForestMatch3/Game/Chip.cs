using GameForestMatch3.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameForestMatch3
{
    public class Chip
    {
        private static Random _rnd = new Random();

        public ChipColor Color { get; }
        public ChipRenderer Renderer { get; }

        public Chip(GameObject<Renderer> parent, RenderCache cache) : this(parent, cache, (ChipColor)_rnd.Next(0, 5)) { }

        public Chip(GameObject<Renderer> parent, RenderCache cache, ChipColor color)
        {
            Color = color;
            Renderer = parent.AddComponent(new ChipRenderer(cache, this));
        }
    }

    public enum ChipColor
    {
        Magenta = 0,
        Cyan = 1,
        Red = 2,
        Blue = 3,
        Green = 4
    }
}
