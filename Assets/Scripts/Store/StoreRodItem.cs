using alexshko.fishingworld.Core;
using alexshko.fishingworld.Enteties.Rods;
using UnityEngine;

namespace alexshko.fishingworld.Store
{
    public class StoreRodItem : MonoBehaviour, IStoreItem
    {
        public RodScriptableObject RodData;

        public int CoinsPrice
        {
            get
            {
                return RodData.CoinsPrice;
            }
        }

        public int EmeraldPrice
        {
            get
            {
                return RodData.EmeraldsPrice;
            }
        }

        public bool isAlreadyBaught
        {
            get
            {
                return StoreManagement.instance.RodsBoughtDict.ContainsKey(RodData.id);
            }
        }

        public bool isCapableBuying
        {
            get
            {
                return (!isAlreadyBaught && (EmeraldPrice <= UserStats.instance.Emeralds) && (CoinsPrice <= UserStats.instance.Coins));
            }
        }

        public string ItemName => RodData.Name;

        public string ItemDesc => RodData.Description;

        public GameObject ImageLink => RodData.Rod2DImagePref;

        public bool Buy()
        {
            if (this.isCapableBuying)
            {
                UserStats.instance.Emeralds -= EmeraldPrice;
                UserStats.instance.Coins -= CoinsPrice;
                StoreManagement.instance.AddRodToDict(RodData.id);
            }
            return this.isCapableBuying;
        }

        public void Equip()
        {
            if (this.isAlreadyBaught)
            {
                StoreManagement.instance.CurrentEquippedRod = RodData.id;
            }
        }
    }
}
