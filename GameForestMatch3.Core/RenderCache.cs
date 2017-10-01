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
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
            foreach (var rend in _renderers.OrderBy(r => r.SortingLayer?.GetDepth(r.OrderInLayer) ?? 0f))
            {
                rend.Shader.CurrentTechnique.Passes[0].Apply();
                rend.Render(spriteBatch);
            }
            spriteBatch.End();

            _renderers.Clear();
        }
    }
}
