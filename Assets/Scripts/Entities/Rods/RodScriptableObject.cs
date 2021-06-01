using alexshko.fishingworld.Enteties.Fishes;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace alexshko.fishingworld.Enteties.Rods
{
    [CreateAssetMenu(fileName = "Rod", menuName = "ScriptableObjects/RodScriptableObject", order = 2)]
    public class RodScriptableObject : ScriptableObject
    {
        [Tooltip("the id of the Rod")]
        public int id;
        [Tooltip("the name of the Rod")]
        public string Name;
        [Tooltip("the prefab of the Rod defined by this Scriptable Object")]
        public GameObject prefab;
        [Tooltip("the 2d image for the ui messages")]
        public GameObject Rod2DImagePref;
        [Tooltip("the price of Rod in Coins")]
        public int CoinsPrice;
        [Tooltip("the price of Rod in Emeralds")]
        public int EmeraldsPrice;
        [Tooltip("the bonuses to catching Fish")]
        public FishPopularity[] FishBonuses;

        [Tooltip("the boost to the coins revenue in every catch")]
        public float BoostCoins;
        [Tooltip("the boost to the Emeralds revenue in every catch")]
        public float BoostEmeralds;
    }
}