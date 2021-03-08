using playerSystems;
using UnityEngine;

namespace movementSystem
{
    public class MoveByRotation : IMovementSkill
    {
        public void MoveAction(Rigidbody2D rb, PlayerStats stats)
        {
            MoveObjectByRotation(rb, stats);
        }
        
        private static void MoveObjectByRotation(Rigidbody2D rb, PlayerStats stats)
        {
            rb.AddTorque(stats.AngularForce);
        }
    }
}
