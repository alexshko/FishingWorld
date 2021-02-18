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

        private Dictionary<ScriptableObjectFish, float> fishpopularitydict;
        public Dictionary<ScriptableObjectFish, float> FishPopularityDict { get { return fishpopularitydict; } }

        private void Awake()
        {
            fishpopularitydict = new Dictionary<ScriptableObjectFish, float>();
            if (FishPopularity.Length > 0)
            {
                for (int i = 0; i < FishPopularity.Length; i++)
                {
                    fishpopularitydict[FishPopularity[i].fish] = FishPopularity[i].popularity;
                }
            }
        }
    }
}
