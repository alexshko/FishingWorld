using alexshko.fishingworld.Enteties.Fishes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace alexshko.fishingworld.UI.Messages {
    public class Messages : MonoBehaviour
    {
        public Transform CatchFishMessage;



        public static Messages instance;
        private string CoinsName = "btnCoins";
        private string EmeraldsName = "btnEmeralds";

        private void Awake()
        {
            instance = this;
        }

        public void ShowMessageNewFish(Fish fish)
        {
            Transform msg = Instantiate(CatchFishMessage, transform);

            foreach (var btn in msg.GetComponentsInChildren<ButtonAddCurrency>())
            {
                if (btn.Currency == Currency.Coins)
                {
                    btn.Amount = Mathf.CeilToInt(fish.FishData.CoinsWorth * fish.weight);
                }
                else if (btn.Currency == Currency.Emeralds)
                {
                    btn.Amount = Mathf.CeilToInt(fish.FishData.EmeraldWorth * fish.weight);
                }
            } 

            msg.gameObject.SetActive(true);
        }
    }
}