using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace alexshko.fishingworld.UI
{
    public class LevelUI : MonoBehaviour
    {

        GameObject btn;
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
    }
}
