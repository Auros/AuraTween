using System;

namespace AuraTween
{
    public struct TweenBuilder<T>
    {
        public Func<float, float, float, float> Interpolator;
        public ITweenAssembler<T> Assembler;
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
                Updater = Assembler.Assemble(StartValue, EndValue, Updater)
            };
        }
    }
}