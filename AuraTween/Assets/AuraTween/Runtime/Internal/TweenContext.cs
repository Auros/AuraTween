using System;

namespace AuraTween.Internal
{
    internal class TweenContext
    {
        public long Id;

        public float Progress;

        public bool WantsToCancel;

        public bool Paused;

        public bool HasLifetime;
        
        public float Duration;
        
        public Action? OnCancel;
        
        public Action? OnComplete;
        
        public Func<bool>? Lifetime;
        
        public Action<float>? Updater;
        
        public Func<float, float>? Interpolator;
    }
}