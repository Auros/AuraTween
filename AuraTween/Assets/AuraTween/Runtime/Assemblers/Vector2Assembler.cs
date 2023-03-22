using System;
using UnityEngine;

namespace AuraTween.Assemblers
{
    public struct Vector2Assembler : ITweenAssembler<Vector2>
    {
        public Action<float> Assemble(Vector2 start, Vector2 end, Action<Vector2> updater)
        {
            return time => updater(Vector2.Lerp(start, end, time));
        }
    }
}