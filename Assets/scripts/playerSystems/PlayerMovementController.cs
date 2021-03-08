using movementSystem;
using playerSystems.movementSystem;
using UnityEngine;

namespace playerSystems
{
    public class PlayerMovementController
    {
        private Rigidbody2D _playerRb;
        private PlayerStats _playerStats;

        public PlayerMovementController(Rigidbody2D playerRB, PlayerStats playerStats)
        {
            _playerRb = playerRB;
            _playerStats = playerStats;
        }

        public void MovePlayer()
        {
            if (_playerStats.PlayerSkill.GetType() == typeof(MoveForward))
            {
                _playerRb.constraints = RigidbodyConstraints2D.FreezeRotation;
            }
            else
            {
                _playerRb.constraints = RigidbodyConstraints2D.None;
            }
            var movementSkill = (IMovementSkill) _playerStats.PlayerSkill;
            movementSkill.MoveAction(_playerRb,_playerStats);
        }
    }
}