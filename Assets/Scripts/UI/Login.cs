using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Include Facebook namespace
using Facebook.Unity;
using System;
using UnityEngine.SceneManagement;

namespace alexshko.fishingworld.UI
{
    public class Login : MonoBehaviour
    {
        //public static string PREFS_ACCESS_TOKEN = "access.token";
        public static string PREFS_NAME = "user.name";
        public static string PREFS_USER = "user.id";
        private void Awake()
        {
            if (!FB.IsInitialized)
            {
                // Initialize the Facebook SDK
                FB.Init(InitCallback, OnHideUnity);
            }
            else
            {
                // Already initialized, signal an app activation App Event
                FB.ActivateApp();
            }
            //need to check if has the latest google services sdk, if not then update it.
            CheckCorrectSDK();
        }

        //Start is called before the first frame update
        public void LogInWithFacebook()
        {
            var perms = new List<string>() { "public_profile", "email" };
            FB.LogInWithReadPermissions(perms, FacebookLoginAuthCallback);
        }

        private void FacebookLoginAuthCallback(ILoginResult result)
        {
            if (FB.IsLoggedIn)
            {
                PlayerPrefs.SetString(Login.PREFS_USER, result.AccessToken.UserId);
                //get the user's name from Facebook graph and update to the Prefs:
                FB.API("/me?fields=name", HttpMethod.GET, UpdateFirstNameInPrefs);
                //Load the Main Scene:
                StartCoroutine(LoadMainMenu());
            }
            else
            {
                Debug.LogFormat("Failed to connect with Facebook {0}", result.Error);
            }
        }

        private void UpdateFirstNameInPrefs(IGraphResult result)
        {
            if (result.Error != null)
            {
                Debug.LogError(result.ResultDictionary["name"].ToString());
                PlayerPrefs.SetString(Login.PREFS_NAME, result.ResultDictionary["name"].ToString());
            }
        }

        private IEnumerator LoadMainMenu()
        {
            AsyncOperation ao = SceneManager.LoadSceneAsync(1);
            while (!ao.isDone)
            {
                yield return null;
            }
        }

        private void OnHideUnity(bool isUnityShown)
        {
            if (!isUnityShown)
            {
                // Pause the game - we will need to hide
                Time.timeScale = 0;
            }
            else
            {
                // Resume the game - we're getting focus again
                Time.timeScale = 1;
            }
        }

        private void InitCallback()
        {
            if (FB.IsInitialized)
            {
                // Signal an app activation App Event
                FB.ActivateApp();
                // Continue with Facebook SDK
                // ...
            }
            else
            {
                Debug.Log("Failed to Initialize the Facebook SDK");
            }
        }

        private void CheckCorrectSDK()
        {
            Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
            {
                var dependencyStatus = task.Result;
                if (dependencyStatus == Firebase.DependencyStatus.Available)
                {
                    // Create and hold a reference to your FirebaseApp,
                    // where app is a Firebase.FirebaseApp property of your application class.

                    // Set a flag here to indicate whether Firebase is ready to use by your app.
                }
                else
                {
                    UnityEngine.Debug.LogError(System.String.Format(
                      "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
                    // Firebase Unity SDK is not safe to use here.
                }
            });
        }
    }
}
