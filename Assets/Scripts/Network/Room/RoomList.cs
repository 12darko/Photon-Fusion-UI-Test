using System;
using Fusion;
using Network.Runner;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Network.Room
{
    public class RoomList : RoomManager
    {
        public TMP_Text sessionStatusText;
        public GameObject roomListItem;
        public VerticalLayoutGroup roomListGrid;
      
        public event Action OnStartedJoinRunnerConnection;
        public event Action OnJoinPlayerSuccessfully;
        private void Awake()
        {
            ClearList();
        }

        public void ClearList()
        {
            foreach (Transform child in roomListGrid.transform)
            {
                Destroy(child.gameObject);
            }

            sessionStatusText.gameObject.SetActive(false);
        }


        public void AddToList(SessionInfo sessionInfo)
        {
            var roomItem = Instantiate(roomListItem, roomListGrid.transform).GetComponent<RoomItem>();
            roomItem.SetInformation(sessionInfo);

            roomItem.OnJoinSession += AddedRoomInfoListOnItem_OnJoinSession;
        }

        private void AddedRoomInfoListOnItem_OnJoinSession(SessionInfo sessionInfo)
        {
            JoinGame(sessionInfo.Name, 2, sessionInfo.MaxPlayers, sessionInfo.IsVisible, sessionInfo.IsVisible);
        }

        public void OnNoSessionFound()
        {
            ClearList();

            sessionStatusText.text = "Şuan Aktif Bir Oyun Bulunamadı.";
            sessionStatusText.gameObject.SetActive(true);
        }

        public void OnLookingForGameSession()
        {   
            ClearList();
            
            sessionStatusText.text = "Aktif Bir Oyun Aranıyor...";
            sessionStatusText.gameObject.SetActive(true);
        }
    }
}