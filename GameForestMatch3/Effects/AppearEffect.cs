using System;
using GameForestMatch3.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameForestMatch3
{
    public class AppearEffect : BaseEffect
    {
        private Renderer[] _objects;

        public float _rotationDuration = 0.5f;

        public float _rotationStartAngle = -45;

        public float _glowDuration = 0.5f;

        public float _glowStart = 5f;

        public float _scaleDuration = 0.7f;

        public float _scaleStart = 1.3f;

        public AppearEffect(params Renderer[] objects)
        {
            _objects = objects;
        }

        protected override void OnPlay(Action effectFinished)
        {
            new GlowInOutEffectAction().Play(new GlowInOutEffectActionSettings()
            {
                DurationIn = _glowDuration,
                DurationOut = -1,
                StartGlow = _glowStart,
                TargetGlow = 1f,
                FinalGlow = 1f,
                FinalShader = Resources.Get<Effect>("default"),
                Items = _objects
            });
            new ScaleInOutEffectAction().Play(new ScaleInOutEffectActionSettings()
            {
                StartScale = Vector2.One / 2f,
                DurationIn = _scaleDuration / 5f,
                TargetScale = new Vector2(_scaleStart, _scaleStart),
                DurationOut = _scaleDuration / 5f * 4f,
                FinalScale = Vector2.One,
                Items = _objects

            }, () =>
            {
                effectFinished?.Invoke();
            });
        }
    }
}
