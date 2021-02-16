using alexshko.fishingworld.Enteties.FishingRod;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace alexshko.fishingworld.Enteties.FisherGuy

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

        public void CastRod(Vector3 posToCast)
        {
            anim.SetBool("RodCasted", true);
            StartCoroutine(CastRodCo(posToCast));
        }

        private IEnumerator CastRodCo(Vector3 posToCast)
        {
            Transform LineTip = FishLineHinge.GetComponent<FishingLineHinge>().FishPoint;
            while (Vector3.Distance(LineTip.position, posToCast) > DistanceFromFishingPoint)
            {
                LineTip.position = Vector3.Lerp(LineTip.position, posToCast, Time.deltaTime);
                yield return null;
            }
            LineTip.position = posToCast;
            yield return null;
        }
    }
}
