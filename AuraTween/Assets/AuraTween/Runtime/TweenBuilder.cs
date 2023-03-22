using System;
using UnityEngine;

namespace AuraTween
{
    public struct TweenBuilder<T>
    {
        public ITweenAssembler<T> Assembler;
        public Action<T> Updater; 
        public float Duration;
        public T StartValue;
        public T EndValue;
        public Ease Ease;

        public TweenBuilder<T> WithDuration(float duration)
        {
            Duration = duration;
            return this;
        }

        public TweenBuilder<T> WithStartValue(T startValue)
        {
            StartValue = startValue;
            return this;
        }

        public TweenBuilder<T> WithEndValue(T endValue)
        {
            EndValue = endValue;
            return this;
        }

        public TweenBuilder<T> WithEase(Ease ease)
        {
            Ease = ease;
            return this;
        }

        public TweenOptions Build()
        {
            return new TweenOptions
            {
                Ease = Ease,
                Duration = Duration,
                Updater = Assembler.Assemble(StartValue, EndValue, Updater)
            };
        }
    }
}