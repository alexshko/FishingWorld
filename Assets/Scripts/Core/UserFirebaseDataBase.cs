using alexshko.fishingworld.Core;
using Firebase.Auth;
using Firebase.Database;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace alexshko.fishingworld.Core.DB
{
    public class UserFirebaseDataBase : UserDataBase{

        private FirebaseUser user;
        private DatabaseReference dbRef;

        public static UserDataBase instance;

        public UserFirebaseDataBase()
        {
            instance = this;
            user = FirebaseAuth.DefaultInstance.CurrentUser;
            dbRef = FirebaseDatabase.DefaultInstance.RootReference;
        }

        public async Task ReadUserData(Action<User> actionAfterRead)
        {
            await dbRef.Child(user.UserId).GetValueAsync().ContinueWith(task => {
                if (task.IsFaulted || task.IsCanceled)
                {
                    Debug.Log("coudlnt read user info");
                }
                if (task.IsCompleted)
                {
                    DataSnapshot data = task.Result;
                    actionAfterRead((User)(data.Value));
                }
            });
        }

        public void UserUpdateCurrency(Currency currency, int newVal)
        {
            string currStr = (currency == Currency.Coins) ? "Coins" : "Emeralds";
            dbRef.Child("users/" + user.UserId).Child(currStr).SetValueAsync(newVal);
        }

        public void ReadUserCreateEmptyIfNotExistInDB()
        {
            createNewUserInDB();

        }

        private void createNewUserInDB()
        {
            User newUser = new User();
            string dataForJson = JsonUtility.ToJson(newUser);
            dbRef.Child(user.UserId).SetRawJsonValueAsync(dataForJson);
        }
    }
}