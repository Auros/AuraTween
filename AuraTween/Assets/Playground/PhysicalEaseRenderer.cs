using UnityEngine;

namespace AuraTween
{
    public class PhysicalEaseRenderer : MonoBehaviour
    {
        [SerializeField]
        private GameObject _point;
        
        [SerializeField]
        private Ease _ease;
        
        private void Start()
        {
            var procedure = _ease.ToInterpolator();
            for (int i = 0; i < 100; i++)
            {
                var time = i * 0.01f;
                var value = procedure(ref time);
                var point = Instantiate(_point, transform);
                point.transform.localPosition = new Vector3(time, value, 0f);
            }
        }
    }
}