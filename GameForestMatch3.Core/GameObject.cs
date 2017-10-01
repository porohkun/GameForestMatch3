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
        protected SpriteBatch _spriteBatch;

        public bool Enabled { get; set; }

        public GameObject(SpriteBatch spriteBatch)
        {
            _spriteBatch = spriteBatch;
            Enabled = true;
        }
        
        protected internal abstract void Update(GameTime gameTime);
        protected internal abstract void Draw(GameTime gameTime);
    }

 
}
