using UnityEngine;

namespace AuraTween
{
    public class PhysicalEaseRenderer : MonoBehaviour
    {
        [SerializeField]
        private GameObject _point;
        
        private void Start()
        {
            for (int i = 0; i < 100; i++)
            {
                var time = i * 0.01f;
                var value = Easer.InOutElastic(0, 1f, time);
                var point = Instantiate(_point, transform);
                point.transform.localPosition = new Vector3(time, value, 0f);
            }
        }
    }
}