﻿using System;

namespace GameForestMatch3
{
    public abstract class BaseEffect
    {
        protected static Random _rnd = new Random();
        
        public void Play(Action onPlayCompleted = null)
        {
            OnPlay(onPlayCompleted);
        }
        
        protected abstract void OnPlay(Action effectFinished);
    }
}