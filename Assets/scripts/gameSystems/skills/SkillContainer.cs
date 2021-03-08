using System.Collections.Generic;
using playerSystems;
using UnityEngine;

namespace gameSystems.skills
{
    public class SkillContainer : MonoBehaviour
    {
        public GameObject SkillGameObject;
        [SerializeField] private SpriteRenderer _spriteRenderer;

        public IPlayerSkill PlayerSkill;

        public List<Sprite> Sprites;

        public SkillContainer(IPlayerSkill skill, Sprite skillSprite)
        {
            PlayerSkill = skill;
            _spriteRenderer.sprite = FindSpriteBySkillName(skill);
        }

        private void Awake()
        {
            UpdateSkillInContainer(PlayerSkill);
        }

        public void UpdateSkillInContainer(IPlayerSkill skill)
        {
            PlayerSkill = skill;
            _spriteRenderer.sprite = FindSpriteBySkillName(skill);
        }

        private Sprite FindSpriteBySkillName(IPlayerSkill skill)
        {
            if (skill == null) return Sprites[0];
            var type = skill.GetType();

            Sprite newSprite = Sprites[0];
            foreach (var sprite in Sprites)
            {
                if (type.Name.Contains(sprite.name))
                    return sprite;
            }
            return newSprite;
        }
    }
}