using System;
using JetBrains.Annotations;
using UnityEngine;

namespace AuraTween
{
    [PublicAPI]
    // Some of the method implementations provided are from https://gist.github.com/cjddmut/d789b9eb78216998e95c
    public static class Easer
    {
        internal const float BackOvershoot = 1.70158f;
        internal const float InOutBackOvershoot = 2.5949095f;

        public static float Linear(float time) => Linear(0, 1, time);
        public static float Linear(float start, float end, float time) => Mathf.Lerp(start, end, time);

        #region Sine

        public static float InSine(float time) => InSine(0, 1, time);
        public static float InSine(float start, float end, float time)
        {
            end -= start;
            var cos = Mathf.Cos(time * (Mathf.PI * 0.5f));
            return -end * cos + end + start;
        }

        public static float OutSine(float time) => OutSine(0f, 1f, time);
        public static float OutSine(float start, float end, float time)
        {
            return 1f - InSine(start, end, 1f - time);
        }

        public static float InOutSine(float time) => InOutSine(0f, 1f, time);
        public static float InOutSine(float start, float end, float time)
        {
            end -= start;
            var cos = Mathf.Cos(time * Mathf.PI) - 1;
            return -end * 0.5f * cos + start;
        }
        
        #endregion

        #region Quad

        public static float InQuad(float time) => InQuad(0f, 1f, time);
        public static float InQuad(float start, float end, float time)
        {
            return (end - start) * time * time + start;
        }

        public static float OutQuad(float time) => OutQuad(0f, 1f, time);
        public static float OutQuad(float start, float end, float time)
        {
            return -(end - start) * time * (time - 2) + start;
        }

        public static float InOutQuad(float time) => InOutQuad(0f, 1f, time);
        public static float InOutQuad(float start, float end, float time)
        {
            time /= .5f;
            end -= start;
            if (time < 1)
                return end * 0.5f * time * time + start;
            time--;
            return -end * 0.5f * (time * (time - 2) - 1) + start;
        }

        #endregion

        #region Cubic

        public static float InCubic(float time) => InCubic(0f, 1f, time);
        public static float InCubic(float start, float end, float time)
        {
            return (end - start) * time * time * time + start;
        }

        public static float OutCubic(float time) => OutCubic(0f, 1f, time);
        public static float OutCubic(float start, float end, float time)
        {
            time--;
            return (end - start) * (time * time * time + 1) + start;
        }

        public static float InOutCubic(float time) => InOutCubic(0f, 1f, time);
        public static float InOutCubic(float start, float end, float time)
        {
            time /= .5f;
            end -= start;
            if (time < 1)
                return end * 0.5f * time * time * time + start;
            time -= 2;
            return end * 0.5f * (time * time * time + 2) + start;
        }

        #endregion

        #region Quart

        public static float InQuart(float time) => InQuart(0f, 1f, time);
        public static float InQuart(float start, float end, float time)
        {
            return (end - start) * time * time * time * time + start;
        }

        public static float OutQuart(float time) => OutQuart(0f, 1f, time);
        public static float OutQuart(float start, float end, float time)
        {
            time--;
            return (end - start) * (time * time * time * time - 1) + start;
        }

        public static float InOutQuart(float time) => InOutQuart(0f, 1f, time);
        public static float InOutQuart(float start, float end, float time)
        {
            time /= .5f;
            end -= start;
            if (time < 1)
                return end * 0.5f * time * time * time * time + start;
            time -= 2;
            return -end * 0.5f * (time * time * time * time - 2) + start;
        }

        #endregion

        #region Quint

        public static float InQuint(float time) => InQuint(0f, 1f, time);
        public static float InQuint(float start, float end, float time)
        {
            return (end - start) * time * time * time * time * time + start;
        }

        public static float OutQuint(float time) => OutQuint(0f, 1f, time);
        public static float OutQuint(float start, float end, float time)
        {
            time--;
            return (end - start) * (time * time * time * time * time + 1) + start;
        }

        public static float InOutQuint(float time) => InOutQuint(0f, 1f, time);
        public static float InOutQuint(float start, float end, float time)
        {
            time /= .5f;
            end -= start;
            if (time < 1)
                return end * 0.5f * time * time * time * time * time + start;
            time -= 2;
            return end * 0.5f * (time * time * time * time * time + 2) + start;
        }
        
        #endregion

        #region Exponential

        public static float InExpo(float time) => InExpo(0f, 1f, time);
        public static float InExpo(float start, float end, float time)
        {
            return (end - start) * Mathf.Pow(2, 10 * (time - 1)) + start;
        }

        public static float OutExpo(float time) => OutExpo(0f, 1f, time);
        public static float OutExpo(float start, float end, float time)
        {
            return (end - start) * (-Mathf.Pow(2, -10 * time) + 1) + start;
        }

        public static float InOutExpo(float time) => InOutExpo(0f, 1f, time);
        public static float InOutExpo(float start, float end, float time)
        {
            time /= .5f;
            end -= start;
            if (time < 1)
                return end * 0.5f * Mathf.Pow(2, 10 * (time - 1)) + start;
            time--;
            return end * 0.5f * (-Mathf.Pow(2, -10 * time) + 2) + start;
        }

        #endregion

        #region Circle

        public static float InCirc(float time) => InCirc(0f, 1f, time);
        public static float InCirc(float start, float end, float time)
        {
            return -(end - start) * (Mathf.Sqrt(1 - time * time) - 1) + start;
        }

