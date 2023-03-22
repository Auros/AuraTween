using System;

namespace AuraTween.Exceptions
{
    public class TweenCanceledException : Exception
    {
        internal TweenCanceledException(Tween tween) : base($"The tween {tween.Id} ")
        {
            
        }
    }
}