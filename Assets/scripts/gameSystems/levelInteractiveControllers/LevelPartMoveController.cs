using UnityEngine;

namespace gameSystems.levelInteractiveControllers
{
    public static class LevelPartMoveController
    {
        private static LevelPartMoveContainer _container;

        private static float _animationTime = 0f;
        private static bool _invertAnimation;

        public static void MoveInteractiveTransform(LevelPartMoveContainer container, float deltaTime)
        {
            _container = container;
            if (_container.PingPong)
            {
                if (_animationTime > 1 || _animationTime < 0) _invertAnimation = !_invertAnimation;
            }
            else
            {
                
                if (_animationTime > 1) _animationTime = 0;
            }
            _animationTime += deltaTime * _container.Speed * (_invertAnimation ? -1:1);
            MoveInteractiveTransform(_animationTime);
        }

        private static void MoveInteractiveTransform(float time)
        {
            float animationValue = _container.AnimationCurve.Evaluate(time);
            var position = Vector3.Lerp(_container.MoveFrom, _container.MoveTo, animationValue);
            Debug.DrawRay(_container.InteractiveTransform.position,Vector3.up);
            _container.InteractiveTransform.localPosition = position;
        }
    }
}