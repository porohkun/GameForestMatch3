using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameForestMatch3.Core;
using GameForestMatch3.Utils;
using Microsoft.Xna.Framework;

namespace GameForestMatch3
{
    /// <summary>
    /// Делает покачивание элемента в заданном направлении и возвращает обратно за указанное время
    /// </summary>
    public class ShakeInOutEffectAction : BaseEffectAction<ShakeInOutEffectActionSettings>
    {
        protected override void OnPlay(ShakeInOutEffectActionSettings settings, Action onCompleted)
        {
            var direction = settings.Direction * settings.Offset;

            var startPositions = new Vector2[settings.Items.Length];

            var itemsArray = settings.Items.ToArray();

            for (int i = 0; i < itemsArray.Length; i++)
                startPositions[i] = itemsArray[i].Position;

            TweenFactory.Tween(settings.Key, Vector2.Zero, direction, settings.Duration / 2f, TweenScaleFunctions.QuadraticEaseOut,
                (t) =>
                {
                    for (int i = 0; i < itemsArray.Length; i++)
                        itemsArray[i].Position = startPositions[i] + t.CurrentValue;
                },
                (t2) =>
                {
                    TweenFactory.Tween(settings.Key, direction, Vector2.Zero, settings.Duration / 2f, TweenScaleFunctions.QuadraticEaseIn,
                        (t) =>
                        {
                            for (int i = 0; i < itemsArray.Length; i++)
                                itemsArray[i].Position = startPositions[i] + t.CurrentValue;
                        },
                        (t3) =>
                        {
                            Unlock(onCompleted);
                        });
                });
        }

    }

    public class ShakeInOutEffectActionSettings : BaseEffectActionSettings
    {
        private Vector2 _direction;

        /// <summary>
        /// Объект для применения экшна
        /// </summary>
        public Renderer[] Items { get; set; }

        /// <summary>
        /// Полное время масштабирования
        /// </summary>
        public float Duration { get; set; }

        /// <summary>
        /// Относительная величина сдвига объекта
        /// </summary>
        public float Offset { get; set; }

        /// <summary>
        /// Направление сдвига объекта
        /// </summary>
        public Vector2 Direction
        {
            get => _direction;
            set { _direction = value; _direction.Normalize(); }
        }
    }
}