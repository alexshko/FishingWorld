using alexshko.fishingworld.Enteties.Fishes;
using System.Collections;
using UnityEngine;

namespace alexshko.fishingworld.Enteties.Rods
{
    [CreateAssetMenu(fileName = "Rod", menuName = "ScriptableObjects/RodScriptableObject", order = 2)]
    public class RodScriptableObject : ScriptableObject
    {

        public int id;
        public string Name;
        public GameObject prefab;
        public int CoinsPrice;
        public int EmeraldsPrice;
        public FishPopularity[] FishBonuses;
        public float BoostCoins;
        public float BoostEmeralds;
    }
}