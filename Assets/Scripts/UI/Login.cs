using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Include Facebook namespace
using Facebook.Unity;
using System;
using UnityEngine.SceneManagement;
using Firebase.Auth;

namespace alexshko.fishingworld.UI
{
    public class Login : MonoBehaviour
    {
        //public static string PREFS_ACCESS_TOKEN = "access.token";
        public static string PREFS_NAME = "user.name";
        public static string PREFS_USER = "user.id";

        private FirebaseAuth auth;
        private FirebaseUser user;

        private void Awake()
        {
            InitializeFirebase();
            InitFacebookLogin();
            //need to check if has the latest google services sdk, if not then update it.
            FireBaseCheckCorrectSDK();
        }

        private void InitFacebookLogin()
        {
            if (!FB.IsInitialized)
            {
                // Initialize the Facebook SDK
                FB.Init(FacebookInitCallback, OnHideUnity);
            }
            else
            {
                // Already initialized, signal an app activation App Event
                FB.ActivateApp();
            }
        }


        private void FacebookLoginAuthCallback(ILoginResult result)
        {
            if (FB.IsLoggedIn)
            {
                PlayerPrefs.SetString(Login.PREFS_USER, result.AccessToken.UserId);
                FireBaseUpdateLogin(result.AccessToken);

                //get the user's name from Facebook graph and update to the Prefs:
                FB.API("me?fields=name", HttpMethod.GET, UpdatePrefsAndLoadScene);
            }
            else
            {
                Debug.LogFormat("Failed to connect with Facebook {0}", result.Error);
            }
        }

        public void LogInWithFacebook()
        {
            var perms = new List<string>() { "public_profile", "gaming_profile", "email" };
            FB.LogInWithReadPermissions(perms, FacebookLoginAuthCallback);
        }

        private void UpdatePrefsAndLoadScene(IGraphResult result)
        {
            if (result.Error == null)
            {
                Debug.Log(result.ResultDictionary["name"].ToString());
                PlayerPrefs.SetString(Login.PREFS_NAME, result.ResultDictionary["name"].ToString());
                ////Load the Main Scene:
                //StartCoroutine(LoadMainMenu());
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

        private void FacebookInitCallback()
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

        private void FireBaseCheckCorrectSDK()
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

        void InitializeFirebase()
        {
            auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
            auth.StateChanged += FireBaseAuthStateChanged;
            //FireBaseAuthStateChanged(this, null);
        }

        private void FireBaseUpdateLogin(AccessToken accessToken)
        {
            Debug.Log("expires: " + accessToken.ExpirationTime);
            auth.SignOut();
            Firebase.Auth.Credential credential = Firebase.Auth.FacebookAuthProvider.GetCredential(accessToken.TokenString);
            auth.SignInWithCredentialAsync(credential).ContinueWith(task =>
            {
                if (task.IsCanceled)
                {
                    Debug.LogError("SignInWithCredentialAsync was canceled.");
                    return;
                }
                if (task.IsFaulted)
                {
                    Debug.LogError("SignInWithCredentialAsync encountered an error: " + task.Exception);
                    return;
                }

                Firebase.Auth.FirebaseUser newUser = task.Result;
                Debug.LogFormat("User signed in successfully: {0} ({1})",
                    newUser.DisplayName, newUser.UserId);
            });
        }

        void FireBaseAuthStateChanged(object sender, System.EventArgs eventArgs)
        {
            if (auth.CurrentUser != user)
            {
                bool signedIn = (user != auth.CurrentUser) && (auth.CurrentUser != null);
                if (!signedIn && user != null)
                {
                    Debug.Log("Signed out " + user.UserId);
                }
                user = auth.CurrentUser;
                if (signedIn)
                {
                    Debug.Log("Firebase Signed in " + user.UserId);
                    Debug.Log(user.DisplayName ?? "");
                }
            }
        }
    }
}
