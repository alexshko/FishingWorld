using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace alexshko.fishingworld.UI
{
    public class PullRodStick : MonoBehaviour
    {
        public Action<int> OnCommitedSlide;

        private void LateUpdate()
        {
            if (Input.GetTouch(0).phase == TouchPhase.Ended || Input.GetTouch(0).phase == TouchPhase.Canceled)
            {
                int value = int.Parse(GetComponent<Slider>().value.ToString());
                OnCommitedSlide(value);
                GetComponent<Slider>().value = 0;
            }
        }
    }
}
