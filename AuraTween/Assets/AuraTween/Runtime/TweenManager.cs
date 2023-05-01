using System;
using System.Collections.Generic;
using AuraTween.Exceptions;
using AuraTween.Internal;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Pool;

namespace AuraTween
{
    [PublicAPI]
    [DefaultExecutionOrder(-5000)] // We want this to execute earlier. Our current design means if someone spawns a tween before .Start(), an exception will be thrown.
    public class TweenManager : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("This number of tweens are pre-allocated when the tween manager starts. This is only applied .Start() is called for this component, so make sure you set it before that point.")]
        private int _defaultTweenCapacity = 100;
        
        // We store the contexts in both a list and dictionary.
        // The list is so we can loop over every active context without
        // allocating any garbage. The dictionary is so we can have O(1) perf
        // when looking for contexts from the individual tweens.
        private List<TweenContext>? _activeContexts;
        private ObjectPool<TweenContext>? _contextPool;
        private Dictionary<long, TweenContext>? _activeContextLookup;

        private void Start()
        {
            _activeContexts = new List<TweenContext>(_defaultTweenCapacity);
            _activeContextLookup = new Dictionary<long, TweenContext>(_defaultTweenCapacity);
            _contextPool = new ObjectPool<TweenContext>(() => new TweenContext(), ClearContext, ClearContext, ClearContext, false, _defaultTweenCapacity, int.MaxValue);
            
            // Warm up the pool
            var contexts = new TweenContext[_defaultTweenCapacity];
            for (int i = 0; i < contexts.Length; i++)
                contexts[i] = _contextPool.Get();
            foreach (var ctx in contexts)
                _contextPool.Release(ctx);
        }
        
        private void OnDestroy()
        {
            _contextPool?.Dispose();
        }
        
        /// <summary>
        /// Sets the capacity of this <see cref="TweenManager"/>. This must be called before this component starts.
        /// </summary>
        /// <remarks>
        /// If you're instantiating the <see cref="TweenManager"/> programatically and need to set the capacity, you can use .SetActive(false)
        /// on the target <see cref="GameObject"/>, add the <see cref="TweenManager"/> to that <see cref="GameObject"/>, and then call <see cref="SetCapacity"/>.
        /// </remarks>
        /// <param name="size">The size of the default tween capacity.</param>
        public void SetCapacity(int size)
        {
            if (0 > size)
                size = 0;
            
            _defaultTweenCapacity = size;
        }

        /// <summary>
        /// Run a tween based on the provided options.
        /// </summary>
        /// <param name="options">The options of the tween.</param>
        /// <returns></returns>
        /// <exception cref="UninitializedTweenManagerException">Occurrs when this TweenManager has not been initialized. Make sure the component is active and enabled.</exception>
        public Tween Run(TweenOptions options)
        {
            if (_contextPool == null)
                throw new UninitializedTweenManagerException();
            
            var handle = new Tween(this);
            var ctx = _contextPool.Get();
            ctx.Id = handle.Id;
            ctx.Updater = options.Updater;
            ctx.Duration = options.Duration;
            ctx.Lifetime = options.Lifetime;
            ctx.Procedure = options.Procedure;
            AddContext(ctx);
            return handle;
        }

        internal bool IsTweenActive(Tween tween)
        {
            return _activeContextLookup != null && _activeContextLookup.ContainsKey(tween.Id);
        }
        
        internal void PlayTween(Tween tween)
        {
            var ctx = GetContext(tween);
            ctx!.Paused = false;
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
        
        internal void SetOnCancel(Tween tween, Action cancel)
        {
            var ctx = GetContext(tween);
            if (ctx is null)
                return;

            ctx.OnCancel = cancel;
        }

        internal void SetOnComplete(Tween tween, Action complete)
        {
            var ctx = GetContext(tween);
            if (ctx is null)
                return;

            ctx.OnComplete = complete;
        }

        private TweenContext? GetContext(Tween tween) => _activeContextLookup != null && _activeContextLookup.TryGetValue(tween.Id, out var ctx) ? ctx : null;
        
        private void AddContext(TweenContext ctx)
        {
            if (_activeContexts is null || _activeContextLookup is null)
                return;
            
            _activeContexts.Add(ctx);
            _activeContextLookup[ctx.Id] = ctx;
        }

        private void Update()
        {
            if (_contextPool is null || _activeContexts is null || _activeContextLookup is null)
                return;
            
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
                    ctx.Updater?.Invoke(1f); // Force the updater to be "1" to re-evaluate its value in case we go over.
                    ctx.OnComplete?.Invoke();
                    
                    // Tween was completed. Remove from active, invoke necessary events, and cleanup.
                    _activeContextLookup.Remove(ctx.Id);
                    _activeContexts.Remove(ctx);
                    _contextPool.Release(ctx);
                    continue;
                }

                var easer = ctx.Procedure;
                var progress = ctx.Progress / ctx.Duration;
                ctx.Updater?.Invoke(easer!(ref progress)); 
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
            ctx.Procedure = null!;
            ctx.Lifetime = null;
        }
    }
}