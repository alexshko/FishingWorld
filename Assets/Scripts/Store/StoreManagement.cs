using alexshko.fishingworld.Core;
using alexshko.fishingworld.Core.DB;
using alexshko.fishingworld.Enteties.Rods;
using System;
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

        //will be called when a rod has been equipped:
        //FishingLineHinge : awake.
        public Action OnRodEquipped;
        public static StoreManagement instance;

        // Use this for initialization
        private void Awake()
        {
            instance = this;
        }
        private void Start()
        {
            //make sure that the correct rod is equpped in the scene.
            UpdateRodUI(user.CurrentRod);
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
                UpdateRodUI(value);
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

        private void UpdateRodUI(string RodToEquip)
        {
            //find the correct RodScriptableObject:
            RodScriptableObject rod = Resources.Load<RodScriptableObject>(RodToEquip);
            if (!rod)
            {
                Debug.LogError("Couldn't load the rod");
            }

            Debug.Log("Equipping rod: " + rod.Name);
            GameObject ContainerParent = GameObject.FindWithTag("RodContainer");
            if (ContainerParent)
            {
                //check if the rod isn't already equiped in the ui:
                Rod currentEquippedRod = ContainerParent.GetComponentInChildren<Rod>();
                if (!currentEquippedRod || (currentEquippedRod.data.id == rod.id))
                {
                    return;
                }

                //delete current rods:
                foreach (var EquippedRod in ContainerParent.GetComponentsInChildren<Rod>())
                {
                    Destroy(EquippedRod.gameObject);
                } 

                //add the new Rod:
                Instantiate(rod.prefab, ContainerParent.transform);
                if (OnRodEquipped !=null)
                {
                    OnRodEquipped();
                }
            }
        }
    }
}