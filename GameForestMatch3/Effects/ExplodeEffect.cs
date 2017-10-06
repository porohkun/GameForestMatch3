using System;
using System.Linq;
using GameForestMatch3.Core;
using GameForestMatch3.Utils;
using Microsoft.Xna.Framework;

namespace GameForestMatch3
{
    public class ExplodeEffect : BaseEffect
    {
        private Renderer _object;

        private float _duration = 0.3f;
        private Color _startColor = Color.White;
        private Color _midColor = new Color(Color.Yellow, 0.7f);
        private Color _endColor = new Color(Color.Red, 0f);

        public ExplodeEffect(Renderer obj)
        {
            _object = obj;
        }

        protected override void OnPlay(Action effectFinished)
        {
            TweenFactory.Tween(_object, _startColor, _midColor, _duration / 2f, TweenScaleFunctions.SineEaseIn,
                p1 => _object.Color = p1.CurrentValue,
                p1 =>
                {
                    TweenFactory.Tween(_object, _midColor, _endColor, _duration / 2f, TweenScaleFunctions.SineEaseOut,
                        p2 => _object.Color = p2.CurrentValue, null);
                });
            TweenFactory.Tween(_object.Rect, _object.Scale, _object.Scale * 6f, _duration, TweenScaleFunctions.SineEaseOut,
                p1 => _object.Scale = p1.CurrentValue,
                p1 => effectFinished?.Invoke());

        }
    }
}
