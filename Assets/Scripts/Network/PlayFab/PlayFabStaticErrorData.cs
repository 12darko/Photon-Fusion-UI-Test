using Network.NetworkPlayer;
using PlayFab;
using TMPro;
using UnityEngine;

namespace Network.PlayFab
{
    public static class PlayFabStaticErrorData
    {

        private static string notifyValue;
        
        public static string ErrorController(PlayFabError result)
        {
            switch (result.Error)
            {
              case PlayFabErrorCode.AccountNotFound:
                  notifyValue = "Böyle bir Hesap bulunmuyor";
                  break;
              case PlayFabErrorCode.InvalidUsernameOrPassword:
                  notifyValue = "Kulllanıcı adı veya şifre yanlış";
                  break;
            }

            return notifyValue;
        }
    }
}