using System;
using GameForestMatch3.Core;
using GameForestMatch3.Utils;
using Microsoft.Xna.Framework;

namespace GameForestMatch3
{
    public class FallEffect : BaseEffect
    {
        private Renderer _object;
        private Vector2 _end;
        private float _alpha0Line;
        private float _alpha1Line;

        private float _fallSpeed = 100f;


        public FallEffect(Renderer obj, float alpha0Line, float alpha1Line, Vector2 target)
        {
            _object = obj;
            _end = target;
            _alpha0Line = alpha0Line;
            _alpha1Line = alpha1Line;
        }

        protected override void OnPlay(Action effectFinished)
        {
            var dist = Vector2.Distance(_end, _object.Position);
            var time = dist / (_fallSpeed * _rnd.Next(980, 1020) / 1000);
            TweenFactory.Tween(_rnd.Next(), _object.Position, _end, time,
                TweenScaleFunctions.QuadraticEaseIn, v =>
                {
                    _object.Position = v.CurrentValue;
                    if (v.CurrentValue.Y < _alpha0Line)
                        _object.Color = new Color(1f, 1f, 1f, 0f);
                    else if (v.CurrentValue.Y > _alpha1Line)
                        _object.Color = new Color(1f, 1f, 1f, 1f);
                    else
                        _object.Color = new Color(1f, 1f, 1f, Mathf.Lerp(0f, 1f, (v.CurrentValue.Y - _alpha0Line) / (_alpha1Line - _alpha0Line)));
                }, e => effectFinished?.Invoke());
        }
    }
}
