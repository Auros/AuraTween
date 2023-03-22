using System;
using AuraTween.Assemblers;
using UnityEngine;

namespace AuraTween
{
    public static class TweenManagerExtensions
    {
        public static Tween Run(this TweenManager tweenManager, float start, float end, float duration, Action<float> updater, Func<float, float, float, float> interpolator)
            => tweenManager.Run<float, FloatAssembler>(start, end, duration, updater, interpolator);
        
        public static Tween Run(this TweenManager tweenManager, Vector2 start, Vector2 end, float duration, Action<Vector2> updater, Func<float, float, float, float> interpolator)
            => tweenManager.Run<Vector2, Vector2Assembler>(start, end, duration, updater, interpolator);
        
        public static Tween Run(this TweenManager tweenManager, Vector3 start, Vector3 end, float duration, Action<Vector3> updater, Func<float, float, float, float> interpolator)
            => tweenManager.Run<Vector3, Vector3Assembler>(start, end, duration, updater, interpolator);
        
        public static Tween Run(this TweenManager tweenManager, Quaternion start, Quaternion end, float duration, Action<Quaternion> updater, Func<float, float, float, float> interpolator)
            => tweenManager.Run<Quaternion, QuaternionAssembler>(start, end, duration, updater, interpolator);
        
        public static Tween Run(this TweenManager tweenManager, Pose start, Pose end, float duration, Action<Pose> updater, Func<float, float, float, float> interpolator)
            => tweenManager.Run<Pose, PoseAssembler>(start, end, duration, updater, interpolator);
        
        public static Tween Run(this TweenManager tweenManager, Color start, Color end, float duration, Action<Color> updater, Func<float, float, float, float> interpolator)
            => tweenManager.Run<Color, ColorAssembler>(start, end, duration, updater, interpolator);

        public static Tween Run<T>(this TweenManager tweenManager, T start, T end, float duration, Action<T> updater, Func<float, float, float, float> interpolator, ITweenAssembler<T> assembler)
        {
            var builder = new TweenBuilder<T>
            {
                EndValue = end,
                Updater = updater,
                StartValue = start,
                Duration = duration,
                Assembler = assembler,
                Interpolator = interpolator
            };
            return tweenManager.Run(builder.Build());
        }

        private static Tween Run<T, TAssembler>(this TweenManager tweenManager, T start, T end, float duration, Action<T> updater, Func<float, float, float, float> interpolator) where TAssembler : ITweenAssembler<T>, new()
        {
            var builder = new TweenBuilder<T>
            {
                EndValue = end,
                Updater = updater,
                StartValue = start,
                Duration = duration,
                Interpolator = interpolator,
                Assembler = new TAssembler()
            };
            return tweenManager.Run(builder.Build());
        }
    }
}