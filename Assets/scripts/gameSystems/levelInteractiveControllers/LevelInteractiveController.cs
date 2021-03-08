using System.Collections.Generic;
using UnityEngine;

namespace gameSystems.levelInteractiveControllers
{
    public class LevelInteractiveController
    {
        private List<LevelPartContainer> _dynamicLevelParts;
        private List<GameObject> _staticLevelParts;

        public LevelInteractiveController(List<GameObject> staticLevelParts, List<LevelPartContainer> dynamicLevelParts)
        {
            _dynamicLevelParts = dynamicLevelParts;
            _staticLevelParts = staticLevelParts;
        }

        public void AddLevelPart(LevelPartContainer levelPart)
        {
            _dynamicLevelParts.Add(levelPart);
        }

        public void AddLevelPart(GameObject levelPart)
        {
            _staticLevelParts.Add(levelPart);
        }

        public void RemoveLevelPart(LevelPartContainer levelPart)
        {
            Object.Destroy(levelPart);
            _dynamicLevelParts.Remove(levelPart);
        }

        public void RemoveLevelPart(GameObject levelPart)
        {
            Object.Destroy(levelPart);
            _staticLevelParts.Remove(levelPart);
        }

        public void UpdateLevelState(float stepTime)
        {
            foreach (var part in _dynamicLevelParts)
            {
                switch (part.PartController)
                {
                    case LevelPartControllers.PartMoveController:
                        LevelPartMoveController.MoveInteractiveTransform((LevelPartMoveContainer)part, stepTime);
                        break;
                    
                }
            }
        }

        public void DestroyLevelParts()
        {
            foreach (var part in _dynamicLevelParts)
            {
                Object.Destroy(part.gameObject);
            }

            foreach (var part in _staticLevelParts)
            {
                Object.Destroy(part);
            }
        }
    }
}