        public static float OutCirc(float time) => OutCirc(0f, 1f, time);
        public static float OutCirc(float start, float end, float time)
        {
            time = time-- * time;
            return (end - start) * Mathf.Sqrt(1 - time) + start;
        }

        public static float InOutCirc(float time) => InOutCirc(0f, 1f, time);
        public static float InOutCirc(float start, float end, float time)
        {
            time /= .5f;
            end -= start;
            if (time < 1) 
                return -end * 0.5f * (Mathf.Sqrt(1 - time * time) - 1) + start;
            time -= 2;
            return end * 0.5f * (Mathf.Sqrt(1 - time * time) + 1) + start;
        }

        #endregion

        #region Back

        public static float InBack(float time) => InBack(0f, 1f, time);
        public static float InBack(float start, float end, float time)
        {
            return (end - start) * time * time * ((BackOvershoot + 1) * time - BackOvershoot) + start;
        }

        public static float OutBack(float time) => OutBack(0f, 1f, time);
        public static float OutBack(float start, float end, float time)
        {
            time -= 1f;
            return (end - start) * (time * time * ((BackOvershoot + 1) * time + BackOvershoot) + 1) + start;
        }

        public static float InOutBack(float time) => InOutBack(0f, 1f, time);
        public static float InOutBack(float start, float end, float time)
        {
            end -= start;
            time /= 0.5f;
            if (1 > time)
                return end * 0.5f * (time * time * ((InOutBackOvershoot + 1) * time - InOutBackOvershoot)) + start;
            time -= 2;
            return end * 0.5f * (time * time * ((InOutBackOvershoot + 1) * time + InOutBackOvershoot) + 2) + start;
        }

        #endregion

        #region Elastic

        public static float InElastic(float time) => InElastic(0f, 1f, time);
        public static float InElastic(float start, float end, float time)
        {
            end -= start;
            const float p = .3f;
            float s;
            float a = 0;

            if (time == 0) return start;

            if (Mathf.Approximately(time, 1f))
                return start + end;

            if (a == 0f || a < Mathf.Abs(end))
            {
                a = end;
                s = p / 4;
            }
            else
            {
                s = p / (2 * Mathf.PI) * Mathf.Asin(end / a);
            }

            return -(a * Mathf.Pow(2, 10 * (time -= 1)) * Mathf.Sin((time - s) * (2 * Mathf.PI) / p)) + start;
        }

        public static float OutElastic(float time) => OutElastic(0f, 1f, time);
        public static float OutElastic(float start, float end, float time)
        {
            end -= start;

            const float p = .3f;
            float a = 0;
            float s;

            if (time == 0) return start;

            if (Mathf.Approximately(time, 1f))
                return start + end;

            if (a == 0f || a < Mathf.Abs(end))
            {
                a = end;
                s = p * 0.25f;
            }
            else
            {
                s = p / (2 * Mathf.PI) * Mathf.Asin(end / a);
            }

            return a * Mathf.Pow(2, -10 * time) * Mathf.Sin((time - s) * (2 * Mathf.PI) / p) + end + start;
        }

        public static float InOutElastic(float time) => InOutElastic(0f, 1f, time);
        public static float InOutElastic(float start, float end, float time)
        {
            end -= start;

            const float d = 1f;
            const float p = d * .3f;
            float s;
            float a = 0;

            if (time == 0) return start;

            if (Math.Abs((time /= d * 0.5f) - 2) < .001f) return start + end;

            if (a == 0f || a < Mathf.Abs(end))
            {
                a = end;
                s = p / 4;
            }
            else
            {
                s = p / (2 * Mathf.PI) * Mathf.Asin(end / a);
            }

            if (time < 1) return -0.5f * (a * Mathf.Pow(2, 10 * (time -= 1)) * Mathf.Sin((time * d - s) * (2 * Mathf.PI) / p)) + start;
            return a * Mathf.Pow(2, -10 * (time -= 1)) * Mathf.Sin((time * d - s) * (2 * Mathf.PI) / p) * 0.5f + end + start;
        }
        
        #endregion

        #region Bounce

        public static float InBounce(float time) => InBounce(0f, 1f, time);
        public static float InBounce(float start, float end, float time)
        {
            end -= start;
            return end - OutBounce(0, end, 1f - time) + start;
        }

        public static float OutBounce(float time) => OutBounce(0f, 1f, time);
        public static float OutBounce(float start, float end, float time)
        {
            time /= 1f;
            end -= start;
            switch (time)
            {
                case < 1 / 2.75f:
                    return end * (7.5625f * time * time) + start;
                case < 2 / 2.75f:
                    time -= 1.5f / 2.75f;
                    return end * (7.5625f * time * time + .75f) + start;
                default:
                {
                    if (time < 2.5 / 2.75)
                    {
                        time -= 2.25f / 2.75f;
                        return end * (7.5625f * time * time + .9375f) + start;
                    }
                    time -= 2.625f / 2.75f;
                    return end * (7.5625f * time * time + .984375f) + start;
                }
            }
        }

        public static float InOutBounce(float time) => InOutBounce(0f, 1f, time);
        public static float InOutBounce(float start, float end, float value)
        {
            end -= start;
            if (value < 1f * 0.5f)
                return InBounce(0, end, value * 2) * 0.5f + start;
            return OutBounce(0, end, value * 2 - 1f) * 0.5f + end * 0.5f + start;
        }
        
        #endregion
    }
}