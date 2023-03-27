using System;

namespace AuraTween
{
    public ref struct TweenOptions
    {
        public float Duration;
        public Func<bool>? Lifetime;
        public Action<float> Updater;
        public EaseProcedure Interpolator;
    }
}