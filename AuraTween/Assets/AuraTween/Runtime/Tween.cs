using System;
using AuraTween.Exceptions;
using JetBrains.Annotations;

namespace AuraTween
{
    /// <summary>
    /// The tween handle.
    /// </summary>
    [PublicAPI]
    public readonly struct Tween
    {
        internal readonly long Id;
        private static long _idIncrementer;
        private readonly TweenManager _tweenManager;

        /// <summary>
        /// Is this tween alive? The tween can be alive even ifs been paused.
        /// </summary>
        public bool IsAlive => _tweenManager.IsTweenActive(this);

        internal Tween(TweenManager tweenManager)
        {
            Id = _idIncrementer++;
            _tweenManager = tweenManager;
        }

        /// <summary>
        /// Starts this tween. This acts as an unpause.
        /// </summary>
        /// <exception cref="MissingTweenException">Occurrs if this tween doesn't exist or no longer exists.</exception>
        /// <exception cref="MissingTweenManagerException">Occurrs if this tween is misconfigured or the TweenManager that created it has been destroyed.</exception>
        public void Start()
        {
            ThrowIfInvalid();
            _tweenManager.PlayTween(this);
        }

        /// <summary>
        /// Pauses this tween.
        /// </summary>
        /// <exception cref="MissingTweenException">Occurrs if this tween doesn't exist or no longer exists.</exception>
        /// <exception cref="MissingTweenManagerException">Occurrs if this tween is misconfigured or the TweenManager that created it has been destroyed.</exception>
        public void Pause()
        {
            ThrowIfInvalid();
            _tweenManager.PauseTween(this);
        }

        /// <summary>
        /// Restarts this tween.
        /// </summary>
        /// <exception cref="MissingTweenException">Occurrs if this tween doesn't exist or no longer exists.</exception>
        /// <exception cref="MissingTweenManagerException">Occurrs if this tween is misconfigured or the TweenManager that created it has been destroyed.</exception>
        public void Restart()
        {
            ThrowIfInvalid();
            _tweenManager.ResetTween(this);
        }

        /// <summary>
        /// Cancel's this tween.
        /// </summary>
        /// <exception cref="MissingTweenException">Occurrs if this tween doesn't exist or no longer exists.</exception>
        /// <exception cref="MissingTweenManagerException">Occurrs if this tween is misconfigured or the TweenManager that created it has been destroyed.</exception>
        public void Cancel()
        {
            ThrowIfInvalid();
            _tweenManager.CancelTween(this);
        }

        /// <summary>
        /// Set the event that gets called when this tween is canceled.
        /// </summary>
        /// <exception cref="MissingTweenException">Occurrs if this tween doesn't exist or no longer exists.</exception>
        /// <exception cref="MissingTweenManagerException">Occurrs if this tween is misconfigured or the TweenManager that created it has been destroyed.</exception>
        public void SetOnCancel(Action cancel)
        {
            ThrowIfInvalid();
            _tweenManager.SetOnCancel(this, cancel);
        }

        /// <summary>
        /// Set the event that gets called when this tween finishes (does not include cancellation).
        /// </summary>
        /// <exception cref="MissingTweenException">Occurrs if this tween doesn't exist or no longer exists.</exception>
        /// <exception cref="MissingTweenManagerException">Occurrs if this tween is misconfigured or the TweenManager that created it has been destroyed.</exception>
        public void SetOnComplete(Action complete)
        {
            ThrowIfInvalid();
            _tweenManager.SetOnComplete(this, complete);
        }

        private void ThrowIfInvalid()
        {
            if (!_tweenManager)
                throw new MissingTweenManagerException(this);

            if (!_tweenManager.IsTweenActive(this))
                throw new MissingTweenException(this);
        }

#if AURATWEEN_UNITASK_SUPPORT
        public Cysharp.Threading.Tasks.UniTask.Awaiter GetAwaiter()
        {
            return CompletionTask().GetAwaiter();
        }

        private async Cysharp.Threading.Tasks.UniTask CompletionTask()
        {
            while (IsAlive)
                await Cysharp.Threading.Tasks.UniTask.Yield();
        }
#endif
    }
}