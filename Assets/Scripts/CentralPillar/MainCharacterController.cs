using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MetroidMaze.Character;

namespace PillarRolling
{
    public class MainCharacterController : MonoBehaviour
    {
        [SerializeField]
        private Rigidbody characterRigidBody;
        [SerializeField]
        private ObstacleCollider leftObstacleCollider;
        [SerializeField]
        private ObstacleCollider rightObstacleCollider;

        public CharacterMover Mover { get; set; }
        public Rigidbody CharacterRigidbody => characterRigidBody;
        public ObstacleCollider LeftObstacleCollider => leftObstacleCollider;
        public ObstacleCollider RightObstacleCollider => rightObstacleCollider;
    }
}
