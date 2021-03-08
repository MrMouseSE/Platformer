using UnityEngine;

namespace gameSystems.cameraSystems
{
    public class CameraContainer : MonoBehaviour
    {
        private Transform _fallowTransform;

        public Transform FallowTransform
        {
            get => _fallowTransform;
            set => _fallowTransform = value;
        }

        private void Update()
        {
            transform.position = Vector3.Lerp(transform.position,_fallowTransform.position, Time.deltaTime);    
        }
    }
}