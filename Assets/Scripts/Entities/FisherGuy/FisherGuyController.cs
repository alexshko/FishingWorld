using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace alexshko.fishingworld.Enteties

{
    public class FisherGuyController : MonoBehaviour
    {
        private Animator anim;
        private float DistanceFromFishingPoint = 1.5f;

        public Transform FishLineHinge;

        private void Start()
        {
            anim = GetComponent<Animator>();
            if (!anim)
            {
                Debug.LogError("can't find the Animator of the Fisher guy.");
            }
        }

        public void PullRod(Vector3 PosToPull, Transform FishCaught)
        {
            anim.SetBool("RodCasted", false);
            anim.SetBool("FishCaught", (FishCaught!=null));
            FishLineHinge.GetComponent<FishingLineHinge>().AttachFishToEndOfLine(FishCaught);
            FishLineHinge.GetComponent<FishingLineHinge>().PullRod();
        }

        public void CastRod(Vector3 posToCast)
        {
            anim.SetBool("RodCasted", true);
            FishLineHinge.GetComponent<FishingLineHinge>().CastRod(posToCast);
        }
    }
}
