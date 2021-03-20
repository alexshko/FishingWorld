using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace alexshko.fishingworld.UI
{
    public class LevelUI : MonoBehaviour
    {

        public Transform StorePanel;

        GameObject btn;

        void Awake()
        {
            RectTransform rt = GetComponent<RectTransform>();
            float canvasHeight = rt.rect.height;
            float desiredCanvasWidth = canvasHeight * Camera.main.aspect;
            rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, desiredCanvasWidth);
        }

        private void Start()
        {
            Core.GameManagement.Instance.OnFinishedPullingFishCycle += ActivateButton;
        }
        public void btnLevelStart()
        {
            Debug.Log("pressed button play");
            if (Core.GameManagement.Instance)
            {
                //deactivate the button:
                EventSystem.current.currentSelectedGameObject.SetActive(false);
                btn = EventSystem.current.currentSelectedGameObject;
                //make the person cast a rod:
                Core.GameManagement.Instance.CastRod();
            }
        }

        public void ActivateButton()
        {
            btn.SetActive(true);
        }

        public void OpenStorePanel()
        {
            StorePanel.gameObject.SetActive(true);
        }
    }
}
