using System;
using GameForestMatch3.Core;
using Microsoft.Xna.Framework;

namespace GameForestMatch3
{
    public class DisappearEffect : BaseEffect
    {
        private Renderer[] _objects;

        private float _scaleInDuration = 0.2f;

        private float _scaleOutDuration = 0.4f;

        private Vector2 _maxScale = new Vector2(1.2f, 1.2f);

        public DisappearEffect(params Renderer[] objects)
        {
            _objects = objects;
        }

        protected override void OnPlay(Action effectFinished)
        {
            new ScaleInOutEffectAction().Play(new ScaleInOutEffectActionSettings()
            {
                DurationIn = _scaleInDuration,
                DurationOut = _scaleOutDuration,
                TargetScale = _maxScale,
                FinalScale = Vector2.Zero,
                Items = _objects
            }, effectFinished);
        }
    }
}
