using alexshko.fishingworld.Enteties;
using alexshko.fishingworld.Enteties.Fishes;
using alexshko.fishingworld.UI.Messages;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace alexshko.fishingworld.Core
{
    [RequireComponent(typeof(UserStats))]
    public class GameManagement : MonoBehaviour
    {
        public static GameManagement Instance;

        //public Action<Transform> OnAfterFinishedPullingFish;
        public Action OnFinishedPullingFishCycle;


        #region attributes 

        public Transform FisherGuy;
        public Lake LevelLake;
        [Tooltip("the place to cast the rod")]
        public Transform FishingSpot;
        public int SecondsToWaitDuringCast;
        #endregion


        #region private attributes for calculating the frequancy

        private Dictionary<ScriptableObjectFish, float> FishFrequancy;
        #endregion

        public Transform FishTookBait { get; set; }


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
                RandomizeFish();
                FisherGuy.GetComponent<FisherGuyController>().CastRod(FishingSpot.position);

                //if no fish was chosen then a suitable message will be diplayed and the player will have to do another cast:
                if (FishTookBait == null)
                {
                    HandleNoFishTookBait().ConfigureAwait(false);
                }
            }
        }

        public void HandleFishTookBait(Transform fish)
        {
            GetComponent<PullingMechanism>().enabled = true;
            Debug.Log("There is a fish. he took the bait.");
        }

        private async Task HandleNoFishTookBait()
        {
            //wait for a while to show that the fish wasn't randomized:
            await Task.Delay(SecondsToWaitDuringCast*1000);
            Debug.Log("No fish in the area");
            FisherGuy.GetComponent<FisherGuyController>().PullRod(null);
            Instance.FinishFishCatchingCycle();
        }

        public void HandleFishCaught()
        {
            Debug.Log("Fish was caught");
            GetComponent<PullingMechanism>().enabled = false;
            ShowNewFishMessage().ConfigureAwait(false);
        }

        public void HandleFishGotLoose()
        {
            Debug.Log("fish got loose");
            GetComponent<PullingMechanism>().enabled = false;

            //show a message that the fish was lost.

            //Finish the cycle:
            Instance.FinishFishCatchingCycle();
        }

        private async Task ShowNewFishMessage()
        {
            //set the cam to focus on the caught fish:
            await CameraController.Instance.SetFocusOnFishingSpot();

            //show new fish message:
            MessageController.instance.ShowMessageNewFish(FishTookBait.GetComponent<Fish>());
        }

        public void FinishFishCatchingCycle()
        {
            HandleEndOfFishCaughtCycle().ConfigureAwait(false);
        }

        private async Task HandleEndOfFishCaughtCycle()
        {
            //destroy the fish:
            if (FishTookBait != null)
            {
                Destroy(FishTookBait.gameObject);
            }

            //set the cam to the person and wait for it to finish:
            await CameraController.Instance.SetFocusOnMainCam();

            //if there is a message of new fish, close it.
            MessageController.instance.HideMessageNewFish();

            //Delegeate:
            //curently show the button START
            if (OnFinishedPullingFishCycle != null)
            {
                OnFinishedPullingFishCycle();
            }
        }

        //private void PullFishFromWater()
        //{
        //    StartCoroutine(GetFishOutOfWater());
        //}



        //private IEnumerator GetFishOutOfWater()
        //{
        //    //wait for fish to start biting:
        //    yield return new WaitForSeconds(2f);

        //    //if a fish was caught:
        //    if (CaughtFish == null)
        //    {
        //        Debug.LogFormat("No fish was caught. try again");
        //    }
        //    else
        //    {


        //        Debug.LogFormat("a fish was caught: " + CaughtFish.GetComponent<Fish>().FishData.Name);
        //        //FisherGuy.GetComponent<FisherGuyController>().PullRod(CaughtFish, HandleAfterFishPulled);
        //        GetComponent<PullingMechanism>().enabled = true;
        //    }
        //    yield return null;
        //}

        //private void HandleAfterFishPulled()
        //{
        //    //call an action.
        //    OnFinishedPullingFish(CaughtFish);
        //}




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
        private void RandomizeFish()
        {
            ScriptableObjectFish fishObj = ChooseRandomFish();
            if (fishObj && fishObj.name != "None")
            {
                GameObject rndob = Instantiate(fishObj.prefab, FishingSpot.position, Quaternion.identity, FishingSpot);
                FishTookBait = rndob.GetComponent<Transform>();
                //let the player pull the rod
            }
            else
            {
                FishTookBait = null;
            }
        }
    }
}
