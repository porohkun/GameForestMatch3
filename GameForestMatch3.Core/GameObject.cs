using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace GameForestMatch3.Core
{
    public abstract class GameObject
    {
        protected RenderCache RenderCache;

        public bool Enabled { get; set; }

        public GameObject(RenderCache renderCache)
        {
            RenderCache = renderCache;
            Enabled = true;
        }
        
        protected internal abstract void Update(GameTime gameTime);
        protected internal abstract void Draw(GameTime gameTime);
    }

 
}
