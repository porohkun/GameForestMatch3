using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameForestMatch3.Core;
using GameForestMatch3.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameForestMatch3
{
    /// <summary>
    /// Масштабирует группу объектов до выбранного масштаба и обратно за выбранное время
    /// </summary>
    public class GlowInOutEffectAction : BaseEffectAction<GlowInOutEffectActionSettings>
    {
        private Effect _shader = Resources.Get<Effect>("glow").Clone();

        protected override void OnPlay(GlowInOutEffectActionSettings settings, Action onCompleted)
        {
            foreach (var rend in settings.Items)
                rend.Shader = _shader;
            TweenFactory.Tween(settings.Key, settings.StartGlow, settings.TargetGlow, settings.DurationIn,
                TweenScaleFunctions.SineEaseIn,
                (t) =>
                {
                    for (int i = 0; i < settings.Items.Length; i++)
                        _shader.Parameters["glow"].SetValue(t.CurrentValue);
                }, (t2) =>
                {
                    if (settings.DurationOut > 0)
                        TweenFactory.Tween(settings.Key, settings.TargetGlow, settings.FinalGlow, settings.DurationOut,
                            TweenScaleFunctions.SineEaseOut,
                            (t) =>
                            {
                                _shader.Parameters["glow"].SetValue(t.CurrentValue);
                            }, (t3) =>
                            {
                                if (settings.FinalShader != null)
                                    foreach (var rend in settings.Items)
                                        rend.Shader = settings.FinalShader;
                                Unlock(onCompleted);
                            });
                    else
                        Unlock(onCompleted);
                });
        }

    }

    public class GlowInOutEffectActionSettings : BaseEffectActionSettings
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
        public float StartGlow { get; set; } = 1f;
        public float TargetGlow { get; set; }
        public float FinalGlow { get; set; } = 1f;

        public Effect FinalShader { get; set; }
    }
}