using UnityEngine;

namespace AuraTween
{
    public class Tester : MonoBehaviour
    {
        [SerializeField]
        private TweenManager _tweenManager;

        [SerializeField]
        private AnimationCurve _animationCurve;

        /*private IEnumerator Start()
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
        }*/

        // ReSharper disable once Unity.IncorrectMethodSignature
        private void Start()
        {
            var mat = transform.GetComponent<Renderer>().material;

            _tweenManager.Run(Vector3.zero, Vector3.one * 5f, 5f, v => transform.localPosition = v, Curve,
                this);
            
            _tweenManager.Run(Color.red, Color.cyan, 5f, v => mat.color = v,
                Ease.OutBounce.ToInterpolator(), HSV, this);
        }

        private static Color HSV(ref Color start, ref Color end, ref float time)
        {
            Color.RGBToHSV(start, out var startH, out var startS, out var startV);
            Color.RGBToHSV(end, out var endH, out var endS, out var endV);
            var h = Mathf.Lerp(startH, endH, time);
            var s = Mathf.Lerp(startS, endS, time);
            var v = Mathf.Lerp(startV, endV, time);
            return Color.HSVToRGB(h, s, v);
        }

        private float Curve(ref float @in) => _animationCurve.Evaluate(@in);
    }
}