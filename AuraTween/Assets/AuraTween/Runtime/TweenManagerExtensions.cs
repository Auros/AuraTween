using System;
using JetBrains.Annotations;
using UnityEngine;
using Object = UnityEngine.Object;

namespace AuraTween
{
    [PublicAPI]
    public static class TweenManagerExtensions
    {
        public static Tween Run(this TweenManager tweenManager, Pose start, Pose end, float duration, Action<Pose> updater, EaseProcedure procedure, Object? owner = null)
            => tweenManager.Run(start, end, duration, updater, procedure, BuiltInInterpolators.Pose, owner);
        
        public static Tween Run(this TweenManager tweenManager, Color start, Color end, float duration, Action<Color> updater, EaseProcedure procedure, Object? owner = null)
            => tweenManager.Run(start, end, duration, updater, procedure, BuiltInInterpolators.Color, owner);
        
        public static Tween Run(this TweenManager tweenManager, float start, float end, float duration, Action<float> updater, EaseProcedure procedure, Object? owner = null)
            => tweenManager.Run(start, end, duration, updater, procedure, BuiltInInterpolators.Float, owner);
        
        public static Tween Run(this TweenManager tweenManager, Vector2 start, Vector2 end, float duration, Action<Vector2> updater, EaseProcedure procedure, Object? owner = null)
            => tweenManager.Run(start, end, duration, updater, procedure, BuiltInInterpolators.Vector2, owner);
        
        public static Tween Run(this TweenManager tweenManager, Vector3 start, Vector3 end, float duration, Action<Vector3> updater, EaseProcedure procedure, Object? owner = null)
            => tweenManager.Run(start, end, duration, updater, procedure, BuiltInInterpolators.Vector3, owner);
        
        public static Tween Run(this TweenManager tweenManager, Quaternion start, Quaternion end, float duration, Action<Quaternion> updater, EaseProcedure procedure, Object? owner = null)
            => tweenManager.Run(start, end, duration, updater, procedure, BuiltInInterpolators.Quaternion, owner);

        public static Tween Run<T>(this TweenManager tweenManager, T start, T end, float duration, Action<T> updater, EaseProcedure procedure, Interpolator<T> interpolator, Object? owner = null)
        {
            var options = new TweenOptions
            {
                Duration = duration,
                Procedure = procedure,
                Updater = time => updater(interpolator(ref start, ref end, ref time)),
                Lifetime = owner ? () => owner : null,
                Owner = owner
            };
            return tweenManager.Run(options);
        }
    }
}