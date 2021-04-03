using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace alexshko.fishingworld.UI
{
    public class MainMenu : MonoBehaviour
    {
        private void Start()
        {
            if (PlayerPrefs.GetString(Login.PREFS_NAME) != null)
            {
                GetComponent<Text>().text = PlayerPrefs.GetString(Login.PREFS_NAME);
            }
        }
    }
}
