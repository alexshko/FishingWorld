using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace alexshko.fishingworld.UI
{
    public class PullRodStick : MonoBehaviour
    {
        public float AllowedYDistanceFromBar = 20f;
        public float timeAnimInterval = 0.005f;
        [ColorUsageAttribute(true, true)]
        public Color animColor;
        public GameObject ArrowsReference;
        public Action<int> OnCommitedSlide;

        private int PressedFingerId;
        private Image[] ImgsForAnim;
        private int diffCount;

        private void Start()
        {
            ImgsForAnim = ArrowsReference.GetComponentsInChildren<Image>();
            StartCoroutine(ArrowsBlinkAnim());
        }

        private void Update()
        {
            if (Input.touchCount > 0)
            {
                float PosDelta = Mathf.Abs(Input.GetTouch(0).position.y - transform.position.y);
                Debug.LogFormat("the delta from the bar position: {0}", PosDelta);
                if ((Input.GetTouch(0).fingerId == PressedFingerId || PressedFingerId == -1) && Input.GetTouch(0).phase == TouchPhase.Moved && (PosDelta < AllowedYDistanceFromBar))
                {
                    Debug.LogFormat("the finger moved: {0}", Input.GetTouch(0).deltaPosition);
                    PressedFingerId = Input.GetTouch(0).fingerId;
                    diffCount += Mathf.FloorToInt(Input.GetTouch(0).deltaPosition.x);
                }
                if (Input.GetTouch(0).phase == TouchPhase.Ended && Input.GetTouch(0).fingerId == PressedFingerId)
                {
                    PressedFingerId = -1;
                    OnCommitedSlide(Mathf.Abs(diffCount));
                    diffCount = 0;
                }
                if (Input.GetTouch(0).phase == TouchPhase.Canceled && Input.GetTouch(0).fingerId == PressedFingerId)
                {
                    PressedFingerId = -1;
                    diffCount = 0;
                }
                //Debug.LogFormat("the value of the slider: {0}", GetComponent<Slider>().value);
                //int value = Mathf.FloorToInt(GetComponent<Slider>().value);
                //OnCommitedSlide(value);
                //GetComponent<Slider>().value = 0;
            }
        }

        private IEnumerator ArrowsBlinkAnim()
        {
            Color curColor = ImgsForAnim[0].color;
            while (true)
            {
                if (ImgsForAnim.Length >= 1)
                {
                    ImgsForAnim[0].CrossFadeColor(animColor, timeAnimInterval, false, true);
                    yield return new WaitForSeconds(timeAnimInterval);
                }
                for (int i = 0; i < ImgsForAnim.Length; i++)
                {
                    if (i < ImgsForAnim.Length - 1)
                    {
                        ImgsForAnim[i + 1].CrossFadeColor(animColor, timeAnimInterval, false, true);
                        yield return new WaitForSeconds(timeAnimInterval);
                    }
                    ImgsForAnim[i].CrossFadeColor(curColor, timeAnimInterval, false, true);
                    yield return new WaitForSeconds(timeAnimInterval);
                }
            }
        }
    }
}
