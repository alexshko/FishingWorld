using alexshko.fishingworld.Enteties;
using alexshko.fishingworld.UI;
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

        private int curResist;

        private void Awake()
        {
            pullingSlider.OnCommitedSlide += ResistValueUpdate;
        }

        private void OnEnable()
        {
            CaughtFish = GameManagement.Instance.CaughtFish;
            FisherGuy = GameManagement.Instance.FisherGuy;

            pullingSlider.gameObject.SetActive(true);
            fishResistSlider.gameObject.SetActive(true);
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
        }

        private IEnumerator HandleFishPulling()
        {
            //while (FishIsHooked())
            //{
            //    MakeFishResist();
            //    yield return null;

            //}
            yield return null;
        }

        //will be called from delegate in PullRodStick
        private void ResistValueUpdate(int resistAmount)
        {
            curResist -= resistAmount;
            fishResistSlider.Value = curResist;
            //FisherGuy.GetComponent<FisherGuyController>().PullRod(CaughtFish, HandleAfterFishPulled);
        }

    }
}
