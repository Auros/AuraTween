using System;

namespace AuraTween.Exceptions
{
    public class MissingTweenException : Exception
    {
        internal MissingTweenException(Tween tween) : base($"The tween {tween.Id} does not exist.")
        {
            
        }
    }
}