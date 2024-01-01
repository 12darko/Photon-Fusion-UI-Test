using System;
using System.Collections;
using System.Collections.Generic;
using Network;
using Network.PlayFab;
using PlayFab;
using PlayFab.ClientModels;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayFabManager : MonoBehaviour
{
    private void Start()
    {
        var globalManager = PlayFabData.Instance;
        globalManager.LoginButton.onClick.AddListener(LoginPlayFab);
        globalManager.RegisterButton.onClick.AddListener(RegisterPlayFab);
    }


    private void RegisterPlayFab()
    {
        var globalManager =PlayFabData.Instance;

        globalManager.NotificationRegisterText.gameObject.SetActive(true);
        if (string.IsNullOrWhiteSpace(globalManager.UserRegisterUserNameTxt.text) ||
            string.IsNullOrWhiteSpace(globalManager.UserRegisterEmailTxt.text) ||
            string.IsNullOrWhiteSpace(globalManager.UserRegisterPasswordTxt.text) ||
            string.IsNullOrWhiteSpace(globalManager.UserRegisterPasswordConfirmTxt.text))
        {
            globalManager.NotificationRegisterText.gameObject.SetActive(true);
            globalManager.NotificationRegisterText.GetComponent<TMP_Text>().color = Color.red;
            globalManager.NotificationRegisterText.text = "Boş Alan Bırakmayın";
        }
        else if (globalManager.UserRegisterPasswordTxt.text != globalManager.UserRegisterPasswordConfirmTxt.text)
        {
            globalManager.NotificationRegisterText.gameObject.SetActive(true);
            globalManager.NotificationRegisterText.GetComponent<TMP_Text>().color = Color.red;
            globalManager.NotificationRegisterText.text = "Şifreler uyuşmuyor";
        }
        else if (globalManager.UserRegisterPasswordTxt.text.Length < 6)
        {
            globalManager.NotificationRegisterText.gameObject.SetActive(true);
            globalManager.NotificationRegisterText.GetComponent<TMP_Text>().color = Color.red;
            globalManager.NotificationRegisterText.text = "Şifre çok kısa";
        }
        else
        {
            var request = new RegisterPlayFabUserRequest()
            {
                Email = globalManager.UserRegisterEmailTxt.text,
                Username = globalManager.UserRegisterUserNameTxt.text,
                Password = globalManager.UserRegisterPasswordTxt.text,
                RequireBothUsernameAndEmail = false,
                InfoRequestParameters = new GetPlayerCombinedInfoRequestParams()
                {
                    GetPlayerProfile = true
                }
            };
            PlayFabClientAPI.RegisterPlayFabUser(request, OnRegisterSuccess, OnError);
        }
    }


    #region PlayFabMethod

    private void OnRegisterSuccess(RegisterPlayFabUserResult result)
    {
        var globalManager = PlayFabData.Instance;

        globalManager.NotificationRegisterText.gameObject.SetActive(true);
        globalManager.NotificationRegisterText.GetComponent<TMP_Text>().color = Color.green;
        globalManager.NotificationRegisterText.text = "Kayıt Başarılı,";
    }

    private void OnLoginSuccess(LoginResult obj)
    {
        var instancePlayFabData = PlayFabData.Instance;
        var instancePlayerData = GlobalManager.Instance.PlayerData;

        instancePlayFabData.NotificationLoginText.gameObject.SetActive(true);
        instancePlayFabData.NotificationLoginText.GetComponent<TMP_Text>().color = Color.green;
        instancePlayFabData.NotificationLoginText.text = "Hoş geldin";

        UIData.Instance.LoginPanel.SetActive(false);
        UIData.Instance.MainPanel.SetActive(true);

        instancePlayerData.SetPlayerName(instancePlayFabData.UserLoginTxt.text);
    }

    private void OnError(PlayFabError result)
    {
        var globalManager =PlayFabData.Instance;
        globalManager.NotificationLoginText.gameObject.SetActive(true);
        globalManager.NotificationLoginText.GetComponent<TMP_Text>().color = Color.red;
        globalManager.NotificationLoginText.text = PlayFabStaticErrorData.ErrorController(result) ;
 
        Debug.Log(result.Error);
    }

    #endregion


    private void LoginPlayFab()
    {
        var globalManager = PlayFabData.Instance;
        if (string.IsNullOrWhiteSpace(globalManager.NotificationLoginText.text) ||
            string.IsNullOrWhiteSpace(globalManager.UserLoginPasswordTxt.text))
        {
            globalManager.NotificationLoginText.gameObject.SetActive(true);
            globalManager.NotificationLoginText.GetComponent<TMP_Text>().color = Color.red;
            globalManager.NotificationLoginText.text = "Boş Alan Bırakmayın";
        }
        else
        {
            var request = new LoginWithPlayFabRequest()
            {
                Username = globalManager.UserLoginTxt.text,
                Password = globalManager.UserLoginPasswordTxt.text,
            };
            PlayFabClientAPI.LoginWithPlayFab(request, OnLoginSuccess, OnError);
        }
    }
}