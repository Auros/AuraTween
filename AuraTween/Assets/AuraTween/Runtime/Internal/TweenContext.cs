namespace AuraTween.Internal
{
    internal class TweenContext
    {
        public long Id { get; set; }
        
        public float Progress { get; set; }
        
        public bool WantsToCancel { get; set; }
        
        public bool Paused { get; set; }
        
        public TweenOptions Options { get; set; }
    }
}