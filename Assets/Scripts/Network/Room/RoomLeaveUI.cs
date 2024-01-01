using System;
using Network.UI.LoadingScreen;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Network.Room
{
    public class RoomLeaveUI : MonoBehaviour
    {
        [SerializeField] private PlayerData playerData;

        private void FixedUpdate()
        {
            if (playerData == null)
                playerData = FindObjectOfType<PlayerData>();
            
            if (!string.IsNullOrEmpty(playerData.PlayerName) && SceneManager.GetActiveScene().name == "LobbyScenes")
            {
                UIData.Instance.LoginPanel.SetActive(false);
           
                UIData.Instance.MainPanel.SetActive(true);
            }
  
        }
    }
}