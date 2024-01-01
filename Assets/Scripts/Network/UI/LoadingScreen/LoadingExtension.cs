using Fusion;
using UnityEngine;

namespace Network.UI.LoadingScreen
{
    public static class LoadingExtension
    {
        private static LoadingScreen loadingScreen;
        
        public static LoadingScreen SceneManager(this NetworkRunner runner ) => loadingScreen;
        public static void SetSceneManager(this NetworkRunner runner, LoadingScreen manager) =>loadingScreen = manager;
    }
}