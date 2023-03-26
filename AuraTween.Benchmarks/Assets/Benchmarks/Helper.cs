using UnityEngine;

namespace AuraTween.Benchmarks
{
    public class Helper
    {
        public static TweenManager Manager => new GameObject("AuraTweenManager").AddComponent<TweenManager>();
    }
}