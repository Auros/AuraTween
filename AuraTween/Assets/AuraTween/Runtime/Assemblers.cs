using UnityEngine;

namespace AuraTween
{
    internal static class Assemblers
    {
        public static Color Color(ref Color start, ref Color end, ref float time)
        {
            return UnityEngine.Color.Lerp(start, end, time);
        }
        
        public static float Float(ref float start, ref float end, ref float time)
        {
            return Mathf.Lerp(start, end, time);
        }
        
        public static Pose Pose(ref Pose start, ref Pose end, ref float time)
        {
            var pos = UnityEngine.Vector3.Lerp(start.position, end.position, time);
            var rot = UnityEngine.Quaternion.Lerp(start.rotation, end.rotation, time);
            return new Pose(pos, rot);
        }
        
        public static Quaternion Quaternion(ref Quaternion start, ref Quaternion end, ref float time)
        {
            return UnityEngine.Quaternion.Lerp(start, end, time);
        }
        
        public static Vector3 Vector3(ref Vector3 start, ref Vector3 end, ref float time)
        {
            return new Vector3(
                Easer.FastLinear(ref start.x, ref end.x, ref time),
                Easer.FastLinear(ref start.y, ref end.y, ref time),
                Easer.FastLinear(ref start.z, ref end.z, ref time)
            );
        }
        
        public static Vector2 Vector2(ref Vector2 start, ref Vector2 end, ref float time)
        {
            return UnityEngine.Vector2.Lerp(start, end, time);
        }
    }
}