using System;
using AuraTween.Assemblers;
using UnityEngine;

namespace AuraTween
{
    public static class TweenManagerExtensions
    {
        public static Tween Run(this TweenManager tweenManager, float start, float end, float duration, Action<float> updater, Ease ease = Ease.Linear)
            => tweenManager.Run<float, FloatAssembler>(start, end, duration, updater, ease);
        
        public static Tween Run(this TweenManager tweenManager, Vector2 start, Vector2 end, float duration, Action<Vector2> updater, Ease ease = Ease.Linear)
            => tweenManager.Run<Vector2, Vector2Assembler>(start, end, duration, updater, ease);
        
        public static Tween Run(this TweenManager tweenManager, Vector3 start, Vector3 end, float duration, Action<Vector3> updater, Ease ease = Ease.Linear)
            => tweenManager.Run<Vector3, Vector3Assembler>(start, end, duration, updater, ease);
        
        public static Tween Run(this TweenManager tweenManager, Quaternion start, Quaternion end, float duration, Action<Quaternion> updater, Ease ease = Ease.Linear)
            => tweenManager.Run<Quaternion, QuaternionAssembler>(start, end, duration, updater, ease);
        
        public static Tween Run(this TweenManager tweenManager, Pose start, Pose end, float duration, Action<Pose> updater, Ease ease = Ease.Linear)
            => tweenManager.Run<Pose, PoseAssembler>(start, end, duration, updater, ease);
        
        public static Tween Run(this TweenManager tweenManager, Color start, Color end, float duration, Action<Color> updater, Ease ease = Ease.Linear)
            => tweenManager.Run<Color, ColorAssembler>(start, end, duration, updater, ease);
        
        private static Tween Run<T, TAssembler>(this TweenManager tweenManager, T start, T end, float duration, Action<T> updater, Ease ease) where TAssembler : ITweenAssembler<T>, new()
        {
            var builder = new TweenBuilder<T>
            {
                Ease = ease,
                EndValue = end,
                Updater = updater,
                StartValue = start,
                Duration = duration,
                Assembler = new TAssembler()
            };
            return tweenManager.Run(builder.Build());
        }
    }
}