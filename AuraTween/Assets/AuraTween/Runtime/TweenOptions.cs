using System;

namespace AuraTween
{
    public ref struct TweenOptions
    {
        public float Duration;
        public Action? OnCancel;
        public Action? OnComplete;
        public Func<bool>? Lifetime;
        public Action<float> Updater;
        public Func<float, float> Interpolator;
    }
}