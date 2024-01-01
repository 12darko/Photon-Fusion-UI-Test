using System;
using System.Collections.Generic;
using Fusion;
using Fusion.Sockets;
using Network.Room;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Image = UnityEngine.UIElements.Image;

namespace Network.NetworkPlayer
{
    public class PlayerRoomSpawnerController : NetworkBehaviour, IPlayerLeft
    {
     

     


        public override void FixedUpdateNetwork()
        {
            foreach (var card in FindObjectsOfType<PlayerCard>())
            {
                RPC_SetParent(card.GetComponent<NetworkObject>());
            }
        }

        public override void Spawned()
        {
            var playerInRoomContent = FindObjectOfType<RoomContent>().GetComponent<NetworkObject>();
            Runner.SetPlayerAlwaysInterested(Runner.LocalPlayer, playerInRoomContent, true);


        }


        [Rpc]
        private void RPC_SetParent(NetworkObject networkObject)
        {
            var playerCard = networkObject.transform;
            if (playerCard == null) return;
            playerCard.SetParent(FindObjectOfType<RoomContent>().transform);
            playerCard.localScale = new Vector3(1, 1, 1);
        }


        public void PlayerLeft(PlayerRef player)
        {
            if (Runner.IsServer)
            {
                if (Runner.TryGetPlayerObject(player, out var playerNetworkObject))
                {
                    Runner.Despawn(playerNetworkObject);
                }
            }
        }


    }
}