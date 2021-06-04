using alexshko.fishingworld.Core;
using alexshko.fishingworld.Core.DB;
using alexshko.fishingworld.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace alexshko.fishingworld.Store
{
    [RequireComponent(typeof(UserStats))]
    public class StoreManagement : MonoBehaviour
    {
        private static User user
        {
            get
            {
                return User.Instance;
            }
            set
            {
                User.Instance = value;
            }
        }

        public static StoreManagement instance;

        // Use this for initialization
        private void Awake()
        {
            instance = this;
        }

        public string CurrentEquippedRod
        {
            get
            {
                return user.CurrentRod;
            }
            set
            {
                //mark the new rod in the dictionry of rods as the current one.
                user.CurrentRod = value;
                //update the data (the new rod) in the DB:
                UserFirebaseDataBase.Instance.SaveUserData(user).ConfigureAwait(false);
                //update the new rod in the UI.
                UpdateRodUI(user.CurrentRod);
            }
        }

        public Dictionary<string, bool> RodsBoughtDict
        {
            get
            {
                return user.RodsBought;
            }
        }

        public void AddRodToDict(string RodId)
        {
            if (RodsBoughtDict.ContainsKey(RodId)) return;
            RodsBoughtDict[RodId] = false;
            //update the data (the new rod) in the DB:
            UserFirebaseDataBase.Instance.SaveUserData(user).ConfigureAwait(false);
        }

        private void UpdateRodUI(string RodUI)
        {
            //update RodUI
        }
    }
}