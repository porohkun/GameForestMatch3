using System;

namespace GameForestMatch3
{
    public abstract class BaseEffect
    {
        protected static Random _rnd = new Random();
        //protected EffectsManager _manager { get; private set; }
        
        //public void Initialize(EffectsManager manager)
        //{
        //    _manager = manager;
        //}
        
        public void Play(Action onPlayCompleted = null)
        {
            OnPlay(onPlayCompleted);
        }
        
        protected abstract void OnPlay(Action effectFinished);
    }
}