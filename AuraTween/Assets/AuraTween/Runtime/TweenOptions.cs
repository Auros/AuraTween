using System;
using JetBrains.Annotations;
using UnityEngine;
using Object = UnityEngine.Object;

namespace AuraTween
{
    [PublicAPI]
    public ref struct TweenOptions
    {
        public float Duration;
        public Func<bool>? Lifetime;
        public Action<float> Updater;
        public EaseProcedure Procedure;
        public Object? Owner;
    }
}