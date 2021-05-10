using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.ExceptionServices;
using UnityEditor;
using UnityEngine;

namespace MetroidMaze.Character
{
    public class RaycastCollisionDetector : MonoBehaviour
    {
        protected Transform _transform;
        [SerializeField]
        protected Vector3 raycastDirection;
        [SerializeField]
        protected float radius;
        [SerializeField]
        protected float raycastDistance;
        [SerializeField]
        private int rayCount = 3;
        [SerializeField]
        protected int playerLayer = 8;

        private int playerLayerMask;
        private Vector3 originBackshift;

        [System.Serializable]
        protected struct RaycastCoords
        {
            public Vector3 from;
            public Vector3 to;
        }
        private RaycastCoords[] raycasts;
        public bool HasCollision => GetRaycastedCollisions();
        private void Awake()
        {
            _transform = transform;
            //radiusDelta = PlanarNormalVector(raycastDirection);
            CalculateRaycasts();
        }

        void CalculateRaycasts()
        {
            Vector3 normalizedV = raycastDirection.normalized;
            Vector3 rayVector = normalizedV * raycastDistance;
            Vector3 raycastStart = new Vector3(-normalizedV.y, normalizedV.x, 0) * radius;
            Vector3 radiusDelta = raycastStart / (rayCount - 1) * -2f;
            originBackshift = -normalizedV * raycastDistance / 2;
            raycasts = new RaycastCoords[rayCount];
            for (int i = 0; i < rayCount; ++i)
            {
                raycasts[i].from = raycastStart + radiusDelta * i;
                raycasts[i].to = raycasts[i].from + rayVector;
            }
        }

        /*
        /// <summary>
        /// Calculates a vector which is normal to the given vector in the same z-plane
        /// The result is rotated 90 degrees counterclockwise
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        Vector3 PlanarNormalVector(Vector3 v)
        {
            Vector3 normalizedV = v.normalized;
            Vector3 result = new Vector3(-normalizedV.y, normalizedV.x, normalizedV.z) * v.magnitude;
            return result;
        }
        */
        /*
        private void OnDrawGizmos()
        {
            CalculateRaycasts();
            for (int i = 0; i < rayCount; ++i)
            {
                Gizmos.DrawLine(raycasts[i, 0], raycasts[i, 1]);
            }
        }
        */
        private void OnDrawGizmosSelected()
        {
            CalculateRaycasts();
            Vector3 origin = transform.position + originBackshift;
            for (int i = 0; i < rayCount; ++i)
            {
                Gizmos.DrawLine(raycasts[i].from + origin, raycasts[i].to + origin);
                Gizmos.DrawSphere(raycasts[i].from + origin, 0.1f);
                Gizmos.DrawRay(raycasts[i].from + origin, raycasts[i].to - raycasts[i].from);
            }
        }

        private bool GetRaycastedCollisions()
        {
            Vector3 origin = _transform.position + originBackshift;
            int hitCount = 0;
            bool CheckRaycast(int i)
            {
                //bool hit = Physics.Raycast(raycasts[i].from + origin, raycasts[i].to + origin, raycastDistance);
                //return Physics.Raycast(raycasts[i].from + origin, raycasts[i].to + origin, raycastDistance, playerLayerMask, QueryTriggerInteraction.Ignore);
                bool hit = Physics.Raycast(raycasts[i].from + origin, raycasts[i].to + origin, out RaycastHit info, raycastDistance, playerLayerMask, QueryTriggerInteraction.Ignore);
                if (hit)
                {
                    Debug.Log($"Hit object {info.collider.gameObject.name}");
                    if (info.collider.CompareTag("Player"))
                    {
                        hit = false;
                    }
                }
                Debug.DrawRay(raycasts[i].from + origin, raycasts[i].to - raycasts[i].from, hit ? Color.cyan : Color.red);
                return hit;
            }
            for (int i = 0; i < rayCount; ++i)
            {
                if (CheckRaycast(i))
                {
                    ++hitCount;
                }
            }
            return hitCount > rayCount / 2;
        }
    }
}
