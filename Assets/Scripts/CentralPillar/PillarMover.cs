using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MetroidMaze.Character;

namespace MetroidMaze
{
    public class PillarMover : CharacterMover
    {
        [SerializeField]
        private GameObject centralPilar;
        [SerializeField]
        private Rigidbody characterRigidbody;
        [SerializeField]
        private ObstacleCollider leftObstacleCollider;
        [SerializeField]
        private ObstacleCollider rightObstacleCollider;
        [SerializeField]
        private string watchTag = "Obstacle";

        public override void MoveCharacter(Vector3 moveDelta)
        {
            Vector3 characterPosition = characterRigidbody.transform.position;
            Vector2 characterPlanePosition = new Vector2(characterPosition.x, characterPosition.z);
            Vector3 pillarPosition = centralPilar.transform.position;
            Vector2 pillarPlanePosition = new Vector2(pillarPosition.x, pillarPosition.z);

            float radius = Vector2.Distance(characterPlanePosition, pillarPlanePosition);
            float degrees = Mathf.Atan2(moveDelta.x, radius) * Mathf.Rad2Deg;

            Vector3 characterMove = new Vector3(0, moveDelta.y, 0);
            Transform characterParentTransform = characterRigidbody.transform.parent;

            bool PathClear(ObstacleCollider collider)
            {
                return collider == null || !collider.HasCollision || collider.CollisionTag != watchTag;
            }

            if ((PathClear(leftObstacleCollider) && moveDelta.x < 0) ||
                (PathClear(rightObstacleCollider) && moveDelta.x >= 0))
            {
                characterParentTransform.transform.RotateAround(pillarPosition, Vector3.down, degrees);
            }
            characterRigidbody.MovePosition(characterRigidbody.transform.position + characterMove);
        }
    }
}
