using alexshko.fishingworld.Core;
using alexshko.fishingworld.Enteties.Rods;
namespace alexshko.fishingworld.Store
{
    public class StoreRodItem : IStoreItem
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
                return StoreManagement.instance.RodsBoughtDict.ContainsKey(RodData.Name);
            }
        }

        public bool isCapableBuying
        {
            get
            {
                return (!isAlreadyBaught && (EmeraldPrice <= UserStats.instance.Emeralds) && (CoinsPrice <= UserStats.instance.Coins));
            }
        }

        public bool Buy()
        {
            if (this.isCapableBuying)
            {
                UserStats.instance.Emeralds -= EmeraldPrice;
                UserStats.instance.Coins -= CoinsPrice;
                StoreManagement.instance.AddRodToDict(RodData.Name);
            }
            return this.isCapableBuying;
        }

        public void Equip()
        {
            if (this.isAlreadyBaught)
            {
                StoreManagement.instance.CurrentRod = RodData.Name;
            }
        }
    }
}
