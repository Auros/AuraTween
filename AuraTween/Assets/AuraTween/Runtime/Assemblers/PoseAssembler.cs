using System;
using UnityEngine;

namespace AuraTween.Assemblers
{
    public struct PoseAssembler : ITweenAssembler<Pose>
    {
        public Action<float> Assemble(Pose start, Pose end, Action<Pose> updater)
        {
            return time =>
            {
                var pos = Vector3.Lerp(start.position, end.position, time);
                var rot = Quaternion.Lerp(start.rotation, end.rotation, time);
                updater(new Pose(pos, rot));
            };
        }
    }
}