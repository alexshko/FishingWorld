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
        public static string PREFS_ACCESS_TOKEN = "access.token";
        public static string PREFS_USER = "user";
        public AccessToken accTo { get; set; }
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
        }
        // Start is called before the first frame update
        public void LogInWithFacebook()
        {
            var perms = new List<string>() { "public_profile", "email" };
            FB.LogInWithReadPermissions(perms, FacebookLoginAuthCallback);
        }

        private void FacebookLoginAuthCallback(ILoginResult result)
        {
            if (FB.IsLoggedIn)
            {
                accTo = result.AccessToken;
                PlayerPrefs.SetString(Login.PREFS_ACCESS_TOKEN, accTo.ToJson());
                PlayerPrefs.SetString(Login.PREFS_USER, accTo.UserId);
                StartCoroutine(LoadMainMenu());
            }
            else
            {
                Debug.LogFormat("Failed to connect with Facebook {0}", result.Error);
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
    }
}
