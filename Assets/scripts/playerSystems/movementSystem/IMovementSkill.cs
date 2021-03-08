using playerSystems;
using UnityEngine;

namespace movementSystem
{
    public interface IMovementSkill : IPlayerSkill
    {
        void MoveAction(Rigidbody2D rb, PlayerStats stats);
    }
}