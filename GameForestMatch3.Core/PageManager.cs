using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameForestMatch3.Core
{
    public class PageManager : GameObject<BasePage>
    {
        public static PageManager Instance { get; private set; }

        public static PageManager CreateInstance(RenderCache spriteBatch, IGame game)
        {
            Instance = new PageManager(spriteBatch, game);
            return Instance;
        }

        private Point _screenSize;

        public PageManager(RenderCache renderCache, IGame game) : base(renderCache)
        {
            _screenSize = game.ScreenSize;
            game.OnUpdate += Update;
            game.OnDraw += Draw;
        }

        public static T Push<T>() where T : BasePage
        {
            return Instance.PushInternal<T>();
        }

        public static T Pop<T>() where T : BasePage
        {
            return Instance.PopInternal<T>();
        }

        private T PushInternal<T>() where T : BasePage
        {
            var type = typeof(T);
            var page = PopInternal<T>() ?? CreatePage<T>();
            Add(page);
            SwitchPagesActivity();
            return page;
        }

        private T PopInternal<T>() where T : BasePage
        {
            var type = typeof(T);
            var page = this.FirstOrDefault(s => s.GetType() == type);
            if (page != null)
                Remove(page);
            SwitchPagesActivity();
            return page as T;
        }

        private T CreatePage<T>() where T : BasePage
        {
            var type = typeof(T);
            var constructor = type.GetConstructor(new[] { typeof(RenderCache), typeof(Point) });
            var page = constructor?.Invoke(new object[] { RenderCache, _screenSize }) as T;
            if (page == null)
                throw new ArgumentNullException();
            return page;
        }

        private void SwitchPagesActivity()
        {
            for (int i = 0; i < Count; i++)
                this[i].Enabled = i == Count - 1;
        }

    }
}
