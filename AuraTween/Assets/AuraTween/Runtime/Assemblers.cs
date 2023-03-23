using System;
using UnityEngine;

namespace AuraTween
{
    internal static class Assemblers
    {
        public static Action<float> Color(Color start, Color end, Action<Color> updater)
        {
            return time => updater(UnityEngine.Color.Lerp(start, end, time));
        }
        
        public static Action<float> Float(float start, float end, Action<float> updater)
        {
            return time => updater(Mathf.Lerp(start, end, time));
        }
        
        public static Action<float> Pose(Pose start, Pose end, Action<Pose> updater)
        {
            return time =>
            {
                var pos = UnityEngine.Vector3.Lerp(start.position, end.position, time);
                var rot = UnityEngine.Quaternion.Lerp(start.rotation, end.rotation, time);
                updater(new Pose(pos, rot));
            };
        }
        
        public static Action<float> Quaternion(Quaternion start, Quaternion end, Action<Quaternion> updater)
        {
            return time => updater(UnityEngine.Quaternion.Lerp(start, end, time));
        }
        
        public static Action<float> Vector3(Vector3 start, Vector3 end, Action<Vector3> updater)
        {
            return time => updater(UnityEngine.Vector3.Lerp(start, end, time));
        }
        
        public static Action<float> Vector2(Vector2 start, Vector2 end, Action<Vector2> updater)
        {
            return time => updater(UnityEngine.Vector2.Lerp(start, end, time));
        }
    }
}