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

        public float TimeInCurrentColor { get; set; }
        public Color CurrentColor { get { return (MeterBackground.color); } }


        private void SetValue(int newValue)
        {
            if (getColorForValue(Mathf.Clamp(newValue, MinVal, MaxVal)) != getColorForValue(val))
            {
                TimeInCurrentColor = 0;
                MeterBackground.color = getColorForValue(newValue);
            }
            val = Mathf.Clamp(newValue, MinVal, MaxVal);
            GetComponent<Slider>().value = val;
        }
        private void Awake()
        {
            if (!MeterBackground)
            {
                Debug.LogError("MeterBackground is not defined");
            }
            MinVal = int.Parse(GetComponent<Slider>().minValue.ToString());
            MaxVal = int.Parse(GetComponent<Slider>().maxValue.ToString());

            //Value = 0;
            //TimeInCurrentColor = 0;
        }

        private void OnEnable()
        {
            TimeInCurrentColor = 0;
            MeterBackground.color = getColorForValue(0);
            GetComponent<Slider>().value = 0;
        }

        private void Update()
        {
            TimeInCurrentColor += Time.deltaTime;
        }

        private bool isGreenResist(float val)
        {
            return (Mathf.Abs(val) < (MaxVal - MinVal) / 10);
        }
        private bool isYellowResist(float val)
        {
            return (Mathf.Abs(val) < (MaxVal - MinVal) / 2);
        }

        private bool isRedResist(float val)
        {
            return ((!isYellowResist(val)) && (!isGreenResist(val)));
        }

        private Color getColorForValue(float val)
        {
            if (isGreenResist(val)) return Color.green;
            if (isYellowResist(val)) return Color.yellow;
            return Color.red;
        }


    }
}