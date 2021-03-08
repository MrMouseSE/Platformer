using gameSystems.skills;
using UnityEngine;

namespace playerSystems
{
    [CreateAssetMenu(fileName = "PlayerStatsData", menuName = "ScriptableObjects/PlayerStatsData", order = 1)]
    public class PlayerStatsData : ScriptableObject
    {
        public SkillsEnum movementSkill;
        public float Speed;
        public float AngularForce;
        public Vector2 JumpVector;
    }
}