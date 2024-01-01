using System;
using System.Collections.Generic;
using Fusion;
using Network.NetworkPlayer;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Network.Room
{
    public class RoomInManager : NetworkBehaviour
    {
        [SerializeField] private TMP_Text roomName;
        [SerializeField] private TMP_Text roomPlayerCount;

        [SerializeField] private Button startGameButton;

        public Button StartGameButton
        {
            get => startGameButton;
            set => startGameButton = value;
        }

        public override void Spawned()
        {
            RPC_RoomDetails();
            
            if (!GlobalManager.Instance.PlayerData.IsLeader)
            {
                ActiveKickButton();
            }
            startGameButton.gameObject.SetActive(false);
        }

        public override void FixedUpdateNetwork()
        {
            RPC_RoomDetails();
        }

        [Rpc]
        private void RPC_RoomDetails()
        {
            roomName.text = Runner.SessionInfo.Name;
            roomPlayerCount.text = Runner.SessionInfo.PlayerCount + " / " + Runner.SessionInfo.MaxPlayers;
        }

        private void ActiveKickButton()
        {
            foreach (var playerCard in FindObjectsOfType<PlayerCard>())
            {
                playerCard.KickBtn.gameObject.SetActive(false);
            }
        }
    }
}