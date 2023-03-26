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
            Vector3 start = default;
            var end = Vector3.one;
            const float duration = 20f;
            
            var gos = new GameObject[10000];
            for (int i = 0; i < gos.Length; i++)
            {
                gos[i] = new GameObject(i.ToString());
                
                // Prewarm transforms
                _ = gos[i].transform;
            }

            yield return new WaitForSecondsRealtime(5f);
            
            foreach (var tween in gos)
                _tweenManager.Run(start, end, duration, v => tween.transform.localPosition = v, Easer.InQuad);
        }

        // ReSharper disable once Unity.IncorrectMethodSignature
        /*private async UniTaskVoid Start()
        {
            var mat = transform.GetComponent<Renderer>().material;
            
            await _tweenManager.Run(Color.red, Color.cyan, 5f, v => mat.color = v,
                Ease.InOutCubic.ToInterpolator(), HSV, this);
        }*/

        /*private static Action<float> HSV(Color start, Color end, Action<Color> updater)
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
        }*/
    }
}