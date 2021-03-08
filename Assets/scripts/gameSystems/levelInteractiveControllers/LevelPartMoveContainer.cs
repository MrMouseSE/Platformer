using UnityEngine;

namespace gameSystems.levelInteractiveControllers
{
    public class LevelPartMoveContainer : LevelPartContainer
    {
        public Transform InteractiveTransform;
        
        [Space]
        public Vector3 MoveFrom;
        [SerializeField]
        public Vector3 MoveTo;

        [Space]
        public float Speed;
        public AnimationCurve AnimationCurve;
        public bool PingPong;
    }
}