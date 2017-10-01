using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;

namespace GameForestMatch3.Core
{
    public static class Resources
    {
        private static ContentManager _content;
        private static Dictionary<string, object> _loaded;

        public static void SetContent(ContentManager content)
        {
            _content = content;
            _loaded = new Dictionary<string, object>();
        }

        public static T Get<T>(string assetName)
        {
            if (_loaded.ContainsKey(assetName))
                return (T)_loaded[assetName];
            var asset = _content.Load<T>(assetName);
            _loaded.Add(assetName, asset);
            return asset;
        }
    }
}
