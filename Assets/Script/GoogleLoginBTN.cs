using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Google;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GoogleLoginBTN : MonoBehaviour
{
    public string userName, userImgUrl;
    public TMP_Text userNameText;
    public Image userImg;

    public string webClientId = "885283006667-al4mmd3b3t5upvsmptpregu839fi4r4k.apps.googleusercontent.com";

    private GoogleSignInConfiguration configuration;

    // Defer the configuration creation until Awake so the web Client ID
    // Can be set via the property inspector in the Editor.
    void Awake()
    {
        configuration = new GoogleSignInConfiguration
        {
            WebClientId = webClientId,
            RequestIdToken = true
        };
        userImg.sprite = null;
        userNameText.text = null;
    }

    public void OnSignIn()
    {
        GoogleSignIn.Configuration = configuration;
        GoogleSignIn.Configuration.UseGameSignIn = false;
        GoogleSignIn.Configuration.RequestIdToken = true;
        GoogleSignIn.Configuration.RequestEmail = true;

        GoogleSignIn.DefaultInstance.SignIn().ContinueWith(
          OnAuthenticationFinished, TaskScheduler.Default);
    }

    internal void OnAuthenticationFinished(Task<GoogleSignInUser> task)
    {
        if (task.IsFaulted)
        {
            using (IEnumerator<System.Exception> enumerator =
                    task.Exception.InnerExceptions.GetEnumerator())
            {
                if (enumerator.MoveNext())
                {
                    GoogleSignIn.SignInException error =
                            (GoogleSignIn.SignInException)enumerator.Current;
                    userNameText.text = "Got Error: " + error.Status + " " + error.Message;
                }
                else
                {
                    userNameText.text = "Got Unexpected Exception?!?" + task.Exception;
                }
            }
        }
        else if (task.IsCanceled)
        {
            userNameText.text = "Canceled";
        }
        else
        {
            userName = task.Result.DisplayName;
            userImgUrl = task.Result.ImageUrl.ToString(); ;
            userNameText.text = userName;

            StartCoroutine(LoadIMG(userImgUrl));
        }
    }

    public void OnSignOut()
    {
        GoogleSignIn.DefaultInstance.SignOut();
        userImg.sprite = null;
        userNameText.text = null;
    }

    IEnumerator LoadIMG(string imgURL)
    {
        WWW www = new WWW(imgURL);
        yield return www;

        userImg.sprite = Sprite.Create(www.texture, new Rect(0, 0, www.texture.width, www.texture.height), new Vector2(0, 0));
    }

}
