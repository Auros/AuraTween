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

        public void Stop()
        {
            ThrowIfInvalid();
            _tweenManager.CancelTween(this);
        }

        private void ThrowIfInvalid()
        {
            if (!_tweenManager)
                throw new MissingTweenManagerException(this);
        }
    }
}