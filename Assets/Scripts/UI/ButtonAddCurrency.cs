using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace alexshko.fishingworld.UI.Messages
{
    public enum Currency { Coins, Emeralds };
    public class ButtonAddCurrency : MonoBehaviour
    {
        [SerializeField]
        private Currency currency;
        [SerializeField]
        private int amount;

        public Currency Currency { 
            get { return currency; } 
        }
        public int Amount
        {
            get { return amount; }
            set
            {
                amount = value;
                transform.GetComponentInChildren<Text>().text = value.ToString();
            }
        }

        public void btnAddCurrency()
        {
            if (Currency == Currency.Coins)
            {
                UserStats.instance.Coins += Amount;
            }
            else if (Currency == Currency.Emeralds)
            {
                UserStats.instance.Emeralds += Amount;
            }
        }


    }
}
