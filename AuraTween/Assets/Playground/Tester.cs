using System;
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
                Ease.InOutCubic.ToInterpolator(),
                HSV);
        }

        private static Action<float> HSV(Color start, Color end, Action<Color> updater)
        {
            Color.RGBToHSV(start, out var startH, out var startS, out var startV);
            Color.RGBToHSV(end, out var endH, out var endS, out var endV);
            return time =>
            {
                var h = Mathf.Lerp(startH, endH, time);
                var s = Mathf.Lerp(startS, endS, time);
                var v = Mathf.Lerp(startV, endV, time);
                updater(Color.HSVToRGB(h, s, v));
            };
        }
    }
}