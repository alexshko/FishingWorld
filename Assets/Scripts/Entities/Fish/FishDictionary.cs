using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace alexshko.fishingworld.Enteties.Fish
{

    [Serializable]
    public class FishPopularity
    {
        public ScriptableObjectFish fish;
        [Range(0, 100)]
        public float popularity;
    }

    public class FishDictionary : MonoBehaviour
    {
        [SerializeField]
        private FishPopularity[] FishPopularity;

        public Dictionary<ScriptableObjectFish, float> FishPopularityDict { get; }

        private void Awake()
        {
            if (FishPopularity.Length > 0)
            {
                for (int i = 0; i < FishPopularity.Length; i++)
                {
                    FishPopularityDict[FishPopularity[i].fish] = FishPopularity[i].popularity;
                }
            }
        }
    }
}
