using System;
using UnityEngine;

namespace AuraTween.Assemblers
{
    public struct ColorAssembler : ITweenAssembler<Color>
    {
        public Action<float> Assemble(Color start, Color end, Action<Color> updater)
        {
            return time => updater(Color.Lerp(start, end, time));
        }
    }
}