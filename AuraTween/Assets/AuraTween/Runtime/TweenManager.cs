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
        private readonly List<TweenContext> _activeContexts = new();
        private readonly Dictionary<long, TweenContext> _activeContextLookup = new();

        private readonly ObjectPool<TweenContext> _contextPool = new(() => new TweenContext(), ClearContext, ClearContext, ClearContext);

        public Tween Run(TweenOptions options)
        {
            var handle = new Tween(this);
            var ctx = _contextPool.Get();
            ctx.Options = options;
            ctx.Id = handle.Id;
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
            var options = ctx!.Options;
            options.OnCancel = cancel;
            ctx.Options = options;
        }

        internal void SetOnComplete(Tween tween, Action complete)
        {
            var ctx = GetContext(tween);
            var options = ctx!.Options;
            options.OnComplete = complete;
            ctx.Options = options;
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
                
                var options = ctx.Options;
                
                // Do not progress the tween if its paused.
                if (!ctx.Paused)
                    ctx.Progress += time;
                
                // Check if the tween has been completed.
                if (ctx.Progress >= options.Duration || options.Duration == 0)
                {
                    // Tween was completed. Remove from active, invoke necessary events, and cleanup.
                    _activeContextLookup.Remove(ctx.Id);
                    _activeContexts.Remove(ctx);
                    _contextPool.Release(ctx);
                    options.Updater(1f); // Force the updater to be "1" to re-evaluate its value in case we go over.
                    options.OnComplete?.Invoke();
                    continue;
                }

                if (ctx.WantsToCancel)
                {
                    _activeContextLookup.Remove(ctx.Id);
                    _activeContexts.Remove(ctx);
                    _contextPool.Release(ctx);
                    options.OnCancel?.Invoke();
                    continue;
                }

                var easer = ctx.Options.Interpolator;
                var progress = ctx.Progress / options.Duration;
                options.Updater(easer(progress));
            }
        }

        private static void ClearContext(TweenContext ctx)
        {
            ctx.Id = -1;
            ctx.Progress = 0;
            ctx.WantsToCancel = false;
            ctx.Paused = false;
            ctx.Options = default;
        }
    }
}