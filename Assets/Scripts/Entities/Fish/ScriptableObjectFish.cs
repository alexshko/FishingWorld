using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace alexshko.fishingworld.Enteties.Fishes
{
    [CreateAssetMenu(fileName = "Fish", menuName = "ScriptableObjects/ScriptableObjectFish", order = 1)]
    public class ScriptableObjectFish : ScriptableObject
    {
        public int id;
        [Tooltip("the name of the fish")]
        public string Name;

        [Tooltip("the prefab that uses this ScritableObject")]
        public GameObject prefab;
        [Tooltip("How dificult to catch the fish. the higher the value the more it will fight before pulled out.")]
        [Range(0, 10)]
        public float DiffucltyToCatch;
        //[Tooltip("the consentration of the fish in the lake")]
        //[Range(0,100)]
        //public float popularity;
        [Tooltip("the max weight possible of the fish")]
        public float MaxWeight;
        [Tooltip("how much Coins one can earn from it per pound")]
        public int CoinsWorth;
        [Tooltip("how much Royal Starts one can earn from it. Only the first time the fish is caught.")]
        public int RoyalStarsWorth;
        [Tooltip("how much Emeralds one can earn from it per pound")]
        public int EmeraldWorth;
    }
}