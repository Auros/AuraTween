using System.Collections;
using UnityEngine;

namespace AuraTween
{
    public class Tester : MonoBehaviour
    {
        [SerializeField]
        private TweenManager _tweenManager;

        private IEnumerator Start()
        {
            yield return new WaitForSeconds(5);
            _tweenManager.Run(Vector3.zero, Vector3.one * 5, 0.5f, v => transform.localPosition = v, Ease.OutElastic);
        }
    }
}