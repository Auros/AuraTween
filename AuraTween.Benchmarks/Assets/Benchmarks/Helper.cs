using UnityEngine;

namespace AuraTween.Benchmarks
{
    public class Helper
    {
        public static TweenManager Manager
        {
            get
            {
                GameObject gameObject = new("AuraTweenManager");
                gameObject.SetActive(false);
                var manager = gameObject.AddComponent<TweenManager>();
                manager.enabled = false;
                gameObject.SetActive(true);
                return manager;
            }
        }
    }
}