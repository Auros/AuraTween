using UnityEngine;

namespace AuraTween
{
    internal static class Assemblers
    {
        public static Color Color(ref Color start, ref Color end, ref float time)
        {
            return new Color(
                Easer.FastLinear(ref start.r, ref end.r, ref time),
                Easer.FastLinear(ref start.g, ref end.g, ref time),
                Easer.FastLinear(ref start.b, ref end.b, ref time),
                Easer.FastLinear(ref start.a, ref end.a, ref time)
            );
        }
        
        public static float Float(ref float start, ref float end, ref float time)
        {
            return Easer.FastLinear(ref start, ref end, ref time);
        }
        
        public static Pose Pose(ref Pose start, ref Pose end, ref float time)
        {
            var pos = Vector3(ref start.position, ref end.position, ref time);
            var rot = Quaternion(ref start.rotation, ref end.rotation, ref time);
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
            return new Vector3(
                Easer.FastLinear(ref start.x, ref end.x, ref time),
                Easer.FastLinear(ref start.y, ref end.y, ref time)
            );
        }
    }
}