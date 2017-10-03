using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using GameForestMatch3.Core;
using Microsoft.Xna.Framework;

namespace GameForestMatch3
{
    //public class BaseEffectAction 
    //{
    //    //public bool Free { get; private set; }

    //    //protected EffectsManager m_Manager;

    //    //public void Initialize(EffectsManager manager)
    //    //{
    //    //    m_Manager = manager;
    //    //    OnInitialize();
    //    //}

    //    //protected virtual void OnInitialize() { }

    //    //public void Lock()
    //    //{
    //    //    //Free = false;
    //    //    Enabled = true;
    //    //    OnLock();
    //    //}

    //    //protected virtual void OnLock() { }


    //}

    public class BaseEffectAction<T> /*: BaseEffectAction*/ where T : BaseEffectActionSettings
    {
        public void Unlock(Action onCompleted = null)
        {
            OnUnlock();
            Coroutine.StopAllFor(this);
            //Free = true;
            //Enabled = false;
            onCompleted?.Invoke();
        }

        protected virtual void OnUnlock() { }

        public void Play(float delayDuration, T settings, Action onCompleted = null)
        {
            if (delayDuration <= 0f)
                OnPlay(settings, onCompleted);
            else
                Delay(delayDuration, settings, onCompleted);
        }

        public void Play(T settings, Action onCompleted = null)
        {
            OnPlay(settings, onCompleted);
        }

        protected virtual void OnPlay(T settings, Action onCompleted) { }

        protected void Delay(float delayDuration, Action onCompleted)
        {
            Coroutine.Start(this, DelayRoutine(delayDuration, onCompleted));
        }

        protected void Delay(float delayDuration, T settings, Action onCompleted)
        {
            Coroutine.Start(this, DelayRoutine(delayDuration, () => OnPlay(settings, onCompleted)));
        }

        private IEnumerator<float> DelayRoutine(float duration, Action onCompleted)
        {
            var remainingDelay = duration;
            while (remainingDelay > 0f)
            {
                remainingDelay -= (float)Coroutine.Time.ElapsedGameTime.TotalSeconds;
                yield return 0;
            }
            onCompleted?.Invoke();
        }

    }

    public abstract class BaseEffectActionSettings
    {
        private static readonly Random _rnd = new Random();

        private object _key;

        public object Key
        {
            get => _key ?? (_key = _rnd.Next(0, 100000));
            set => _key = value;
        }
    }
}