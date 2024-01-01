using System;
using System.Collections.Generic;
using Fusion;
using Fusion.Sockets;
using UnityEngine;
using UnityEngine.UI;

namespace Network.NetworkPlayer
{
    public class PlayerKick : NetworkBehaviour
    {
        [SerializeField] private Button disconnectButton;
        [SerializeField] private NetworkObject playerCard;
        private void Start()
        {
            if (HasStateAuthority) //Sunucu Otoritesinden input alan otoriteyi hedef alarak kick işlemini gerçekleştiriyoruz
                disconnectButton.onClick.AddListener(() => RPC_Kick(playerCard.InputAuthority));
        } 

        public override void Spawned()
        {
            playerCard = GetComponent<PlayerCard>().GetComponent<NetworkObject>();
        }

        [Rpc(RpcSources.StateAuthority, RpcTargets.InputAuthority)]
        private void RPC_Kick(PlayerRef playerRef)
        {
            if (Runner.TryGetPlayerObject(playerRef, out var playerNetworkObject))
            {
                playerNetworkObject.Runner.Shutdown(false);
            }
        }
  
    }
}