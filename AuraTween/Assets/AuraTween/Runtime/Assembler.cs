using JetBrains.Annotations;

namespace AuraTween
{
    [PublicAPI]
    public delegate T Assembler<T>(ref T start, ref T end, ref float time);
}