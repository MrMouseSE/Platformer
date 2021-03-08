using System.Collections.Generic;

namespace playerSystems
{
    public class PlayerSkillsHolder
    {
        public IPlayerSkill CurrentSkill;
        
        private List<IPlayerSkill> _playerSkills = new List<IPlayerSkill>();

        public PlayerSkillsHolder(IPlayerSkill playerSkill, List<IPlayerSkill> playerSkills = null)
        {
            _playerSkills.Add(playerSkill);
            if (playerSkills != null)
            {
                _playerSkills.AddRange(playerSkills);
            }
        }

        public int GetPlayerSkillsCount()
        {
            return _playerSkills.Count;
        }

        public void AddNewSkill(IPlayerSkill playerSkill)
        {
            if(!FindSkill(playerSkill)) _playerSkills.Add(playerSkill);
        }

        public void DeleteExistSkill(IPlayerSkill playerSkill)
        {
            if (_playerSkills.Count>1)
                _playerSkills.Remove(playerSkill);
        }

        public IPlayerSkill SwapCurrentSkill(IPlayerSkill swapThis, IPlayerSkill toThis)
        {
            var skillIndex = FindSkillIndex(swapThis);
            var skill = _playerSkills[skillIndex];
            _playerSkills[skillIndex] = toThis;
            CurrentSkill = toThis;
            return skill;
        }

        public IPlayerSkill SwitchSkill(IPlayerSkill switchThis)
        {
            var skillIndex = FindSkillIndex(switchThis);
            skillIndex++;
            skillIndex = skillIndex < _playerSkills.Count ? skillIndex : 0;
            CurrentSkill = _playerSkills[skillIndex];
            return _playerSkills[skillIndex];
        }

        private int FindSkillIndex(IPlayerSkill skillToFind)
        {
            return _playerSkills.FindIndex(x => x == skillToFind);
        }

        private bool FindSkill(IPlayerSkill skillToFind)
        {
            return _playerSkills.Exists(x => x == skillToFind);
        }
    }
}