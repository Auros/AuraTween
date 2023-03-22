using System.Collections.Generic;
using AuraTween.Internal;
using UnityEngine;

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

        public Tween Run(TweenOptions options)
        {
            var handle = new Tween();
            var ctx = new TweenContext
            {
                Id = handle.Id,
                Options = options
            };
            AddContext(ctx);
            return handle;
        }

        internal bool IsTweenActive(Tween tween) => _activeContextLookup.ContainsKey(tween.Id);
        
        internal void PlayTween(Tween tween)
        {
            var ctx = GetContext(tween);
            if (ctx is null)
                return;

            ctx.Paused = false;
        }
        
        internal void PauseTween(Tween tween)
        {
            var ctx = GetContext(tween);
            if (ctx is null)
                return;

            ctx.Paused = true;
        }
        
        internal void ResetTween(Tween tween)
        {
            var ctx = GetContext(tween);
            if (ctx is null)
                return;

            ctx.Progress = 0f;
        }
        
        internal void CancelTween(Tween tween)
        {
            var ctx = GetContext(tween);
            if (ctx is null)
                return;

            ctx.WantsToCancel = true;
        }
        
        private TweenContext? GetContext(Tween tween) =>
            _activeContextLookup.TryGetValue(tween.Id, out var ctx) ? ctx : null;
        
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
                    options.Updater(1f); // Force the updater to be "1" to re-evaluate its value in case we go over.
                    //options.OnComplete?.Invoke();
                    continue;
                }

                if (ctx.WantsToCancel)
                {
                    _activeContextLookup.Remove(ctx.Id);
                    _activeContexts.Remove(ctx);
                    //options.OnCanceled?.Invoke();
                    continue;
                }

                var progress = ctx.Progress / options.Duration;
                options.Updater(Easer.Ease(0f, 1f, progress, options.Ease));
            }
        }
    }
}