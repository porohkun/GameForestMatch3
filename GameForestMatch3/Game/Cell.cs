using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameForestMatch3;
using Microsoft.Xna.Framework;

namespace GameForestMatch3
{
    public class Cell
    {
        private bool _ready;
        public Chip CurrentChip;
        public Vector2 Position { get; }

        public bool Empty => CurrentChip == null;

        public bool Ready
        {
            get => _ready && !Empty;
            set => _ready = value;
        }

        //public bool Dirty { get; set; }

        public Cell(Vector2 position)
        {
            Position = position;
        }
    }
}
