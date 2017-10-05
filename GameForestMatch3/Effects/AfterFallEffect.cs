using System;
using GameForestMatch3.Core;
using Microsoft.Xna.Framework;

namespace GameForestMatch3
{
    public class AfterFallEffect : BaseEffect
    {
        private Renderer[] _objects;

        /// <summary>
        /// Длительность деформации
        /// </summary>
        private float _deformationDuration = 0.3f;
        /// <summary>
        /// Размер объекта при падении
        /// </summary>
        private Vector2 _deformationScale = new Vector2(1.1f, 0.9f);

        /// <summary>
        /// Длительность пролёта
        /// </summary>
        private float _fallDuration = 0.15f;

        /// <summary>
        /// Максимальная длина пролёта
        /// </summary>
        private float _fallOffset = 10f;


        /// <summary>
        /// Длительность отскока
        /// </summary>
        private float _bounceDuration = 0.15f;

        /// <summary>
        /// Максимальная длина отскока
        /// </summary>
        private float _bounceOffset = 5f;


        public AfterFallEffect(params Renderer[] objects)
        {
            _objects = objects;
        }

        protected override void OnPlay(Action effectFinished)
        {
            new ScaleInOutEffectAction().Play(new ScaleInOutEffectActionSettings()
            {
                DurationIn = _deformationDuration / 2f,
                DurationOut = _deformationDuration / 2f,
                TargetScale = _deformationScale,
                Items = _objects
            });
            new ShakeInOutEffectAction().Play(new ShakeInOutEffectActionSettings()
            {
                Duration = _fallDuration,
                Offset = _fallOffset,
                Direction = Vector2.UnitY,
                Items = _objects
            }, () =>
                new ShakeInOutEffectAction().Play(new ShakeInOutEffectActionSettings()
                {
                    Duration = _bounceDuration,
                    Offset = _bounceOffset,
                    Direction = -Vector2.UnitY,
                    Items = _objects
                }, effectFinished)
            );
        }
    }
}
