using gameSystems.levelInteractiveControllers;
using UnityEngine;

namespace gameSystems.levelGenerator
{
    public class LevelGeneratorPartContainer : MonoBehaviour
    {
        public int LevelPartComplexity;
        
        [Space]
        public Transform LeftJoint;
        public Transform RightJoint;

        [Space]
        public LevelPartContainer InteractiveContainer;

        public Vector3 CalculateInstantiatePosition(Vector3 position)
        {
            return transform.position = position - LeftJoint.localPosition;
        }
    }
}