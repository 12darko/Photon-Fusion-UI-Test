using Fusion;
using UnityEngine;

namespace Network.UI
{
    public class UILobbyMenu : MonoBehaviour 
    {
        public void LeaveLobby()
        {
            UIData.Instance.LobbyPanel.SetActive(false);
            UIData.Instance.MainPanel.SetActive(true);
        }

        public void RoomSettings()
        {
            UIData.Instance.RoomSettingsPanel.SetActive(true);
        }  
   
    }
}