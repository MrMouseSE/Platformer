using movementSystem;
using playerSystems;
using playerSystems.movementSystem;

namespace gameSystems.skills
{
    public static class SkillGenerator
    {
        public static IPlayerSkill GeneratePlayerSkill(SkillsEnum skill)
        {
            return skill switch
            {
                SkillsEnum.MoveForward => new MoveForward(),
                SkillsEnum.MoveByRotation => new MoveByRotation(),
                SkillsEnum.MoveByJump => new MoveByJump(),
                _ => new MoveForward()
            };
        }
    }
}