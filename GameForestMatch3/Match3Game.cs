using System;
using System.Collections.Generic;
using System.Linq;
using GameForestMatch3.Core;
using GameForestMatch3.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameForestMatch3
{
    public class Match3Game : Game, IGame
    {
        GraphicsDeviceManager _graphics;
        SpriteBatch _spriteBatch;
        RenderCache _renderCache;
        SpriteFont font;
        TPS tps = new TPS();

        public event Action<GameTime> OnUpdate;
        public event Action<GameTime> OnDraw;
        public Point ScreenSize => new Point(GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);

        public Match3Game()
        {
            _graphics = new GraphicsDeviceManager(this)
            {
                PreferredBackBufferWidth =
                    MathHelper.Min(1280, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width),
                PreferredBackBufferHeight =
                    MathHelper.Min(800, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height)
            };
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            Resources.SetContent(Content);
            Coroutine.BindToGame(this);
            TweenFactory.Initialize(this);

            SortingLayer.RegisterLayer("background", 0);
            SortingLayer.RegisterLayer("field", 1);
            SortingLayer.RegisterLayer("items", 2);
            SortingLayer.RegisterLayer("effects", 3);
            SortingLayer.RegisterLayer("gui_back", 4);
            SortingLayer.RegisterLayer("gui", 5);
            SortingLayer.RegisterLayer("fade", 99);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _renderCache = new RenderCache();

            PageManager.CreateInstance(_renderCache, this);
            PageManager.Push<StartPage>();

            font = Resources.Get<SpriteFont>("candara");

        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            OnUpdate?.Invoke(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            OnDraw?.Invoke(gameTime);

            _renderCache.RenderCached(_spriteBatch);

            tps.Tick(gameTime.TotalGameTime);
            _spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);
            _spriteBatch.DrawString(font, "FPS:" + tps, new Vector2(10, 10), Color.Black);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
