using System;
using UnityEngine;

namespace AuraTween.Assemblers
{
    public struct QuaternionAssembler : ITweenAssembler<Quaternion>
    {
        public Action<float> Assemble(Quaternion start, Quaternion end, Action<Quaternion> updater)
        {
            return time => updater(Quaternion.Lerp(start, end, time));
        }
    }
}