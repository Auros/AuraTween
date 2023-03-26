using System;
using JetBrains.Annotations;
using UnityEngine;
using Object = UnityEngine.Object;

namespace AuraTween
{
    [PublicAPI]
    public static class TweenManagerExtensions
    {
        public static Tween Run(this TweenManager tweenManager, Pose start, Pose end, float duration, Action<Pose> updater, Func<float, float> interpolator, Object? owner = null)
            => tweenManager.Run(start, end, duration, updater, interpolator, Assemblers.Pose, owner);
        
        public static Tween Run(this TweenManager tweenManager, Color start, Color end, float duration, Action<Color> updater, Func<float, float> interpolator, Object? owner = null)
            => tweenManager.Run(start, end, duration, updater, interpolator, Assemblers.Color, owner);
        
        public static Tween Run(this TweenManager tweenManager, float start, float end, float duration, Action<float> updater, Func<float, float> interpolator, Object? owner = null)
            => tweenManager.Run(start, end, duration, updater, interpolator, Assemblers.Float, owner);
        
        public static Tween Run(this TweenManager tweenManager, Vector2 start, Vector2 end, float duration, Action<Vector2> updater, Func<float, float> interpolator, Object? owner = null)
            => tweenManager.Run(start, end, duration, updater, interpolator, Assemblers.Vector2, owner);
        
        public static Tween Run(this TweenManager tweenManager, Vector3 start, Vector3 end, float duration, Action<Vector3> updater, Func<float, float> interpolator, Object? owner = null)
            => tweenManager.Run(start, end, duration, updater, interpolator, Assemblers.Vector3, owner);
        
        public static Tween Run(this TweenManager tweenManager, Quaternion start, Quaternion end, float duration, Action<Quaternion> updater, Func<float, float> interpolator, Object? owner = null)
            => tweenManager.Run(start, end, duration, updater, interpolator, Assemblers.Quaternion, owner);

        public static Tween Run<T>(this TweenManager tweenManager, T start, T end, float duration, Action<T> updater, Func<float, float> interpolator, Assembler<T> assembler, Object? owner = null)
        {
            var options = new TweenOptions
            {
                Duration = duration,
                Interpolator = interpolator,
                Updater = time => updater(assembler(ref start, ref end, ref time)),
                Lifetime = owner ? () => owner : null,
            };
            return tweenManager.Run(options);
        }
    }
}