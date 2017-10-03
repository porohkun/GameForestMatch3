using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameForestMatch3;

namespace GameForestMatch3
{
    public class Cell
    {
        public Chip CurrentChip;

        public bool Empty => CurrentChip == null;
    }
}
