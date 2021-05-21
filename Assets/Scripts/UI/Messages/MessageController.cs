using alexshko.fishingworld.Enteties.Fishes;
using alexshko.fishingworld.Store;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

            //foreach (var btn in msg.GetComponentsInChildren<ButtonAddCurrency>())
            //{
            //    if (btn.Currency == Currency.Coins)
            //    {
            //        btn.Amount = Mathf.CeilToInt(fish.FishData.CoinsWorth * fish.weight);
            //    }
            //    else if (btn.Currency == Currency.Emeralds)
            //    {
            //        btn.Amount = Mathf.CeilToInt(fish.FishData.EmeraldWorth * fish.weight);
            //    }
            //} 

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

        }
    }
}