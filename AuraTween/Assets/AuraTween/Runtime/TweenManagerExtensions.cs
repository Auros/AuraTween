using System;
using JetBrains.Annotations;
using UnityEngine;

namespace AuraTween
{
    [PublicAPI]
    public static class TweenManagerExtensions
    {
        public static Tween Run(this TweenManager tweenManager, float start, float end, float duration, Action<float> updater, Func<float, float> interpolator)
            => tweenManager.Run(start, end, duration, updater, interpolator, Assemblers.Float);
        
        public static Tween Run(this TweenManager tweenManager, Vector2 start, Vector2 end, float duration, Action<Vector2> updater, Func<float, float> interpolator)
            => tweenManager.Run(start, end, duration, updater, interpolator, Assemblers.Vector2);
        
        public static Tween Run(this TweenManager tweenManager, Vector3 start, Vector3 end, float duration, Action<Vector3> updater, Func<float, float> interpolator)
            => tweenManager.Run(start, end, duration, updater, interpolator, Assemblers.Vector3);
        
        public static Tween Run(this TweenManager tweenManager, Quaternion start, Quaternion end, float duration, Action<Quaternion> updater, Func<float, float> interpolator)
            => tweenManager.Run(start, end, duration, updater, interpolator, Assemblers.Quaternion);
        
        public static Tween Run(this TweenManager tweenManager, Pose start, Pose end, float duration, Action<Pose> updater, Func<float, float> interpolator)
            => tweenManager.Run(start, end, duration, updater, interpolator, Assemblers.Pose);
        
        public static Tween Run(this TweenManager tweenManager, Color start, Color end, float duration, Action<Color> updater, Func<float, float> interpolator)
            => tweenManager.Run(start, end, duration, updater, interpolator, Assemblers.Color);

        public static Tween Run<T>(this TweenManager tweenManager, T start, T end, float duration, Action<T> updater, Func<float, float> interpolator, Func<T, T, Action<T>, Action<float>> assembler)
        {
            var options = new TweenOptions
            {
                Duration = duration,
                Interpolator = interpolator,
                Updater = assembler(start, end, updater)
            };
            return tweenManager.Run(options);
        }
    }
}