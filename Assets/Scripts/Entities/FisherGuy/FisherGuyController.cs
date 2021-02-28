using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace alexshko.fishingworld.Enteties

{
    public class FisherGuyController : MonoBehaviour
    {
        private Animator anim;

        public Transform FishLineHinge;

        private void Start()
        {
            anim = GetComponent<Animator>();
            if (!anim)
            {
                Debug.LogError("can't find the Animator of the Fisher guy.");
            }
        }

        public void PullRod(Transform FishCaught)
        {
            //FishLineHinge.GetComponent<FishingLineHinge>().AttachFishToEndOfLine(FishCaught);
            anim.SetBool("RodCasted", false);
            anim.SetBool("FishCaught", (FishCaught!=null));
            FishLineHinge.GetComponent<FishingLineHinge>().PullRod();
        }

        public void CastRod(Vector3 posToCast)
        {
            anim.SetBool("RodCasted", true);
            FishLineHinge.GetComponent<FishingLineHinge>().CastRod(posToCast);
        }
    }
}
