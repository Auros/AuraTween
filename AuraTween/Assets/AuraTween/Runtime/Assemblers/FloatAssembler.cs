using System;
using UnityEngine;

namespace AuraTween.Assemblers
{
    public class FloatAssembler : ITweenAssembler<float>
    {
        public Action<float> Assemble(float start, float end, Action<float> updater)
        {
            return time => updater(Mathf.Lerp(start, end, time));
        }
    }
}