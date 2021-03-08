using System.Collections.Generic;
using gameSystems.levelInteractiveControllers;
using UnityEngine;

namespace gameSystems.levelGenerator
{
    public static class LevelGenerator
    {
        private static LevelGenerationData _levelGenerationData;
        private static Dictionary<int, float> _levelPartPair;

        public static void GenerateLevel(LevelGenerationData data, ref List<GameObject> staticLevelParts, ref List<LevelPartContainer> dynamicLevelPart, LevelPartFinalContainer finalContainer)
        {
            _levelGenerationData = data;
            Vector3 instantiatePoint = _levelGenerationData.StartPartPosition;
            _levelPartPair = CalculatePartsWeight();

            int weight = 0;

            for (int i = 0; i < _levelGenerationData.LevelPartsSize; i++)
            {
                instantiatePoint = InstantiateLevelPart(instantiatePoint, weight, ref staticLevelParts, ref dynamicLevelPart);
                weight = Random.Range(0, 10);
            }

            var finalGeneratorContainer = finalContainer.GetComponent<LevelGeneratorPartContainer>();
            finalContainer.transform.position = finalGeneratorContainer.CalculateInstantiatePosition(instantiatePoint);
        }

        private static Vector3 InstantiateLevelPart(Vector3 instantiatePoint, int currWeight, ref List<GameObject> staticLevelParts, ref List<LevelPartContainer> dynamicLevelPart)
        {
            GameObject newLevelPart = Object.Instantiate(GetNextLevelPart(currWeight));
            var partContainer = newLevelPart.GetComponent<LevelGeneratorPartContainer>();
            newLevelPart.transform.position = partContainer.CalculateInstantiatePosition(instantiatePoint);
            if (partContainer.InteractiveContainer != null)
            {
                dynamicLevelPart.Add(partContainer.InteractiveContainer);
            }
            else
            {
                staticLevelParts.Add(partContainer.gameObject);
            }
            return partContainer.RightJoint.position;
        }

        private static GameObject GetNextLevelPart(int currWeight)
        {
            return _levelGenerationData.LevelParts[GetNextLevelPartIndex(currWeight)];
        }

        private static Dictionary<int, float> CalculatePartsWeight()
        {
            float totalWeight = 0f;
            var count = _levelGenerationData.LevelParts.Count;
            Dictionary<int,float> levelPartPair = new Dictionary<int, float>();
            for (int i = 0; i < count; i++)
            {
                var currWeight = 1f - totalWeight;
                var difWeight = currWeight / (count - i);
                currWeight = difWeight + (count - i - 1) * difWeight * 0.2f;
                totalWeight += currWeight;
                levelPartPair.Add(i,totalWeight);
            }

            return levelPartPair;
        }

        private static int GetNextLevelPartIndex(int currWeight)
        {
            int index = 0;
            foreach (var pair in _levelPartPair)
            {
                if (currWeight<pair.Value*10)
                {
                    index = pair.Key;
                    break;
                }
            }
            return index;
        }

        public static void SetPlayer(GameObject playerGO)
        {
            playerGO.transform.position = _levelGenerationData.StartPlayerPosition;
        }
    }
}