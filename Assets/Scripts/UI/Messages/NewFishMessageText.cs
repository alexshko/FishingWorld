using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace alexshko.fishingworld.UI.Messages
{
    public class NewFishMessageText : MonoBehaviour
    {
        public Transform FishNameRef;
        public Transform FishWeightRef;
        public Transform ButtonCoinsRef;
        public Transform ButtonEmeraldsRef;

        private string fishName;
        private float fishWeight;
        private int fishCoinsWorth;
        private int fishEmeraldsWorth;

        public string FishName {
            get { return fishName; }
            set {
                fishName = value;
                FishNameRef.GetComponent<Text>().text = value;
            }
        }
        public float FishWeight
        {
            get { return fishWeight; }
            set
            {
                fishWeight = value;
                //format F1 - 1 digit after Decimal point.
                FishWeightRef.GetComponent<Text>().text = value.ToString("F1");
            }
        }

        public int FishCoinsWorth
        {
            get { return fishCoinsWorth; }
            set
            {
                fishCoinsWorth = value;
                ButtonCoinsRef.GetComponent<ButtonAddCurrency>().Amount = value;
            }
        }
        public int FishEmeraldsWorth
        {
            get { return fishEmeraldsWorth; }
            set
            {
                fishEmeraldsWorth = value;
                ButtonEmeraldsRef.GetComponent<ButtonAddCurrency>().Amount = value;
            }
        }


    }
}
