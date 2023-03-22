using UnityEngine;

namespace AuraTween
{
    public class Tester : MonoBehaviour
    {
        [SerializeField]
        private TweenManager _tweenManager;

        private void Start()
        {
            _tweenManager.Run(Vector3.zero, Vector3.one * 5, 5f, v => transform.localPosition = v, Ease.InOutQuart);
        }
    }
}