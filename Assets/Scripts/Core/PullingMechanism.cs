using alexshko.fishingworld.Enteties;
using alexshko.fishingworld.Enteties.Fishes;
using alexshko.fishingworld.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace alexshko.fishingworld.Core
{
    public class PullingMechanism : MonoBehaviour
    {
        public PullRodStick pullingSlider;
        public FishResistMeter fishResistSlider;
        public int AmountToScutractFromPulling = 1;
        public float TimeToHoldGreen = 2;
        public float TimeToHoldRed = 2;


        private Transform FisherGuy;
        private Transform TookBaitFish;

        private Coroutine FishResisting;

        private void Awake()
        {
            pullingSlider.OnCommitedSlide += ResistValueUpdate;
        }

        private void OnEnable()
        {
            TookBaitFish = GameManagement.Instance.FishTookBait;
            FisherGuy = GameManagement.Instance.FisherGuy;

            if (pullingSlider)
            {
                pullingSlider.gameObject.SetActive(true);
            }
            if (fishResistSlider)
            {
                fishResistSlider.gameObject.SetActive(true);
                ResistValueUpdate(0);
            }

            //start the Coroutine for handling the resistance of the fish.
            //if the Fish Resistance Coroutine is active then it should restart.
            if (FishResisting!=null)
            {
                StopCoroutine(FishResisting);
            }
            FishResisting = StartCoroutine(HandleFishPulling());
        }
        private void OnDisable()
        {
            if (pullingSlider)
            {
                pullingSlider.gameObject.SetActive(false);
            }
            if (fishResistSlider)
            {
                fishResistSlider.gameObject.SetActive(false);
            }
            if (FishResisting != null)
            {
                StopCoroutine(FishResisting);
            }
        }

        private IEnumerator HandleFishPulling()
        {
            bool fishCaught = false;
            bool fishLoose = false;
            while (FishIsHooked())
            {
                MakeFishResist();
                if (FishGotCaught())
                {
                    fishCaught = true;
                    break;
                }
                if (FishGotLooseFromRod())
                {
                    fishLoose = true;
                    break;
                }
                yield return null;
            }
            if (fishCaught)
            {
                FisherGuy.GetComponent<FisherGuyController>().PullRod(TookBaitFish);
                GameManagement.Instance.HandleFishCaught();
            }
            else if (fishLoose)
            {
                FisherGuy.GetComponent<FisherGuyController>().PullRod(null);
                GameManagement.Instance.HandleFishGotLoose();
            }
        }

        private void MakeFishResist()
        {
            float resistInSecond = TookBaitFish.GetComponent<Fish>().FishData.DiffucltyToCatch;
            ResistValueUpdate(resistInSecond * Time.deltaTime);
        }

        private bool FishIsHooked()
        {
            return ((TookBaitFish.GetComponent<Fish>().TookBait));
        }
        private bool FishGotCaught()
        {
            return ((TookBaitFish.GetComponent<Fish>().TookBait) && (fishResistSlider.CurrentColor == Color.green && fishResistSlider.TimeInCurrentColor > TimeToHoldGreen));
        }
        private bool FishGotLooseFromRod()
        {
            //if (fishResistSlider.CurrentColor == Color.red)
            //{
            //    Debug.Log("time in red: " + fishResistSlider.TimeInCurrentColor+". the max: "+ TimeToHoldRed);
            //}
            return ((TookBaitFish.GetComponent<Fish>().TookBait) && (fishResistSlider.CurrentColor == Color.red && fishResistSlider.TimeInCurrentColor > TimeToHoldRed));
        }

        private void ResistValueUpdate(float resistAmountToAdd)
        {
            TookBaitFish.GetComponent<Fish>().CurrentResist += resistAmountToAdd;
            TookBaitFish.GetComponent<Fish>().CurrentResist = Mathf.Clamp(TookBaitFish.GetComponent<Fish>().CurrentResist, fishResistSlider.MinVal, fishResistSlider.MaxVal);
            fishResistSlider.Value = Mathf.FloorToInt(TookBaitFish.GetComponent<Fish>().CurrentResist);
            //FisherGuy.GetComponent<FisherGuyController>().PullRod(CaughtFish, HandleAfterFishPulled);
        }

        //will be called from delegate in PullRodStick
        private void ResistValueUpdate()
        {
            ResistValueUpdate(-AmountToScutractFromPulling);
        }

    }
}
