using System;
using System.Collections.Generic;
using AuraTween.Internal;
using UnityEngine;
using UnityEngine.Pool;

namespace AuraTween
{
    public class TweenManager : MonoBehaviour
    {
        // We store the contexts in both a list and dictionary.
        // The list is so we can loop over every active context without
        // allocating any garbage. The dictionary is so we can have O(1) perf
        // when looking for contexts from the individual tweens.
        private readonly List<TweenContext> _activeContexts = new(10_000);
        private readonly Dictionary<long, TweenContext> _activeContextLookup = new(10_000);
        private ObjectPool<TweenContext> _contextPool = null!;

        private void Awake()
        {
            _contextPool = new ObjectPool<TweenContext>(() => new TweenContext(), ClearContext, ClearContext, ClearContext, false, 10_000);
            
            // Warm up the pool
            var contexts = new TweenContext[10_000];
            for (int i = 0; i < contexts.Length; i++)
                contexts[i] = _contextPool.Get();
            for (int i = 0; i < contexts.Length; i++)
                _contextPool.Release(contexts[i]);
        }

        private void OnDestroy()
        {
            _contextPool.Dispose();
        }

        public Tween Run(TweenOptions options)
        {
            var handle = new Tween(this);
            var ctx = _contextPool.Get();
            ctx.Id = handle.Id;
            ctx.Updater = options.Updater;
            ctx.OnCancel = options.OnCancel;
            ctx.Duration = options.Duration;
            ctx.Lifetime = options.Lifetime;
            ctx.OnComplete = options.OnComplete;
            ctx.Interpolator = options.Interpolator;
            AddContext(ctx);
            return handle;
        }

        internal bool IsTweenActive(Tween tween) => _activeContextLookup.ContainsKey(tween.Id);
        
        internal void PlayTween(Tween tween)
        {
            var ctx = GetContext(tween);
            ctx!.Paused = false;
        }
        
        internal void PauseTween(Tween tween)
        {
            var ctx = GetContext(tween);
            ctx!.Paused = true;
        }
        
        internal void ResetTween(Tween tween)
        {
            var ctx = GetContext(tween);
            ctx!.Progress = 0f;
        }
        
        internal void CancelTween(Tween tween)
        {
            var ctx = GetContext(tween);
            if (ctx is null)
                return;

            ctx.WantsToCancel = true;
        }
        
        internal void SetOnCancel(Tween tween, Action cancel)
        {
            var ctx = GetContext(tween);
            ctx!.OnCancel = cancel;
        }

        internal void SetOnComplete(Tween tween, Action complete)
        {
            var ctx = GetContext(tween);
            ctx!.OnComplete = complete;
        }

        private TweenContext? GetContext(Tween tween) => _activeContextLookup.TryGetValue(tween.Id, out var ctx) ? ctx : null;
        
        private void AddContext(TweenContext ctx)
        {
            _activeContexts.Add(ctx);
            _activeContextLookup[ctx.Id] = ctx;
        }

        private void Update()
        {
            var time = Time.deltaTime;
            // Iterate over every active context in reverse order
            // so we can remove them if they become if they're invalid.
            for (int i = _activeContexts.Count - 1; i >= 0; i--)
            {
                var ctx = _activeContexts[i];
                
                // Do not progress the tween if its paused.
                if (!ctx.Paused)
                    ctx.Progress += time;

                var lifetimeExpired = ctx.Lifetime != null && !ctx.Lifetime();
                if (ctx.WantsToCancel || lifetimeExpired)
                {
                    _activeContextLookup.Remove(ctx.Id);
                    _activeContexts.Remove(ctx);
                    _contextPool.Release(ctx);
                    
                    // We only want to invoke the cancellation event if it was intended.
                    if (lifetimeExpired)
                        continue;
                    
                    ctx.OnCancel?.Invoke();
                }
                
                // Check if the tween has been completed.
                if (ctx.Progress >= ctx.Duration || ctx.Duration == 0)
                {
                    // Tween was completed. Remove from active, invoke necessary events, and cleanup.
                    _activeContextLookup.Remove(ctx.Id);
                    _activeContexts.Remove(ctx);
                    _contextPool.Release(ctx);
                    ctx.Updater?.Invoke(1f); // Force the updater to be "1" to re-evaluate its value in case we go over.
                    ctx.OnComplete?.Invoke();
                    continue;
                }

                var easer = ctx.Interpolator;
                var progress = ctx.Progress / ctx.Duration;
                ctx.Updater?.Invoke(easer!(progress)); 
            }
        }

        private static void ClearContext(TweenContext ctx)
        {
            ctx.Id = -1;
            ctx.Progress = 0;
            ctx.WantsToCancel = false;
            ctx.Paused = false;
            ctx.OnCancel = null;
            ctx.OnComplete = null;
            ctx.Updater = null!;
            ctx.Duration = 0;
            ctx.Interpolator = null!;
            ctx.Lifetime = null;
        }
    }
}