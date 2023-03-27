using System;
using System.Collections;
using AnimeTask;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using ElRaccoone.Tweens;
using JetBrains.Annotations;
using NUnit.Framework;
using Unity.PerformanceTesting;
using UnityEngine;
using UnityEngine.TestTools;

namespace AuraTween.Benchmarks
{
    [PublicAPI]
    public class TweenComparisonTests
    {
        private const int Cooldown = 1;
        private static int[] _counts = { 10_000 };
        
        public const string AuraTweenGroup = "AuraTween";
        public const string DoTweenGroup = "DOTween";
        public const string LeanTweenGroup = "LeanTween";
        public const string UnityTweenGroup = "Unity Tweens";
        public const string AnimeTaskGroup = "AnimeTask";

        [UnityTest, Performance]
        public IEnumerator ActiveTweenStress([ValueSource(nameof(_counts))] int count)
        {
            Vector3 start = default;
            var end = Vector3.one;
            const float duration = 0.5f;

            var manager = Helper.Manager;
            manager.SetCapacity(count);
            manager.enabled = true;

            var tweeners = CreateTweeners(count);
            
            yield return new WaitForSecondsRealtime(Cooldown);
            GC.Collect();
            yield return new WaitForSecondsRealtime(Cooldown);
            
            using (Measure.Frames().Scope(AuraTweenGroup))
            {
                foreach (var tween in tweeners)
                {
                    var transform = tween.transform;
                    manager.Run(start, end, duration, v => transform.localPosition = v, Easer.OutQuad, tween);
                }
                yield return new WaitForSeconds(duration);
            }
            
            UnityEngine.Object.Destroy(manager);
            
            DeleteTweeners(tweeners);
            tweeners = CreateTweeners(count);
 
            yield return new WaitForSecondsRealtime(Cooldown);
            DOTween.SetTweensCapacity(count + 1, 50);
            GC.Collect();
            yield return new WaitForSecondsRealtime(Cooldown);

            using (Measure.Frames().Scope(DoTweenGroup))
            {
                foreach (var tween in tweeners)
                    tween.transform.DOLocalMove(end, duration).SetEase(DG.Tweening.Ease.OutQuad);
                yield return new WaitForSeconds(duration);
            }

            UnityEngine.Object.Destroy(DOTween.instance);
            
            DeleteTweeners(tweeners);
            tweeners = CreateTweeners(count);
            GC.Collect();
            
            yield return new WaitForSecondsRealtime(Cooldown);
            
            LeanTween.init(count);
            
            yield return new WaitForSecondsRealtime(Cooldown);
            
            using (Measure.Frames().Scope(LeanTweenGroup))
            {
                foreach (var tween in tweeners)
                    tween.gameObject.LeanMoveLocal(end, duration).setEaseOutQuad();
                yield return new WaitForSeconds(duration);
            }
            
            DeleteTweeners(tweeners);
            tweeners = CreateTweeners(count);
            
            yield return new WaitForSecondsRealtime(Cooldown);
            GC.Collect();
            yield return new WaitForSecondsRealtime(Cooldown);
            
            using (Measure.Frames().Scope(UnityTweenGroup))
            {
                foreach (var tween in tweeners)
                    tween.gameObject.TweenLocalPosition(end, duration).SetEaseQuadOut();
                yield return new WaitForSeconds(duration);
            }
            
            DeleteTweeners(tweeners);
            tweeners = CreateTweeners(count);
            
            yield return new WaitForSecondsRealtime(Cooldown);
            GC.Collect();
            yield return new WaitForSecondsRealtime(Cooldown);
            
            using (Measure.Frames().Scope(AnimeTaskGroup))
            {
                foreach (var tween in tweeners)
                    Easing.Create<OutQuad>(start, end, duration).ToLocalPosition(tween).Forget();
                yield return new WaitForSeconds(duration);
            }
        }

        private Tweener[] CreateTweeners(int count)
        {
            var tweeners = new Tweener[count];
            for (int i = 0; i < tweeners.Length; i++)
            {
                tweeners[i] = Tweener.Create();
                
                // Warm up the transform property
                _ = tweeners[i].transform;
            }
            return tweeners;
        }

        private void DeleteTweeners(Tweener[] tweeners)
        {
            foreach (var tweener in tweeners)
                tweener.Delete();
        }
    }
}
