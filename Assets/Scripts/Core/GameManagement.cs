using alexshko.fishingworld.Enteties;
using alexshko.fishingworld.Enteties.Fishes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace alexshko.fishingworld.Core
{

    public class GameManagement : MonoBehaviour
    {
        public static GameManagement Instance;


        #region attributes 

        public Transform FisherGuy;
        public Lake LevelLake;
        [Tooltip("the place to cast the rod")]
        public Transform FishingSpot;
        #endregion


        #region private attributes for calculating the frequancy

        private Dictionary<ScriptableObjectFish, float> FishFrequancy;
        #endregion

        private Transform CaughtFish;

        public Action<Transform> OnFinishedPullingFish { get; set; }

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            UpdateFishCatchingFrequancy();
        }

        public void CastRod()
        {
            Debug.LogFormat("cast a rod");
            if (FishingSpot)
            {
                FisherGuy.GetComponent<FisherGuyController>().CastRod(FishingSpot.position);
                ScriptableObjectFish fishObj = ChooseRandomFish();
                if (fishObj && fishObj.name!="None")
                {
                    GameObject rndob = Instantiate(fishObj.prefab, FishingSpot.position, Quaternion.identity, FishingSpot);
                    CaughtFish = rndob.GetComponent<Transform>();
                    //let the player pull the rod
                }
                else
                {
                    CaughtFish = null;
                }
                StartCoroutine(GetFishOutOfWater());
            }
        }

        private IEnumerator GetFishOutOfWater()
        {
            yield return new WaitForSeconds(2f);
            if (CaughtFish == null)
            {
                Debug.LogFormat("No fish was caught. try again");
            }
            else
            {
                Debug.LogFormat("a fish was caught: " + CaughtFish.GetComponent<Fish>().FishData.Name);
            }
            FisherGuy.GetComponent<FisherGuyController>().PullRod(FishingSpot.position, CaughtFish);
            //action:
            OnFinishedPullingFish(CaughtFish);
            yield return null;
        }

        //a function that chooses a fish to be caught.
        //consideraing the popularity of the fish in the lake, the rod, the bait and etc.
        private void UpdateFishCatchingFrequancy()
        {
            //make a dictionary of all fish and their popularity:
            float sum = 0;
            FishFrequancy = new Dictionary<ScriptableObjectFish, float>();
            foreach (KeyValuePair<ScriptableObjectFish, float> item in LevelLake.FishPopularityDict)
            {
                if (FishFrequancy.ContainsKey(item.Key))
                {
                    FishFrequancy[item.Key] += item.Value;
                }
                else
                {
                    FishFrequancy[item.Key] = item.Value;
                }
                sum += item.Value;
            }

            //Normalize the prop and make it Cumulative:
            ScriptableObjectFish prev = null;
            List<ScriptableObjectFish> keys = new List<ScriptableObjectFish>(FishFrequancy.Keys);
            foreach (ScriptableObjectFish item in keys)
            {
                //Normalize:
                FishFrequancy[item] = FishFrequancy[item] / sum;
                //add the previous value so that the propabilities will be Cumulative.
                if (prev)
                {
                    FishFrequancy[item] += FishFrequancy[prev];
                }
                prev = item;
            }
        }

        private ScriptableObjectFish ChooseRandomFish()
        {
            float rnd = UnityEngine.Random.value;
            foreach (var item in FishFrequancy)
            {
                if (item.Value > rnd) return item.Key;
            }
            return null;
        }
    }
}
