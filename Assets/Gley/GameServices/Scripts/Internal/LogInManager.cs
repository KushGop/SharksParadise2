using UnityEngine;
using UnityEngine.Events;

#if GLEY_GAMESERVICES_ANDROID
using GooglePlayGames;
#endif
using Firebase.Auth;

namespace Gley.GameServices.Internal
{
  public class LogInManager
  {
    bool login = false;
    bool activated = false;

    /// <summary>
    /// Authenticate the user for Android and iOS using Unity Social interface
    /// </summary>
    /// <param name="LoginComplete">callback -> login result</param>
    public void LogiInServices(UnityAction<bool> LoginComplete = null)
    {
      if (activated == false)
      {
#if GLEY_GAMESERVICES_ANDROID
        PlayGamesPlatform.Activate();
#endif
        activated = true;
      }
      Social.localUser.Authenticate((bool success) =>
      {
        if (success)
        {
#if GLEY_GAMESERVICES_ANDROID
          string idToken = PlayGamesPlatform.Instance.localUser.id;
          FirebaseAuth auth = FirebaseAuth.DefaultInstance;
          Credential credential = GoogleAuthProvider.GetCredential(idToken, null);
          FirebaseAuth.DefaultInstance.SignInWithCredentialAsync(credential).
          ContinueWith(task =>
          {
            if (task.IsCompleted) Debug.Log("Firebase signed-in");
            else Debug.Log("Firebase sign-in failed");
          });
#endif
          login = true;


          if (LoginComplete != null)
          {
            LoginComplete(true);
          }
        }
        else
        {
          if (LoginComplete != null)
          {
            LoginComplete(false);
          }
        }
      });
    }

    /// <summary>
    /// check if user is logged in
    /// </summary>
    /// <returns>true of the user is logged in</returns>
    public bool IsLoggedIn()
    {
      return login;
    }
  }
}
