using System;
using System.Linq;
using GameForestMatch3.Core;
using Microsoft.Xna.Framework;

namespace GameForestMatch3
{
    public class SelectEffect : BaseEffect
    {
        private Renderer _object;

        private float _duration = 0.1f;
        
        private Vector2 _maxScale = new Vector2(1.2f, 1.2f);

        private float _maxGlow = 2f;

        public SelectEffect(Renderer obj)
        {
            _object = obj;
        }

        protected override void OnPlay(Action effectFinished)
        {
            new ScaleInOutEffectAction().Play(new ScaleInOutEffectActionSettings()
            {
                DurationIn = _duration,
                DurationOut = -1,
                TargetScale = _maxScale,
                FinalScale = _maxScale,
                Items = _object.SingleItemAsEnumerable().ToArray()
            });
            new GlowInOutEffectAction().Play(new GlowInOutEffectActionSettings()
            {
                DurationIn = _duration,
                DurationOut = -1,
                TargetGlow = _maxGlow,
                FinalGlow = _maxGlow,
                Items = _object.SingleItemAsEnumerable().ToArray()
            }, effectFinished);
        }
    }
}
