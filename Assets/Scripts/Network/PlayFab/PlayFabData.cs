using EMA.Scripts.PatternClasses;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Network.PlayFab
{
    public class PlayFabData : MonoSingleton<PlayFabData>
    {




        #region Register Data
        [Header("Register Elements")]
        [SerializeField] private Button registerButton;
        [SerializeField] private TMP_Text notificationRegisterText;
        [SerializeField] private TMP_InputField userRegisterUserNameTxt;
        [SerializeField] private TMP_InputField userRegisterEmailTxt;
        [SerializeField] private TMP_InputField userRegisterPasswordTxt;
        [SerializeField] private TMP_InputField userRegisterPasswordConfirmTxt;
      
        
        
        public Button RegisterButton
        {
            get => registerButton;
            set => registerButton = value;
        }
        
        public TMP_Text NotificationRegisterText
        {
            get => notificationRegisterText;
            set => notificationRegisterText = value;
        }

        public TMP_InputField UserRegisterUserNameTxt
        {
            get => userRegisterUserNameTxt;
            set => userRegisterUserNameTxt = value;
        }

        public TMP_InputField UserRegisterEmailTxt
        {
            get => userRegisterEmailTxt;
            set => userRegisterEmailTxt = value;
        }

        public TMP_InputField UserRegisterPasswordTxt
        {
            get => userRegisterPasswordTxt;
            set => userRegisterPasswordTxt = value;
        }

        public TMP_InputField UserRegisterPasswordConfirmTxt
        {
            get => userRegisterPasswordConfirmTxt;
            set => userRegisterPasswordConfirmTxt = value;
        }

        #endregion

        #region Login Data
        
        [Header("Login Elements")]
        [SerializeField] private Button loginButton;
        [SerializeField] private TMP_Text notificationLoginText;
        [SerializeField] private TMP_InputField userLoginTxt;
        [SerializeField] private TMP_InputField userLoginPasswordTxt;
        
        public Button LoginButton
        {
            get => loginButton;
            set => loginButton = value;
        }
        
        public TMP_Text NotificationLoginText
        {
            get => notificationLoginText;
            set => notificationLoginText = value;
        }

        public TMP_InputField UserLoginTxt
        {
            get => userLoginTxt;
            set => userLoginTxt = value;
        }

        public TMP_InputField UserLoginPasswordTxt
        {
            get => userLoginPasswordTxt;
            set => userLoginPasswordTxt = value;
        }

        #endregion

 
    
    }
}