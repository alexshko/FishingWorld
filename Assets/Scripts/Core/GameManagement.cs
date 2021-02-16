using alexshko.fishingworld.Enteties.FisherGuy;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace alexshko.fishingworld.Core
{
    public class GameManagement : MonoBehaviour
    {
        public static GameManagement Instance;

        public Transform FisherGuy;

        [Tooltip("the place to cast the rod")]
        public Transform FishingSpot;

        private void Awake()
        {
            Instance = this;
        }

        public void CastRod()
        {
            Debug.LogFormat("cast a rod");
            if (FishingSpot)
            {
                FisherGuy.GetComponent<FisherGuyController>().CastRod(FishingSpot.position);
            }
        }
    }
}
