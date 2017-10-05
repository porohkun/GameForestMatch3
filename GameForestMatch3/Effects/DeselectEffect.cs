using System;
using System.Linq;
using GameForestMatch3.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameForestMatch3
{
    public class DeselectEffect : BaseEffect
    {
        private Renderer _object;

        private float _duration = 0.1f;

        private Vector2 _maxScale = new Vector2(1.2f, 1.2f);

        private float _maxGlow = 2f;

        public DeselectEffect(Renderer obj)
        {
            _object = obj;
        }

        protected override void OnPlay(Action effectFinished)
        {
            new ScaleInOutEffectAction().Play(new ScaleInOutEffectActionSettings()
            {
                DurationIn = _duration,
                DurationOut = -1,
                StartScale = Vector2.One,
                TargetScale = Vector2.One/ _maxScale,
                FinalScale = Vector2.One/ _maxScale,
                Items = _object.SingleItemAsEnumerable().ToArray()
            });
            new GlowInOutEffectAction().Play(new GlowInOutEffectActionSettings()
            {
                DurationIn = _duration,
                DurationOut = -1,
                StartGlow = _maxGlow,
                TargetGlow = 1f,
                Items = _object.SingleItemAsEnumerable().ToArray(),
                FinalShader = Resources.Get<Effect>("default")
            }, effectFinished);
        }
    }
}
