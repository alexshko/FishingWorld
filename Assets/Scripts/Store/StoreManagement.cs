using alexshko.fishingworld.Core;
using alexshko.fishingworld.UI;
using System.Collections;
using UnityEngine;

namespace alexshko.fishingworld.Store
{
    public class StoreManagement : MonoBehaviour
    {
        private User user;

        public static StoreManagement instance;

        // Use this for initialization
        private void Awake()
        {
            instance = this;
            user = User.FromJson(PlayerPrefs.GetString(Login.PREFS_USER_STATS));
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}