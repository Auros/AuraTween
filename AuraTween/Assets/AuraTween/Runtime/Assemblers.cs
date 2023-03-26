using UnityEngine;

namespace AuraTween
{
    internal static class Assemblers
    {
        public static Color Color(Color start, Color end, float time)
        {
            return UnityEngine.Color.Lerp(start, end, time);
        }
        
        public static float Float(float start, float end, float time)
        {
            return Mathf.Lerp(start, end, time);
        }
        
        public static Pose Pose(Pose start, Pose end, float time)
        {
            var pos = UnityEngine.Vector3.Lerp(start.position, end.position, time);
            var rot = UnityEngine.Quaternion.Lerp(start.rotation, end.rotation, time);
            return new Pose(pos, rot);
        }
        
        public static Quaternion Quaternion(Quaternion start, Quaternion end, float time)
        {
            return UnityEngine.Quaternion.Lerp(start, end, time);
        }
        
        public static Vector3 Vector3(Vector3 start, Vector3 end, float time)
        {
            return UnityEngine.Vector3.Lerp(start, end, time);
        }
        
        public static Vector2 Vector2(Vector2 start, Vector2 end, float time)
        {
            return UnityEngine.Vector2.Lerp(start, end, time);
        }
    }
}