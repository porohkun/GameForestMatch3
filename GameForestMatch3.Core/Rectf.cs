using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace GameForestMatch3.Core
{
    public struct Rectf
    {
        public static Rectf Empty { get; } = new Rectf(0, 0, 0, 0);

        public float X;
        public float Y;
        public float Width;
        public float Height;

        public Vector2 Position => new Vector2(X, Y);
        public Vector2 Size => new Vector2(Width, Height);

        public float Top => Y;
        public float Bottom => Y + Height;
        public float Left => X;
        public float Right => X + Width;

        public Rectf(float x, float y, float width, float height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        public Rectf(Vector2 position, Vector2 size)
        {
            X = position.X;
            Y = position.Y;
            Width = size.X;
            Height = size.Y;
        }

        public Rectf(Point position, Point size) : this(position.X, position.Y, size.X, size.Y) { }

        public static bool operator ==(Rectf rect1, Rectf rect2)
        {
            return Mathf.Approximately(rect1.X, rect2.X) &&
                   Mathf.Approximately(rect1.Y, rect2.Y) &&
                   Mathf.Approximately(rect1.Width, rect2.Width) &&
                   Mathf.Approximately(rect1.Height, rect2.Height);
        }

        public static bool operator !=(Rectf rect1, Rectf rect2)
        {
            return !Mathf.Approximately(rect1.X, rect2.X) ||
                   !Mathf.Approximately(rect1.Y, rect2.Y) ||
                   !Mathf.Approximately(rect1.Width, rect2.Width) ||
                   !Mathf.Approximately(rect1.Height, rect2.Height);
        }

        public static implicit operator Rectf(Rectangle rect)
        {
            return new Rectf(rect.X, rect.Y, rect.Width, rect.Height);
        }

        public static explicit operator Rectangle(Rectf rect)
        {
            return new Rectangle(Mathf.FloorToInt(rect.X), Mathf.FloorToInt(rect.Y), Mathf.FloorToInt(rect.Width), Mathf.FloorToInt(rect.Height));
        }

        public bool Contains(Vector2 pos)
        {
            return Contains(pos.X, pos.Y);
        }

        public bool Contains(float x, float y)
        {
            return Left <= x && x <= Right && Top <= y && y <= Bottom;
        }

        public Vector2 ScaleFrom(Rectf origin)
        {
            return new Vector2(Width / origin.Width, Height / origin.Height);
        }

        public Rectf FitInThis(float width, float height)
        {
            var parentRate = Width / Height;
            var targetRate = width / height;

            if (Mathf.Approximately(parentRate, targetRate))
                return this;
            if (parentRate > targetRate) //parent more wide
            {
                var h = Height;
                var w = targetRate * h;
                var x = X + (Width - w) / 2f;
                var y = Y;
                return new Rectf(x, y, w, h);
            }
            else
            {
                var w = Width;
                var h = w / targetRate;
                var x = X;
                var y = Y + (Height - h) / 2f;
                return new Rectf(x, y, w, h);
            }
        }

        public Rectf FitInThis(Vector2 size)
        {
            return FitInThis(size.X, size.Y);
        }

        public Rectf EnvelopeInThis(float width, float height)
        {
            var parentRate = Width / Height;
            var targetRate = width / height;

            if (Mathf.Approximately(parentRate, targetRate))
                return this;
            if (parentRate > targetRate) //parent more wide
            {
                var w = Width;
                var h = w / targetRate;
                var x = X;
                var y = Y + (Height - h) / 2f;
                return new Rectf(x, y, w, h);
            }
            else
            {
                var h = Height;
                var w = targetRate * h;
                var x = X + (Width - w) / 2f;
                var y = Y;
                return new Rectf(x, y, w, h);
            }
        }

        public Rectf EnvelopeInThis(Point size)
        {
            return EnvelopeInThis(size.X, size.Y);
        }

        public Rectf Expand(float radius)
        {
            return new Rectf(
                X - radius,
                Y - radius,
                Width + radius * 2,
                Height + radius * 2);
        }
    }
}
