using System;

namespace AuraTween
{
    public interface ITweenAssembler<T>
    {
        Action<float> Assemble(T start, T end, Action<T> updater);
    }
}