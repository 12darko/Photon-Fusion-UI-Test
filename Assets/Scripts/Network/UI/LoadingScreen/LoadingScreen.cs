using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using DG.Tweening;
using Fusion;
using Network.Runner;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


namespace Network.UI.LoadingScreen
{
    public class LoadingScreen : NetworkBehaviour
    {
        public GameObject loadingScreen;
        public NetworkRunnerController networkRunnerController;


        private void Start()
        {
            networkRunnerController = GlobalManager.Instance.NetworkRunnerController;
            networkRunnerController.OnPlayerJoinedSuccessfully += NetworkRunnerControllerOnOnPlayerJoinedSuccessfully;
            networkRunnerController.OnStartedRunnerConnection += NetworkRunnerControllerOnOnStartedRunnerConnection;
        }

        private void NetworkRunnerControllerOnOnStartedRunnerConnection()
        {
            loadingScreen.SetActive(true);
       
            loadingScreen.GetComponent<CanvasGroup>().DOFade(1f, 0.5f).SetEase(Ease.Linear);
        }

        private void NetworkRunnerControllerOnOnPlayerJoinedSuccessfully()
        {
            loadingScreen.GetComponent<CanvasGroup>().DOFade(0f, 0.5f).SetEase(Ease.Linear).OnComplete(() => loadingScreen.SetActive(false));
            
        }

        private void OnDestroy()
        {
           networkRunnerController.OnPlayerJoinedSuccessfully -= NetworkRunnerControllerOnOnPlayerJoinedSuccessfully;
           networkRunnerController.OnStartedRunnerConnection -= NetworkRunnerControllerOnOnStartedRunnerConnection;
        }
    }
}