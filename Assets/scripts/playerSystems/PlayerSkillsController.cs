using gameSystems.skills;
using movementSystem;
using UnityEngine;

namespace playerSystems
{
    public class PlayerSkillsController
    {
        private PlayerStats _playerStats;
        private PlayerSkillsHolder _playerSkillsHolder;

        public PlayerSkillsController(PlayerStats playerStats)
        {
            _playerStats = playerStats;
            _playerSkillsHolder = new PlayerSkillsHolder(playerStats.PlayerSkill);
        }

        public void ManipulatePlayerSkills(SkillContainer skillContainer)
        {
            if (skillContainer == null)
            {
                _playerSkillsHolder.SwitchSkill(_playerStats.PlayerSkill);
                UpdateCurrentSkill();
            }
            else
            {
                if (_playerStats.SkillHolderCapacity>_playerSkillsHolder.GetPlayerSkillsCount())
                {
                    _playerSkillsHolder.AddNewSkill(skillContainer.PlayerSkill);
                }
                else
                {
                    CreateDroppedSkillItem(_playerSkillsHolder.SwapCurrentSkill(_playerStats.PlayerSkill, skillContainer.PlayerSkill), skillContainer.SkillGameObject);
                    UpdateCurrentSkill();
                }
            }
        }

        private void UpdateCurrentSkill()
        {
            _playerStats.PlayerSkill = _playerSkillsHolder.CurrentSkill;
        }

        private static void CreateDroppedSkillItem(IPlayerSkill playerSkill, GameObject skillGO)
        {
            skillGO.GetComponent<SkillContainer>().UpdateSkillInContainer(playerSkill);
        }
    }
}