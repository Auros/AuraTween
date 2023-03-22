using System;

namespace AuraTween
{
    public struct TweenOptions
    {
        public float Duration;
        public Action<float> Updater;
        public Func<float, float, float, float> Interpolator;
    }
}