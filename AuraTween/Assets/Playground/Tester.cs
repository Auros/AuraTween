using UnityEngine;

namespace AuraTween
{
    public class Tester : MonoBehaviour
    {
        [SerializeField]
        private Renderer _renderer;
    
        [SerializeField]
        private float _duration = 2f;
    
        [SerializeField]
        private TweenManager _tweenManager;
    
        [SerializeField]
        private AnimationCurve _animationCurve;

        private void Start()
        {
            var myTransform = transform;
            var myMaterial = _renderer.material;

            // float
            //_tweenManager.Run(-5f, 5f, _duration, value => Debug.Log($"float: ${value}"), Easer.OutCubic, this);
        
            // Vector3
            var tween = _tweenManager.Run(Vector3.zero, new Vector3(0f, 5f, 0f), _duration, value => myTransform.localPosition = value, Easer.Linear, this);
            
            tween.SetOnComplete(() => Debug.Log("yippee"));
    
            // Pause a tween
            //tween.Pause();
    
            // Cancel a tween
            //tween.Cancel();
        
            // Custom types and or value calculation
            //_tweenManager.Run(Color.red, Color.cyan, _duration, value => myMaterial.color = value, Easer.OutElastic, HSV, this);
        
            // Custom easing methods
            //_tweenManager.Run(Quaternion.identity, Quaternion.Euler(new Vector3(0f, 90f, 0f)), _duration, value => myTransform.localRotation = value, CustomEaseWithAnimationCurve, this);
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
    
        private float CustomEaseWithAnimationCurve(ref float time)
        {
            return _animationCurve.Evaluate(time);
        }
    }
}