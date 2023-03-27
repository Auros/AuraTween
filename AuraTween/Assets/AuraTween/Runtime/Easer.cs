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

        public static float Linear(float time) => time;
        public static float FastLinear(ref float time) => time;
        public static float Linear(float start, float end, float time) => Mathf.Lerp(start, end, time);
        public static float FastLinear(ref float start, ref float end, ref float time) => start + (end - start) * time;

        #region Sine

        public static float InSine(ref float time)
        {
            var cos = Mathf.Cos(time * (Mathf.PI * 0.5f));
            return -1f * cos + 1f;
        }

        public static float OutSine(ref float time)
        {
            var opposite = 1f - time;
            return 1f - InSine(ref opposite);
        }

        public static float InOutSine(ref float time)
        {
            var cos = Mathf.Cos(time * Mathf.PI) - 1;
            return -1f * 0.5f * cos;
        }
        
        #endregion

        #region Quad

        public static float InQuad(ref float time) => time * time;

        public static float OutQuad(ref float time) => -1f * time * (time - 2);

        public static float InOutQuad(ref float time)
        {
            var newTime = time / 0.5f;
            if (newTime < 1)
                return 0.5f * newTime * newTime;
            newTime--;
            return -1f * 0.5f * (newTime * (newTime - 2) - 1);
        }

        #endregion

        #region Cubic

        public static float InCubic(ref float time) => time * time * time;

        public static float OutCubic(ref float time)
        {
            var newTime = time - 1f;
            return InCubic(ref newTime) + 1;
        }

        public static float InOutCubic(ref float time)
        {
            var newTime = time / 0.5f;
            if (newTime < 1)
                return 0.5f * newTime * newTime * newTime;
            newTime -= 2;
            return 0.5f * (newTime * newTime * newTime + 2);
        }

        #endregion

        #region Quart

        public static float InQuart(ref float time) => time * time * time * time;

        public static float OutQuart(ref float time)
        {
            var newTime = time - 1f;
            return (InQuart(ref newTime) - 1f) * -1f;
        }

        public static float InOutQuart(ref float time)
        {
            var newTime = time / 0.5f;
            if (newTime < 1)
                return 0.5f * InQuart(ref newTime);
            newTime -= 2;
            return -1f * 0.5f * (InQuart(ref newTime) - 2);
        }

        #endregion

        #region Quint

        public static float InQuint(ref float time) => time * time * time * time * time;

        public static float OutQuint(ref float time)
        {
            var newTime = time - 1f;
            return InQuint(ref newTime) + 1f;
        }

        public static float InOutQuint(ref float time)
        {
            var newTime = time / .5f;
            if (newTime < 1)
                return 0.5f * InQuint(ref newTime);
            newTime -= 2;
            return 0.5f * (InQuint(ref newTime) + 2);
        }
        
        #endregion

        #region Exponential

        public static float InExpo(ref float time) => Mathf.Pow(2, 10 * (time - 1));

        public static float OutExpo(ref float time) => -Mathf.Pow(2, -10 * time) + 1;

        public static float InOutExpo(ref float time)
        {
            var newTime = time / 0.5f;
            if (newTime < 1)
                return 0.5f * Mathf.Pow(2, 10 * (newTime - 1));
            newTime--;
            return 0.5f * (-Mathf.Pow(2, -10 * newTime) + 2);
        }

        #endregion

        #region Circle

        public static float InCirc(ref float time) => -1f * (Mathf.Sqrt(1 - time * time) - 1);

        public static float OutCirc(ref float time) => Mathf.Sqrt(1 - (time - 1) * (time - 1));

        public static float InOutCirc(ref float time)
        {
            var newTime = time / 0.5f;
            if (newTime < 1) 
                return -1f * 0.5f * (Mathf.Sqrt(1 - newTime * newTime) - 1);
            newTime -= 2;
            return 0.5f * (Mathf.Sqrt(1 - newTime * newTime) + 1);
        }

        #endregion

        #region Back

        public static float InBack(ref float time) => time * time * ((BackOvershoot + 1) * time - BackOvershoot);

        public static float OutBack(ref float time)
        {
            var newTime = time - 1f;
            return newTime * newTime * ((BackOvershoot + 1) * newTime + BackOvershoot) + 1;
        }

        public static float InOutBack(ref float time)
        {
            var newTime = time / 0.5f;
            if (1 > newTime)
                return 0.5f * (newTime * newTime * ((InOutBackOvershoot + 1f) * newTime - InOutBackOvershoot));
            newTime -= 2;
            return 0.5f * (newTime * newTime * ((InOutBackOvershoot + 1f) * newTime + InOutBackOvershoot) + 2);
        }

        #endregion

        #region Elastic

        public static float InElastic(ref float time)
        {
            float s;
            float a = 0;
            const float d = 1f;
            const float p = d * .3f;

            if (time == 0)
                return 0;

            var newTime = time;
            if (Mathf.Abs((newTime /= d) - 1) < 0.0001f)
                return 1f;

            if (a is 0f or < 1f)
            {
                a = 1f;
                s = p / 4;
            }
            else
            {
                s = p / (2 * Mathf.PI) * Mathf.Asin(1f / a);
            }

            return -(a * Mathf.Pow(2, 10 * (newTime -= 1)) * Mathf.Sin((newTime * d - s) * (2 * Mathf.PI) / p));
        }

        public static float OutElastic(ref float time)
        {
            const float p = .3f;
            const float s = p * 0.25f;
            const float a = 1f;

            if (time == 0)
                return 0;

            if (Mathf.Approximately(time, 1f))
                return 1f;

            return a * Mathf.Pow(2, -10 * time) * Mathf.Sin((time - s) * (2 * Mathf.PI) / p) + 1f;
        }

        public static float InOutElastic(ref float time)
        {
            float s;
            float a = 0;
            const float d = 1f;
            const float p = d * .3f;

            if (time == 0)
                return 0;

            var newTime = time;
            if (Math.Abs((newTime /= d * 0.5f) - 2) < 0.0001f)
                return 1f;

            if (a is 0f or < 1f)
            {
                a = 1f;
                s = p / 4;
            }
            else
            {
                s = p / (2 * Mathf.PI) * Mathf.Asin(1f / a);
            }

            if (newTime < 1)
                return -0.5f * (a * Mathf.Pow(2, 10 * (newTime -= 1)) * Mathf.Sin((newTime * d - s) * (2 * Mathf.PI) / p));
            return a * Mathf.Pow(2, -10 * (newTime -= 1)) * Mathf.Sin((newTime * d - s) * (2 * Mathf.PI) / p) * 0.5f + 1f;
        }
        
        #endregion

        #region Bounce

        public static float InBounce(ref float time)
        {
            var newTime = 1f - time;
            return 1f - OutBounce(ref newTime);
        }

        public static float OutBounce(ref float time)
        {
            switch (time)
            {
                case < 1 / 2.75f:
                    return 1f * (7.5625f * time * time);
                case < 2 / 2.75f:
                    var newTime = time - 1.5f / 2.75f;
                    return 7.5625f * newTime * newTime + .75f;
                default:
                {
                    if (time < 2.5 / 2.75)
                    {
                        var nTime = time - 2.25f / 2.75f;
                        return 7.5625f * nTime * nTime + .9375f;
                    }
                    var xTime = time - 2.625f / 2.75f;
                    return 7.5625f * xTime * xTime + .984375f;
                }
            }
        }

        public static float InOutBounce(ref float time)
        {
            var target = time * 2f;
            if (time < 0.5f)
            {
                return InBounce(ref target) * 0.5f;
            }
            target -= 1f;
            return OutBounce(ref target) * 0.5f + 1f * 0.5f;
        }
        
        #endregion
    }
}