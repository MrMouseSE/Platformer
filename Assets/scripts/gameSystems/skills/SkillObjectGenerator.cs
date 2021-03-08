using movementSystem;
using playerSystems.movementSystem;
using Unity.Mathematics;
using UnityEngine;

namespace gameSystems.skills
{
    public static class SkillObjectGenerator
    {
        public static GameObject GenerateSkillGameObject(GameObject skillGameObject, SkillsEnum skillNum)
        {
            skillGameObject = Object.Instantiate(skillGameObject);
            var container = skillGameObject.GetComponent<SkillContainer>();
            switch (skillNum)
            {
                case SkillsEnum.MoveForward:
                    container.UpdateSkillInContainer(new MoveForward());
                    break;
                case SkillsEnum.MoveByRotation:
                    container.UpdateSkillInContainer(new MoveByRotation());
                    break;
                case SkillsEnum.MoveByJump:
                    container.UpdateSkillInContainer(new MoveByJump());
                    break;
            }

            return skillGameObject;
        }

        public static GameObject GenerateSkillGameObject(IMovementSkill skill, GameObject skillGameObject, Vector3 position)
        {
            skillGameObject = Object.Instantiate(skillGameObject, position,quaternion.identity);
            var container = skillGameObject.GetComponent<SkillContainer>();
            container.PlayerSkill = skill;
            return skillGameObject;
        }
    }
}