using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace GameForestMatch3.Core
{
    public static class RectHelper
    {
        public static Rectangle FitInThis(this Rectangle parent, int width, int height)
        {
            var parentRate = (float)parent.Width / parent.Height;
            var targetRate = (float)width / height;

            if (Mathf.Approximately(parentRate, targetRate))
                return parent;
            if (parentRate > targetRate) //parent more wide
            {
                var h = parent.Height;
                var w = Mathf.RoundToInt(targetRate * h);
                var x = parent.X + (parent.Width - w) / 2;
                var y = parent.Y;
                return new Rectangle(x, y, w, h);
            }
            else
            {
                var w = parent.Width;
                var h = Mathf.RoundToInt(w / targetRate);
                var x = parent.X;
                var y = parent.Y + (parent.Height - h) / 2;
                return new Rectangle(x, y, w, h);
            }
        }

        public static Rectangle FitInThis(this Rectangle parent, Point size)
        {
            return parent.FitInThis(size.X, size.Y);
        }

        public static Rectangle EnvelopeInThis(this Rectangle parent, int width, int height)
        {
            var parentRate = (float)parent.Width / parent.Height;
            var targetRate = (float)width / height;

            if (Mathf.Approximately(parentRate, targetRate))
                return parent;
            if (parentRate > targetRate) //parent more wide
            {
                var w = parent.Width;
                var h = Mathf.RoundToInt(w / targetRate);
                var x = parent.X;
                var y = parent.Y + (parent.Height - h) / 2;
                return new Rectangle(x, y, w, h);
            }
            else
            {
                var h = parent.Height;
                var w = Mathf.RoundToInt(targetRate * h);
                var x = parent.X + (parent.Width - w) / 2;
                var y = parent.Y;
                return new Rectangle(x, y, w, h);
            }
        }

        public static Rectangle EnvelopeInThis(this Rectangle parent, Point size)
        {
            return parent.EnvelopeInThis(size.X, size.Y);
        }

        public static Rectangle Expand(this Rectangle rect, int radius)
        {
            return new Rectangle()
            {
                X = rect.X - radius,
                Y = rect.Y - radius,
                Width = rect.Width + radius * 2,
                Height = rect.Height + radius * 2
            };
        }
    }
}
