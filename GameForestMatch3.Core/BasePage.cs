﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameForestMatch3.Core
{
    public abstract class BasePage : GameObject<GameObject>
    {
        protected Rectangle _screenRect;

        public BasePage(RenderCache renderCache, Point screenSize):base(renderCache)
        {
            _screenRect = new Rectangle(Point.Zero, screenSize);
        }
    }
}
