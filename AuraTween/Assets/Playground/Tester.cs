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
            yield return new WaitForSeconds(1);
            var mat = transform.GetComponent<Renderer>().material;
            
            _tweenManager.Run(
                Vector3.zero, 
                Vector3.one * 5, 
                5f, 
                v => transform.localPosition = v,
                Easer.InOutCubic);

            _tweenManager.Run(
                Color.red, 
                Color.cyan, 
                5f, 
                v => mat.color = v,
                Ease.InOutCubic.ToInterpolator());
        }
    }
}