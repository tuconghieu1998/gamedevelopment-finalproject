using UnityEngine;

namespace Shared.Extensions
{
    public static class TransfromExtensions
    {
        /// <summary>
        /// Kiểm tra mục tiêu có bị khuất tầm nhìn không
        /// </summary>
        /// <param name="origin">Transform origin</param>
        /// <param name="target"></param>
        /// <param name="fieldOfView"></param>
        /// <param name="collisionMask"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public static bool IsInLineOfSight(this Transform origin, Vector3 target, float fieldOfView, LayerMask collisionMask, Vector3 offset)
        {
            Vector3 direction = target - origin.position;
            if (Vector3.Angle(origin.forward, direction.normalized) < fieldOfView / 2)
            {
                float distanceToTarget = Vector3.Distance(origin.position, target);

                //bị cản tầm nhìn
                if (Physics.Raycast(origin.position + offset + origin.forward * .3f, direction.normalized, distanceToTarget, collisionMask))
                {
                    return false;
                }
                return true;
            }
            return false;
        }
    }
}
