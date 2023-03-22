using System;

namespace AuraTween
{
    public static class Easer
    {
        public static float Ease(float start, float end, float time, Ease ease)
        {
            switch (ease)
            {
                case AuraTween.Ease.Linear:
                    return EasingMethods.Linear(start, end, time);
                case AuraTween.Ease.InSine:
                    return EasingMethods.InSine(start, end, time);
                case AuraTween.Ease.OutSine:
                    return EasingMethods.OutSine(start, end, time);
                case AuraTween.Ease.InOutSine:
                    return EasingMethods.InOutSine(start, end, time);
                case AuraTween.Ease.InQuad:
                    return EasingMethods.InQuad(start, end, time);
                case AuraTween.Ease.OutQuad:
                    return EasingMethods.OutQuad(start, end, time);
                case AuraTween.Ease.InOutQuad:
                    return EasingMethods.InOutQuad(start, end, time);
                case AuraTween.Ease.InCubic:
                    return EasingMethods.InCubic(start, end, time);
                case AuraTween.Ease.OutCubic:
                    return EasingMethods.OutCubic(start, end, time);
                case AuraTween.Ease.InOutCubic:
                    return EasingMethods.InOutCubic(start, end, time);
                case AuraTween.Ease.InQuart:
                    return EasingMethods.InQuart(start, end, time);
                case AuraTween.Ease.OutQuart:
                    return EasingMethods.OutQuart(start, end, time);
                case AuraTween.Ease.InOutQuart:
                    return EasingMethods.InOutQuart(start, end, time);
                case AuraTween.Ease.InQuint:
                    return EasingMethods.InQuint(start, end, time);
                case AuraTween.Ease.OutQuint:
                    return EasingMethods.OutQuint(start, end, time);
                case AuraTween.Ease.InOutQuint:
                    return EasingMethods.InOutQuint(start, end, time);
                case AuraTween.Ease.InExpo:
                    return EasingMethods.InExpo(start, end, time);
                case AuraTween.Ease.OutExpo:
                    return EasingMethods.OutExpo(start, end, time);
                case AuraTween.Ease.InOutExpo:
                    return EasingMethods.InOutExpo(start, end, time);
                case AuraTween.Ease.InCirc:
                    return EasingMethods.InCirc(start, end, time);
                case AuraTween.Ease.OutCirc:
                    return EasingMethods.OutCirc(start, end, time);
                case AuraTween.Ease.InOutCirc:
                    return EasingMethods.InOutCirc(start, end, time);
                case AuraTween.Ease.InBack:
                    return EasingMethods.InBack(start, end, time);
                case AuraTween.Ease.OutBack:
                    return EasingMethods.OutBack(start, end, time);
                case AuraTween.Ease.InOutBack:
                    return EasingMethods.InOutBack(start, end, time);
                case AuraTween.Ease.InElastic:
                    return EasingMethods.InElastic(start, end, time);
                case AuraTween.Ease.OutElastic:
                    return EasingMethods.OutElastic(start, end, time);
                case AuraTween.Ease.InOutElastic:
                    return EasingMethods.InOutElastic(start, end, time);
                case AuraTween.Ease.InBounce:
                    return EasingMethods.InBounce(start, end, time);
                case AuraTween.Ease.OutBounce:
                    return EasingMethods.OutBounce(start, end, time);
                case AuraTween.Ease.InOutBounce:
                    return EasingMethods.InOutBounce(start, end, time);
                default:
                    throw new ArgumentOutOfRangeException(nameof(ease), ease, null);
            }
        }
    }
}