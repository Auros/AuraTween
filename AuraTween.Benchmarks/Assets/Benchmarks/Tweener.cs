using UnityEngine;

namespace AuraTween.Benchmarks
{
    public class Tweener : MonoBehaviour
    {
        public static Tweener Create()
        {
            GameObject gameObject = new(nameof(Tweener));
            var tweener = gameObject.AddComponent<Tweener>();
            return tweener;
        }

        public void Delete()
        {
            Destroy(gameObject);
        }
    }
}