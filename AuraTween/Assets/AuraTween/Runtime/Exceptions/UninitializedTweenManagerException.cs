using System;

namespace AuraTween.Exceptions
{
    public class UninitializedTweenManagerException : Exception
    {
        public UninitializedTweenManagerException() : base("The tween manager has not been initialized, ensure that it is active.")
        {
            
        }
    }
}