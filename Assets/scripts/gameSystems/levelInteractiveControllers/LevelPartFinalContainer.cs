using UnityEngine;

namespace gameSystems.levelInteractiveControllers
{
    public class LevelPartFinalContainer : LevelPartContainer
    {
        public GameController GameController;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.GetComponentInParent<PlayerController>() != null)
            {
                GameController.LevelUp();
            }
        }
    }
}