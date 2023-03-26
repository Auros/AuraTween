using System;
using System.Collections;
using AnimeTask;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using ElRaccoone.Tweens;
using JetBrains.Annotations;
using Unity.PerformanceTesting;
using UnityEngine;
using UnityEngine.TestTools;

namespace AuraTween.Benchmarks
{
    [PublicAPI]
    public class TweenComparisonTests
    {
        private const int N = 10_000;

        public const string AuraTweenGroup = "AuraTween";
        public const string DoTweenGroup = "DOTween";
        public const string LeanTweenGroup = "LeanTween";
        public const string UnityTweenGroup = "Unity Tweens";
        public const string AnimeTaskGroup = "AnimeTask";

        private readonly struct TestVector
        {
            public readonly float x;
            public readonly float y;
            public readonly float z;

            public TestVector(float x)
            {
                this.x = x;
                y = x;
                z = x;
            }
        }

        private static TestVector Bruh(TestVector s, TestVector e, float time)
        {
            return new TestVector(s.x + time);
        }
        
        [UnityTest, Performance]
        public IEnumerator Test()
        {
            Vector3 start = default;
            var end = Vector3.one;
            const float duration = 0.5f;

            var manager = Helper.Manager;

            var tweeners = CreateTweeners(10000);
            
            GC.Collect();
            yield return new WaitForSecondsRealtime(1);
            
            using (Measure.Frames().Scope(AuraTweenGroup))
            {
                foreach (var tween in tweeners)
                {
                    manager.Run(start, end, duration, v => tween.transform.localPosition = v, Easer.OutQuad, tween);
                    //manager.Run(new TestVector(), new TestVector(), duration,
                    //    v => tween.transform.localPosition = new Vector3(v.x, 0f, 0f), Easer.OutQuad, Bruh, tween);
                }
                yield return new WaitForSeconds(duration);
            }
            
            DeleteTweeners(tweeners);
            tweeners = CreateTweeners(10000);
            UnityEngine.Object.Destroy(manager);
 
            GC.Collect();
            yield return new WaitForSecondsRealtime(1);

            DOTween.SetTweensCapacity(10001, 50);
            
            using (Measure.Frames().Scope(DoTweenGroup))
            {
                foreach (var tween in tweeners)
                    tween.transform.DOLocalMove(end, duration).SetEase(DG.Tweening.Ease.OutQuad);
                yield return new WaitForSeconds(duration);
            }
            
            DeleteTweeners(tweeners);
            tweeners = CreateTweeners(10000);
            
            GC.Collect();
            yield return new WaitForSecondsRealtime(1);
            
            LeanTween.init(10000);
            
            using (Measure.Frames().Scope(LeanTweenGroup))
            {
                foreach (var tween in tweeners)
                    tween.gameObject.LeanMoveLocal(end, duration).setEaseOutQuad();
                yield return new WaitForSeconds(duration);
            }
            
            DeleteTweeners(tweeners);
            tweeners = CreateTweeners(10000);
            
            GC.Collect();
            yield return new WaitForSecondsRealtime(1);
            
            using (Measure.Frames().Scope(UnityTweenGroup))
            {
                foreach (var tween in tweeners)
                    tween.gameObject.TweenLocalPosition(end, duration).SetEaseQuadOut();
                yield return new WaitForSeconds(duration);
            }
            
            GC.Collect();
            yield return new WaitForSecondsRealtime(1);
            
            using (Measure.Frames().Scope(AnimeTaskGroup))
            {
                foreach (var tween in tweeners)
                    AnimeTask.Easing.Create<AnimeTask.OutQuad>(start, end, duration).ToLocalPosition(tween).Forget();
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
