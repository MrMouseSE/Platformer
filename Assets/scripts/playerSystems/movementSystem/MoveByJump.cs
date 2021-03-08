using playerSystems;
using UnityEngine;

namespace movementSystem
{
    public class MoveByJump : IMovementSkill
    {
        public void MoveAction(Rigidbody2D rb, PlayerStats stats)
        {
            Jump(rb,stats);
        }

        private static void Jump(Rigidbody2D rb, PlayerStats stats)
        {
            rb.AddForce(stats.JumpVector);
        }
    }
}