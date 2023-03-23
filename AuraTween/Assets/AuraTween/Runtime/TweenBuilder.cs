using System;

namespace AuraTween
{
    public struct TweenBuilder<T>
    {
        public Func<T, T, Action<T>, Action<float>> Assembler;
        public Func<float, float> Interpolator;
        public Action<T> Updater; 
        public float Duration;
        public T StartValue;
        public T EndValue;

        public TweenOptions Build()
        {
            return new TweenOptions
            {
                Duration = Duration,
                Interpolator = Interpolator,
                Updater = Assembler(StartValue, EndValue, Updater)
            };
        }
    }
}