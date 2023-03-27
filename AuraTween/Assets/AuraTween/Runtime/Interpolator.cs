using JetBrains.Annotations;

namespace AuraTween
{
    [PublicAPI]
    public delegate T Interpolator<T>(ref T start, ref T end, ref float time);
}