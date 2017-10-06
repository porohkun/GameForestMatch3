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
        public ChipBonus Bonus { get; } 
        public ChipRenderer Renderer { get; }
        public bool Falling { get; set; }

        public Chip(GameObject<GameObject> parent, RenderCache cache) : this(parent, cache, (ChipColor)_rnd.Next(0, 5)) { }
        
        public Chip(GameObject<GameObject> parent, RenderCache cache, ChipColor color, ChipBonus bonus= ChipBonus.None)
        {
            Color = color;
            Bonus = bonus;
            Renderer = parent.AddComponent(new ChipRenderer(cache, this, bonus));
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

    public enum ChipBonus
    {
        None = 0,
        HorLine = 1,
        VerLine = 2,
        Bomb = 3
    }
}
