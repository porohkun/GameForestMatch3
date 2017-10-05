using System;
using System.Linq;
using GameForestMatch3.Core;
using GameForestMatch3.Utils;
using Microsoft.Xna.Framework;

namespace GameForestMatch3
{
    public class NoSwapEffect : BaseEffect
    {
        private Renderer _object1;
        private Renderer _object2;

        private float _duration = 0.2f;

        public NoSwapEffect(Renderer obj1, Renderer obj2)
        {
            _object1 = obj1;
            _object2 = obj2;
        }

        protected override void OnPlay(Action effectFinished)
        {
            var pos1 = _object1.Position;
            var pos2 = _object2.Position;
            var pos1t = pos1 + (pos2 - pos1) / 3f;
            var pos2t = pos2 + (pos1 - pos2) / 3f;
            TweenFactory.Tween(_object1, pos1, pos1t, _duration / 2f, TweenScaleFunctions.SineEaseOut,
                p => _object1.Position = p.CurrentValue, p =>
                {
                    TweenFactory.Tween(_object1, pos1t, pos1, _duration / 2f, TweenScaleFunctions.SineEaseIn,
                        p1 => _object1.Position = p1.CurrentValue, null);
                });
            TweenFactory.Tween(_object2, pos2, pos2t, _duration / 2f, TweenScaleFunctions.SineEaseOut,
                p => _object2.Position = p.CurrentValue, p =>
                {
                    TweenFactory.Tween(_object2, pos2t, pos2, _duration / 2f, TweenScaleFunctions.SineEaseIn,
                        p1 => _object2.Position = p1.CurrentValue, p1 =>
                        {
                            effectFinished?.Invoke();
                        });
                });

        }
    }
}
