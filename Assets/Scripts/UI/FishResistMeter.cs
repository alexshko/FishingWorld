using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace alexshko.fishingworld.UI
{
    public class FishResistMeter : MonoBehaviour
    {
        public Image MeterBackground;
        public int MinVal { get; set; }
        public int MaxVal { get; set; }
        public int Value { 
            get { 
                return val; 
            } 
            set {
                SetValue(value); 
            } 
        }
        private int val;


        private void SetValue(int newValue)
        {
            val = Mathf.Clamp(newValue, MinVal, MaxVal);
            GetComponent<Slider>().value = val;
            if (Mathf.Abs(val) < (MaxVal - MinVal) / 10)
            {
                //its the good range.
                //green color
                MeterBackground.color = Color.green;
            }
            else if (Mathf.Abs(val) < (MaxVal - MinVal) / 2)
            {
                //its not good range. should pull the rod to get it back to green.
                //yellow color.
                MeterBackground.color = Color.yellow;
            }
            else
            {
                //red color

                MeterBackground.color = Color.red;
            }
        }
        private void Awake()
        {
            if (!MeterBackground)
            {
                Debug.LogError("MeterBackground is not defined");
            }
            MinVal = int.Parse(GetComponent<Slider>().minValue.ToString());
            MaxVal = int.Parse(GetComponent<Slider>().maxValue.ToString());
        }


    }
}