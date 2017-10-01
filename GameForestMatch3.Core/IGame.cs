using System;
using Microsoft.Xna.Framework;

namespace GameForestMatch3.Core
{
    public interface IGame
    {
        event Action<GameTime> OnUpdate;
        event Action<GameTime> OnDraw;
        Point ScreenSize { get; }
    }
}