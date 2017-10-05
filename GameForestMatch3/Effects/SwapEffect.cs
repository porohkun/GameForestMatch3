using System;
using System.Linq;
using GameForestMatch3.Core;
using GameForestMatch3.Utils;
using Microsoft.Xna.Framework;

namespace GameForestMatch3
{
    public class SwapEffect : BaseEffect
    {
        private Renderer _object1;
        private Renderer _object2;

        private float _duration = 0.3f;
        
        public SwapEffect(Renderer obj1, Renderer obj2)
        {
            _object1 = obj1;
            _object2 = obj2;
        }

        protected override void OnPlay(Action effectFinished)
        {
            var pos1 = _object1.Position;
            var pos2 = _object2.Position;
            TweenFactory.Tween(_object1, pos1, pos2, _duration, TweenScaleFunctions.SineEaseIn,
                p => _object1.Position = p.CurrentValue, null);
            TweenFactory.Tween(_object2, pos2, pos1, _duration, TweenScaleFunctions.SineEaseIn,
                p => _object2.Position = p.CurrentValue, p =>
                {
                    effectFinished?.Invoke();
                });
            
        }
    }
}
