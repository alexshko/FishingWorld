using alexshko.fishingworld.Enteties.Fishes;
using alexshko.fishingworld.Store;
using UnityEngine;

namespace alexshko.fishingworld.UI.Messages {
    public class MessageController : MonoBehaviour
    {
        public static MessageController instance;

        private void Awake()
        {
            instance = this;
        }

        public void ShowMessageNewFish(Fish fish)
        {
            NewFishMessageText msg = GetComponentInChildren<NewFishMessageText>(includeInactive: true);
            if (!msg) return;

            msg.FishWeight = fish.weight;
            msg.FishName = fish.FishData.Name;
            msg.FishCoinsWorth = Mathf.CeilToInt(fish.FishData.CoinsWorth * fish.weight);
            msg.FishEmeraldsWorth = Mathf.CeilToInt(fish.FishData.EmeraldWorth * fish.weight);

            msg.gameObject.SetActive(true);
        }

        public void HideMessageNewFish()
        {
            NewFishMessageText msg = GetComponentInChildren<NewFishMessageText>(includeInactive: true);
            if (!msg) return;
            msg.gameObject.SetActive(false);
        }

        public void ShowMessageBuyStoreItem(StoreRodItem item)
        {
            BuyItemMessageText msg = GetComponentInChildren<BuyItemMessageText>(includeInactive: true);
            if (!msg) return;

            msg.gameObject.SetActive(true);
            msg.ItemToBuy = item;
        }
    }
}