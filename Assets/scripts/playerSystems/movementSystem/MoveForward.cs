using movementSystem;
using UnityEngine;

namespace playerSystems.movementSystem
{
    public class MoveForward : IMovementSkill
    {
        public void MoveAction(Rigidbody2D rb, PlayerStats stats)
        {
            MoveObjectForward(rb, stats);
        }

        private static void MoveObjectForward(Rigidbody2D rb, PlayerStats stats)
        {
            rb.AddForce(Vector2.right * stats.Speed);
        }
    }
}