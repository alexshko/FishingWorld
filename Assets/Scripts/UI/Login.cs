using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Include Facebook namespace
using Facebook.Unity;
using System;
using UnityEngine.SceneManagement;
using Firebase.Auth;
using Firebase.Extensions;
using alexshko.fishingworld.Core.DB;

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
            try
            {
                if (!FB.IsInitialized)
                {
                    // Initialize the Facebook SDK
                    FB.Init(FacebookInitCallback, OnHideUnity);
                }
                else
                {
                    // Already initialized, signal an app activation App Event
                    FB.LogOut();
                    FB.ActivateApp();
                }
            }
            catch (Exception e)
            {
                Debug.LogError("error: " + e.Message);
            }
        }

        private void FacebookInitCallback()
        {
            if (FB.IsInitialized)
            {
                //incase the user didn't logout correctly last time:
                FB.LogOut();
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

        //called from Button in the game:
        public void LogInWithFacebook()
        {
            StartCoroutine(FacebookLoginRoutine());
        }

        private IEnumerator FacebookLoginRoutine()
        {
            while (!FB.IsInitialized)
            {
                yield return new WaitForSeconds(2);
            }
            try
            {
                var perms = new List<string>() { "gaming_profile", "email" };
                FB.LogInWithReadPermissions(perms, FacebookLoginAuthCallback);
            }
            catch (Exception e)
            {
                Debug.Log("Exception occured during login from facebook: " + e.Message);
            }
        }

        //called from function FacebookLoginRoutine after tried to log into facebook account:
        private void FacebookLoginAuthCallback(ILoginResult result)
        {
            if (FB.IsLoggedIn)
            {
                FireBaseLoginOfFacebook(result.AccessToken);

                //get the user's name from Facebook graph and update to the Prefs:
                FB.API("me?fields=name", HttpMethod.GET, UpdatePrefsName);
            }
            else
            {
                Debug.LogFormat("Failed to connect with Facebook {0}", result.Error);
            }
        }

        //called from FacebookLoginAuthCallback once it loggen to facebook successfuly:
        private void UpdatePrefsName(IGraphResult result)
        {
            if (result.Error == null)
            {
                Debug.Log(result.ResultDictionary["name"].ToString());
                PlayerPrefs.SetString(Login.PREFS_NAME, result.ResultDictionary["name"].ToString());
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
            auth.SignOut();
            auth.StateChanged += FireBaseAuthStateChanged;
            //FireBaseAuthStateChanged(this, null);
        }

        private void FireBaseLoginOfFacebook(AccessToken accessToken)
        {
            //auth.SignOut();
            Firebase.Auth.Credential credential = Firebase.Auth.FacebookAuthProvider.GetCredential(accessToken.TokenString);
            auth.SignInWithCredentialAsync(credential).ContinueWithOnMainThread(task =>
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
                
                if (signedIn)
                {
                    user = auth.CurrentUser;
                    PlayerPrefs.SetString(Login.PREFS_USER, user.UserId);
                    PlayerPrefs.SetString(Login.PREFS_NAME, user.DisplayName);
                    

                    Debug.Log("Firebase Signed in " + user.UserId);
                    Debug.Log(user.DisplayName ?? "");

                    await UserFirebaseDataBase.instance.ReadUserCreateEmptyIfNotExistInDB();
                    UserFirebaseDataBase.instance.ReadUserData(UpdateUser);

                    //Load the Main Scene:
                    StartCoroutine(LoadMainMenu());
                }
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
    }
}
