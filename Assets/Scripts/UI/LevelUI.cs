using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace alexshko.fishingworld.UI
{
    public class LevelUI : MonoBehaviour
    {
        public void btnLevelStart()
        {
            Debug.Log("pressed button play");
            if (Core.GameManagement.Instance)
            {
                //deactivate the button:
                EventSystem.current.currentSelectedGameObject.SetActive(false);
                //make the person cast a rod:
                Core.GameManagement.Instance.CastRod();
            }
        }
    }
}
