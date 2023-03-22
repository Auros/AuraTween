using System;

namespace AuraTween.Exceptions
{
    public class MissingTweenManagerException : Exception
    {
        internal MissingTweenManagerException(Tween tween) : base($"Tween {tween.Id} does not have a valid {nameof(TweenManager)}. Has it been destroyed?")
        {
            
        }
    }
}