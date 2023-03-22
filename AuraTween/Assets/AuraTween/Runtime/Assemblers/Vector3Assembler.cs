using System;
using UnityEngine;

namespace AuraTween.Assemblers
{
    public struct Vector3Assembler : ITweenAssembler<Vector3>
    {
        public Action<float> Assemble(Vector3 start, Vector3 end, Action<Vector3> updater)
        {
            return time => updater(Vector3.Lerp(start, end, time));
        }
    }
}