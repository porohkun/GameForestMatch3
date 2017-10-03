using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace GameForestMatch3.Core
{
    public static class VectorExtensions
    {
        public static Vector2 Scale(this Vector2 vec1, Vector2 vec2)
        {
            return new Vector2(vec1.X * vec2.X, vec1.Y * vec2.Y);
        }

        public static Vector2 Scale(this Vector2 vec1, float x, float y)
        {
            return new Vector2(vec1.X * x, vec1.Y * y);
        }
    }
}
