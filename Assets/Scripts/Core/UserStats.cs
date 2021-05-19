using UnityEngine;
using TMPro;
using alexshko.fishingworld.UI;
using alexshko.fishingworld.Core.DB;
using System.Threading.Tasks;

namespace alexshko.fishingworld.Core
{
    public enum Currency { Coins, Emeralds };

    public class UserStats : MonoBehaviour
    {
        public Transform CoinsRef;
        public Transform EmeraldsRef;

        private User user;

        private void UpdateCurrencyValueUI(Currency cur, int val)
        {
            Transform CurRef = (cur == Currency.Coins ? CoinsRef : EmeraldsRef);
            CurRef.GetComponent<TMP_Text>().text = val.ToString("F0");
        }

        //private int coins;
        public int Coins
        {
            get
            {
                return user.Coins;
            }
            set
            {
                user.Coins = value;
                //UserFirebaseDataBase.Instance.UserUpdateCurrency(Currency.Coins, user.Coins);
                UserFirebaseDataBase.Instance.SaveUserData(user).ConfigureAwait(false);
                UpdateCurrencyValueUI(Currency.Coins, user.Coins);
            }
        }

        public string CurrentRod { 
            get
            {
                return user.CurrentRod;
            }
            set
            {
                //unset all the rods of the user
                user.UnSetAllRods();
                //mark the new rod in the dictionry of rods as the current one.
                user.RodsBought[value] = true;
                //update the data (the new rod) in the DB:
                UserFirebaseDataBase.Instance.SaveUserData(user).ConfigureAwait(false);
                //update the new rod in the UI.
                UpdateRodUI(user.CurrentRod);
            }
        }

        public int Emeralds
        {
            get
            {
                return user.Emeralds;
            }
            set
            {
                user.Emeralds = value;
                //UserFirebaseDataBase.Instance.UserUpdateCurrency(Currency.Emeralds, user.Emeralds);
                UserFirebaseDataBase.Instance.SaveUserData(user).ConfigureAwait(false);
                UpdateCurrencyValueUI(Currency.Emeralds, user.Emeralds);
            }
        }
        //public int RoyalStarts { get; set; }

        //public int FishCaught { get; set; }

        //private string userID;

        public static UserStats instance;

        private void Awake()
        {
            instance = this;
            //userID = PlayerPrefs.GetString(Login.PREFS_NAME);
            ReadUserDataAndUpdateUI();
        }

        private void ReadUserDataAndUpdateUI()
        {
            //user = await UserFirebaseDataBase.Instance.ReadUserData(UserFirebaseDataBase.TimeoutMillis);
            user = User.FromJson(PlayerPrefs.GetString(Login.PREFS_USER_STATS));
            UpdateCurrencyValueUI(Currency.Coins, user.Coins);
            UpdateCurrencyValueUI(Currency.Emeralds, user.Emeralds);
            UpdateRodUI(user.CurrentRod);
            //add here init for list of caught fishes.
        }

        private void UpdateRodUI(string RodUI)
        {
            //update RodUI
        }

    }
}