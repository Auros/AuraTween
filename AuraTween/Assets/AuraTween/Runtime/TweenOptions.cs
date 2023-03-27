using System;
using JetBrains.Annotations;

namespace AuraTween
{
    [PublicAPI]
    public ref struct TweenOptions
    {
        public float Duration;
        public Func<bool>? Lifetime;
        public Action<float> Updater;
        public EaseProcedure Procedure;
    }
}