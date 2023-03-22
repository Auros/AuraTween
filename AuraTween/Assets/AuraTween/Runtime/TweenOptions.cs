using System;

namespace AuraTween
{
    public struct TweenOptions
    {
        public Ease Ease;
        public float Duration;
        public Action<float> Updater;
    }
}