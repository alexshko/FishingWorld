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


        private Transform FisherGuy;
        private Transform CaughtFish;

        private Coroutine FishResisting;
        public int AmountToScutractFromPulling = 1;

        private void Awake()
        {
            pullingSlider.OnCommitedSlide += ResistValueUpdate;
        }

        private void OnEnable()
        {
            CaughtFish = GameManagement.Instance.CaughtFish;
            FisherGuy = GameManagement.Instance.FisherGuy;

            if (pullingSlider)
            {
                pullingSlider.gameObject.SetActive(true);
            }
            if (fishResistSlider)
            {
                fishResistSlider.gameObject.SetActive(true);
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
            while (FishIsHooked())
            {
                MakeFishResist();
                yield return null;

            }
        }

        private void MakeFishResist()
        {
            float resistInSecond = CaughtFish.GetComponent<Fish>().FishData.DiffucltyToCatch;
            ResistValueUpdate(resistInSecond * Time.deltaTime);
        }

        private bool FishIsHooked()
        {
            return CaughtFish.GetComponent<Fish>().IsCaught;
        }

        private void ResistValueUpdate(float resistAmountToAdd)
        {
            CaughtFish.GetComponent<Fish>().CurrentResist += resistAmountToAdd;
            CaughtFish.GetComponent<Fish>().CurrentResist = Mathf.Clamp(CaughtFish.GetComponent<Fish>().CurrentResist, fishResistSlider.MinVal, fishResistSlider.MaxVal);
            fishResistSlider.Value = Mathf.FloorToInt(CaughtFish.GetComponent<Fish>().CurrentResist);
            //FisherGuy.GetComponent<FisherGuyController>().PullRod(CaughtFish, HandleAfterFishPulled);
        }

        //will be called from delegate in PullRodStick
        private void ResistValueUpdate()
        {
            ResistValueUpdate(-AmountToScutractFromPulling);
        }

    }
}
