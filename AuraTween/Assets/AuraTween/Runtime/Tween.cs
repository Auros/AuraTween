using System;
using AuraTween.Exceptions;

namespace AuraTween
{
    /// <summary>
    /// The tween handle.
    /// </summary>
    public readonly struct Tween
    {
        internal readonly long Id;
        private static long _idIncrementer;
        private readonly TweenManager _tweenManager;

        public bool IsAlive => _tweenManager.IsTweenActive(this);

        internal Tween(TweenManager tweenManager)
        {
            Id = _idIncrementer++;
            _tweenManager = tweenManager;
        }

        public void Start()
        {
            ThrowIfInvalid();
            _tweenManager.PlayTween(this);
        }

        public void Pause()
        {
            ThrowIfInvalid();
            _tweenManager.PauseTween(this);
        }

        public void Restart()
        {
            ThrowIfInvalid();
            _tweenManager.ResetTween(this);
        }

        public void Cancel()
        {
            ThrowIfInvalid();
            _tweenManager.CancelTween(this);
        }

        public void SetOnCancel(Action cancel)
        {
            ThrowIfInvalid();
            _tweenManager.SetOnCancel(this, cancel);
        }

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
    }
}