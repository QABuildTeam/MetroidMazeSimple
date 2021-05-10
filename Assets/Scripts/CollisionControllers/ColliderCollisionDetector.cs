using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

namespace MetroidMaze.Character
{
    public class ColliderCollisionDetector : MonoBehaviour
    {
        [SerializeField]
        protected bool debugOutput = false;
        protected int MaxSameValueCounter { get; } = 2;
        protected Collider thisCollider;
        protected int sameValueCounter = 0;
        protected int colliderEnterCounter = 0;

        public bool HasCollision { get; protected set; } = false;
        /// <summary>
        /// A distance from the center of the bound collider to the nearest point of collision (if any).
        /// Only valid if HasCollision == true
        /// </summary>
        public Vector3 Distance { get; protected set; } = new Vector3(float.PositiveInfinity, float.PositiveInfinity, 0);

        public string CollisionTag { get; protected set; } = string.Empty;


        private void Awake()
        {
            thisCollider = GetComponent<Collider>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.isTrigger && !other.CompareTag("Player"))
            {
                ++colliderEnterCounter;
                SetDistance(other);
                CollisionTag = other.gameObject.tag;
            }
            SetHasCollision(colliderEnterCounter > 0);
            if (debugOutput)
            {
                Debug.Log($"[{GetType().Name}.{nameof(OnTriggerEnter)}] other: name={other.name}, tag={other.tag}, trigger={other.isTrigger}, counter inc = {colliderEnterCounter}, collision={HasCollision}");
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.isTrigger && !other.CompareTag("Player"))
            {
                --colliderEnterCounter;
                SetDistance(other);
            }
            SetHasCollision(colliderEnterCounter > 0);
            if (debugOutput)
            {
                Debug.Log($"[{GetType().Name}.{nameof(OnTriggerExit)}] other: name={other.name}, tag={other.tag}, trigger={other.isTrigger}, counter dec = {colliderEnterCounter}, collision={HasCollision}");
            }
        }

        protected virtual void SetHasCollision(bool newValue)
        {
            HasCollision = newValue;
            /*
            if (newValue != HasCollision)
            {
                ++sameValueCounter;
                if (sameValueCounter >= MaxSameValueCounter)
                {
                    HasCollision = newValue;
                    sameValueCounter = 0;
                }
            }
            else
            {
                sameValueCounter = 0;
            }
            */
        }

        protected virtual void SetDistance(Collider other) { }
    }
}
