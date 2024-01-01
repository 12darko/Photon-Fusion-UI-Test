using System;
using Fusion;
using Network.NetworkPlayer;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;


namespace Network.Room
{
    public class RoomManager : MonoBehaviour
    {
        protected RoomData RoomData;
        private void Start()
        {
            RoomData = new RoomData();
            UIData.Instance.JoinRandomRoomBtn.onClick.AddListener(JoinRandom);
            
            RoomData.RoomMaxPlayerCount = 2;//Default
            UIData.Instance.CreateRoomBtn.onClick.AddListener(() =>
            { 
                CreateRoom();
            });
            
            UIData.Instance.RoomListBtn.onClick.AddListener(JoinLobby);
        }

        private void JoinLobby()
        {
            
            GlobalManager.Instance.NetworkRunnerController.OnJoinLobby();
            
            FindObjectOfType<RoomList>(true).OnLookingForGameSession();
        }        
        
        private void CreateRoom()
        {
            if (UIData.Instance.RoomName.text.Length > 3)
            {
                GlobalManager.Instance.NetworkRunnerController.StartGame(GameMode.Host, RoomData.RoomName,
                    RoomData.RoomMaxPlayerCount,
                    RoomData.RoomIsVisible, RoomData.RoomIsOpen, 2);
                
                GlobalManager.Instance.PlayerData.SetLeader(true);
                GlobalManager.Instance.PlayerData.SetRoom(true);
            } 
            else
            {
                Debug.Log("Boş Bırakılamaz");
            }
          
        }
        
        private void JoinRandom()
        {
            var roomName=   RoomData.RoomName = "Room " + Random.Range(0, 10000000);
            var roomPlayerCount = Random.Range(2, 8);
            
            GlobalManager.Instance.NetworkRunnerController.StartGame(GameMode.AutoHostOrClient, roomName,
                roomPlayerCount,
                true, true, 2);
            GlobalManager.Instance.PlayerData.SetLeader(true);
            GlobalManager.Instance.PlayerData.SetRoom(true);
        }

        public void JoinGame(string sessionName, int scene, int sessionMaxPlayer, bool sessionIsVisible, bool sessionIsOpen)
        {
            GlobalManager.Instance.NetworkRunnerController.StartGame(GameMode.Client, sessionName,
                sessionMaxPlayer,
                sessionIsVisible, sessionIsOpen, scene);
            GlobalManager.Instance.PlayerData.SetLeader(false);
            GlobalManager.Instance.PlayerData.SetRoom(true);
        }

        
    }
}