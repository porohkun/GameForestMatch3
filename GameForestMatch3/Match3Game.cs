using System;
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
            SortingLayer.RegisterLayer("gui_back", 1);
            SortingLayer.RegisterLayer("gui", 2);
            SortingLayer.RegisterLayer("fade", 99);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            PageManager.CreateInstance(_spriteBatch, this);
            PageManager.Push<StartPage>();
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            OnUpdate?.Invoke(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            OnDraw?.Invoke(gameTime);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
