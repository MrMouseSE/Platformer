using gameSystems.skills;
using UnityEngine;

namespace playerSystems
{
    public class PlayerStats
    {
        public IPlayerSkill PlayerSkill;
        public int SkillHolderCapacity;
        public float Speed;
        public float AngularForce;
        public Vector2 JumpVector;

        public PlayerStats(PlayerStatsData data)
        {
            Speed = data.Speed;
            PlayerSkill = SkillGenerator.GeneratePlayerSkill(data.movementSkill);
            AngularForce = data.AngularForce;
            JumpVector = data.JumpVector;
        }
    }
}