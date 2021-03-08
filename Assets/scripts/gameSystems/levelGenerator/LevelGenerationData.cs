using System.Collections.Generic;
using gameSystems.skills;
using UnityEngine;

namespace gameSystems.levelGenerator
{
    [CreateAssetMenu(fileName = "LevelGenerationData", menuName = "ScriptableObjects/LevelGenerationData", order = 1)]
    public class LevelGenerationData : ScriptableObject
    {
        public int Level;
        public Vector3 StartPlayerPosition;
        public Vector3 StartPartPosition;
        public Vector3 StartSkillPosition;
        public SkillsEnum LevelSkill;
        public List<GameObject> LevelParts;
        public int LevelPartsSize;
    }
}