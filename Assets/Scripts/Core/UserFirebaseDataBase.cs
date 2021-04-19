using alexshko.fishingworld.Core;
using Firebase.Auth;
using Firebase.Database;
using Firebase.Extensions;
using MiniJSON;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace alexshko.fishingworld.Core.DB
{
    public class UserFirebaseDataBase : UserDataBase{

        private FirebaseUser user;
        private DatabaseReference dbRef;

        #region singelton variables:
        private static UserDataBase instance;
        private static readonly object padlock = new object();

        public const int TimeoutMillis = 3000;

        public static UserDataBase Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new UserFirebaseDataBase();
                    }
                    return instance;
                }
            }
        }

        #endregion

        public UserFirebaseDataBase()
        {
            //instance = this;
            user = FirebaseAuth.DefaultInstance.CurrentUser;
            dbRef = FirebaseDatabase.DefaultInstance.GetReference("Users");
        }

        public async Task<User> ReadUserData(int timeout = TimeoutMillis)
        {
            User u = null;
            var task = dbRef.GetValueAsync();
            using (var timeoutCancellationTokenSource = new CancellationTokenSource())
            {
                var completedTask = await Task.WhenAny(task, Task.Delay(timeout, timeoutCancellationTokenSource.Token));
                if (completedTask == task)
                {
                    timeoutCancellationTokenSource.Cancel();
                    await task.ContinueWithOnMainThread(execTask =>
                    {
                        if (execTask.IsFaulted || execTask.IsCanceled)
                        {
                            Debug.Log("coudlnt read user info");
                        }
                        else if (execTask.IsCompleted)
                        {
                            if (execTask.Result.Child(user.UserId).Exists)
                            {
                                DataSnapshot data = execTask.Result.Child(user.UserId);
                                u = JsonUtility.FromJson<User>(data.GetRawJsonValue());
                                //u = (User)(Json.Deserialize(data.GetRawJsonValue()));
                            }
                        }
                    }); ;  // Very important in order to propagate exceptions
                }
                else
                {
                    throw new TimeoutException("The operation has timed out.");
                }
            }
            return u;
        }

        public void UserUpdateCurrency(Currency currency, int newVal)
        {
            string currStr = (currency == Currency.Coins) ? "Coins" : "Emeralds";
            dbRef.Child("users/" + user.UserId).Child(currStr).SetValueAsync(newVal);
        }

        public async Task ReadUserCreateEmptyIfNotExistInDB()
        {
            try { 
                User readUser = await ReadUserData(TimeoutMillis);
                if (readUser == null)
                {
                    await createNewUserInDB();
                }
            } catch (Exception e)
            {
                Debug.LogError("there was an error creating the user");
            }
        }

        private async Task createNewUserInDB()
        {
            User newUser = new User();
            string dataForJson = newUser.ToJson();
            await dbRef.Child(user.UserId).SetRawJsonValueAsync(dataForJson);
        }
    }
}