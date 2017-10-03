using System;
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
            TweenFactory.Tween(settings.Key, Vector2.One, settings.TargetScale, settings.Duration / 2f,
                TweenScaleFunctions.SineEaseIn,
                (t) =>
                {
                    for (int i = 0; i < settings.Items.Length; i++)
                        settings.Items[i].Scale = originScale[i] * t.CurrentValue;
                }, (t2) =>
                {
                    TweenFactory.Tween(settings.Key, settings.TargetScale, Vector2.One, settings.Duration / 2f,
                        TweenScaleFunctions.SineEaseOut,
                        (t) =>
                        {
                            for (int i = 0; i < settings.Items.Length; i++)
                                settings.Items[i].Scale = originScale[i] * t.CurrentValue;
                        }, (t3) =>
                        {
                            Unlock(onCompleted);
                        });
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
        public float Duration { get; set; }

        /// <summary>
        /// Целевой масштаб
        /// </summary>
        public Vector2 TargetScale { get; set; }

    }
}