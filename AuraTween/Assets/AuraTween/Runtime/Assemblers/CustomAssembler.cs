using System;
using JetBrains.Annotations;

namespace AuraTween.Assemblers
{
    [PublicAPI]
    public struct CustomAssembler<T> : ITweenAssembler<T>
    {
        private readonly Func<T, T, float, T> _updater;

        public CustomAssembler(Func<T, T, float, T> updater)
        {
            _updater = updater;
        }
        
        public Action<float> Assemble(T start, T end, Action<T> updater)
        {
            var local = _updater;
            return time => updater(local(start, end, time));
        }
    }
}