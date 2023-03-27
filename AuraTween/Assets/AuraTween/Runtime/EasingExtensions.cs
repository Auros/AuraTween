using System;
using JetBrains.Annotations;

namespace AuraTween
{
    [PublicAPI]
    public static class EasingExtensions
    {
        public static EaseProcedure ToInterpolator(this Ease ease)
        {
            return ease switch
            {
                Ease.Linear => Easer.FastLinear,
                Ease.InSine => Easer.InSine,
                Ease.OutSine => Easer.OutSine,
                Ease.InOutSine => Easer.InOutSine,
                Ease.InQuad => Easer.InQuad,
                Ease.OutQuad => Easer.OutQuad,
                Ease.InOutQuad => Easer.InOutQuad,
                Ease.InCubic => Easer.InCubic,
                Ease.OutCubic => Easer.OutCubic,
                Ease.InOutCubic => Easer.InOutCubic,
                Ease.InQuart => Easer.InQuart,
                Ease.OutQuart => Easer.OutQuart,
                Ease.InOutQuart => Easer.InOutQuart,
                Ease.InQuint => Easer.InQuint,
                Ease.OutQuint => Easer.OutQuint,
                Ease.InOutQuint => Easer.InOutQuint,
                Ease.InExpo => Easer.InExpo,
                Ease.OutExpo => Easer.OutExpo,
                Ease.InOutExpo => Easer.InOutExpo,
                Ease.InCirc => Easer.InCirc,
                Ease.OutCirc => Easer.OutCirc,
                Ease.InOutCirc => Easer.InOutCirc,
                Ease.InBack => Easer.InBack,
                Ease.OutBack => Easer.OutBack,
                Ease.InOutBack => Easer.InOutBack,
                Ease.InElastic => Easer.InElastic,
                Ease.OutElastic => Easer.OutElastic,
                Ease.InOutElastic => Easer.InOutElastic,
                Ease.InBounce => Easer.InBounce,
                Ease.OutBounce => Easer.OutBounce,
                Ease.InOutBounce => Easer.InOutBounce,
                _ => throw new ArgumentOutOfRangeException(nameof(ease), ease, null)
            };
        }
    }
}