﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameForestMatch3.Core;
using GameForestMatch3.Utils;
using Microsoft.Xna.Framework;

namespace GameForestMatch3
{
    /// <summary>
    /// Масштабирует группу объектов до выбранного масштаба и обратно за выбранное время
    /// </summary>
    public class ScaleInOutEffectAction : BaseEffectAction<ScaleInOutEffectActionSettings>
    {
        protected override void OnPlay(ScaleInOutEffectActionSettings settings, Action onCompleted)
        {
            var originScale = settings.Items.Select(i => i.Scale).ToArray();
            TweenFactory.Tween(settings.Key, settings.StartScale, settings.TargetScale, settings.DurationIn,
                TweenScaleFunctions.SineEaseIn,
                (t) =>
                {
                    for (int i = 0; i < settings.Items.Length; i++)
                        settings.Items[i].Scale = originScale[i] * t.CurrentValue;
                }, (t2) =>
                {
                    if (settings.DurationOut > 0)
                        TweenFactory.Tween(settings.Key, settings.TargetScale, settings.FinalScale, settings.DurationOut,
                            TweenScaleFunctions.SineEaseOut,
                            (t) =>
                            {
                                for (int i = 0; i < settings.Items.Length; i++)
                                    settings.Items[i].Scale = originScale[i] * t.CurrentValue;
                            }, (t3) =>
                            {
                                Unlock(onCompleted);
                            });
                    else
                        Unlock(onCompleted);
                });
        }

    }

    public class ScaleInOutEffectActionSettings : BaseEffectActionSettings
    {
        /// <summary>
        /// Объект для применения экшна
        /// </summary>
        public Renderer[] Items { get; set; }

        /// <summary>
        /// Полное время масштабирования
        /// </summary>
        public float DurationIn { get; set; }
        public float DurationOut { get; set; }

        /// <summary>
        /// Целевой масштаб
        /// </summary>
        public Vector2 StartScale { get; set; } = Vector2.One;
        public Vector2 TargetScale { get; set; }
        public Vector2 FinalScale { get; set; } = Vector2.One;

    }
}