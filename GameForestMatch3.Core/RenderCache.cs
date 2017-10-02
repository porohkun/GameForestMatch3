using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameForestMatch3.Core
{
    public class RenderCache
    {
        private List<Renderer> _renderers = new List<Renderer>();

        public void Cache(Renderer renderer)
        {
            if (renderer.Enabled)
                _renderers.Add(renderer);
        }

        public void RenderCached(SpriteBatch spriteBatch)
        {
            Effect shader = null;
            foreach (var rend in _renderers.OrderBy(r => r.LayerDepth))
            {
                if (rend.Shader != shader)
                {
                    if (shader != null)
                        spriteBatch.End();
                    shader = rend.Shader;
                    spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, effect: shader);
                }
                rend.Render(spriteBatch);
            }
            spriteBatch.End();

            _renderers.Clear();
        }
    }
}
