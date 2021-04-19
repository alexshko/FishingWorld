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

        private void UpdateCurrencyValue(Currency cur, int val)
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
                UserFirebaseDataBase.Instance.UserUpdateCurrency(Currency.Coins, user.Coins);
                UpdateCurrencyValue(Currency.Coins, user.Coins);
            }
        }
        //private int emeralds;
        public int Emeralds
        {
            get
            {
                return user.Emeralds;
            }
            set
            {
                user.Emeralds = value;
                UserFirebaseDataBase.Instance.UserUpdateCurrency(Currency.Emeralds, user.Emeralds);
                UpdateCurrencyValue(Currency.Emeralds, user.Emeralds);
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
            ReadUserDataAndUpdateUI().Start();
        }

        private async Task ReadUserDataAndUpdateUI()
        {
            user = await UserFirebaseDataBase.Instance.ReadUserData(UserFirebaseDataBase.TimeoutMillis);
            UpdateCurrencyValue(Currency.Coins, user.Coins);
            UpdateCurrencyValue(Currency.Emeralds, user.Emeralds);
        }

    }
}