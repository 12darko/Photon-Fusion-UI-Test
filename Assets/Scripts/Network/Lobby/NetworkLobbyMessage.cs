using System;
using Fusion;
using PlayFab.ClientModels;
using UnityEngine;

namespace Network.Lobby
{
    public class NetworkLobbyMessage : NetworkBehaviour 
    {
        [SerializeField] private LobbyMessages lobbyMessages;

        public LobbyMessages LobbyMessages => lobbyMessages;


        public void SendInLobbyRPCMessages(string userName, string message)
        {
            RPC_LobbyMessages($"<b>{userName}</b> {message} <br>");
        }

        public void SendInLobbyRPCWithPlayerMessages(string userName, string message)
        {
            RPC_SendLobbyWitPlayer($"<b>{userName}</b> {message} <br>");
        }
        

        [Rpc(RpcSources.StateAuthority, RpcTargets.All)]
        public void RPC_LobbyMessages(string message, RpcInfo rpcInfo = default)
        {
            Debug.Log($"$[PRC] Messages {message}");

            if (message != null)
                lobbyMessages.OnLobbyMessageReceived(message);
        }
        
        [Rpc(RpcSources.All, RpcTargets.All)]
        public void RPC_SendLobbyWitPlayer(string message, RpcInfo rpcInfo = default)
        {
            Debug.Log($"$[PRC] Messages {message}");

            if (message != null)
                lobbyMessages.OnLobbyMessageReceived(message);
        }

    }
}