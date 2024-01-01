using System;
using Fusion;
using Network.Lobby;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Network.NetworkPlayer
{
    public class PlayerCard : NetworkBehaviour, IPlayerLeft
    {
        public  static PlayerCard Local { get; set; }
        //Serialize Variables
        [SerializeField] private Color[] playerColorList;
        [SerializeField] private Button kickBtn;

        [SerializeField] private NetworkObject cardBackUI;
        [SerializeField] private NetworkObject carFrontUI;

        [Networked(OnChanged = nameof(OnChangedNickName))]
        private NetworkString<_8> playerName { get; set; }

        [SerializeField, Networked] private NetworkObject playerNameTxt { get; set; }


        [SerializeField, Networked] private int colorId { get; set; }

       [SerializeField] private NetworkLobbyMessage networkLobbyMessage;

        #region Variables

        //Private Variables
        private bool _isColorUsed;
        private bool _isJoinMessageSend = false;

        //Public Variables
        public Button KickBtn
        {
            get => kickBtn;
            set => kickBtn = value;
        }

        #endregion
     
    

        private void Awake()
        {
            networkLobbyMessage = FindObjectOfType<NetworkLobbyMessage>();
        }

        private void Start()
        {
            colorId = FindObjectsOfType<PlayerCard>().Length - 1;
        }


        public override void Spawned()
        {
            if (Object.HasInputAuthority)
            {
                Local = this;
            }
            RPC_ColorChange();

            SetLocalObject();
            
            if(networkLobbyMessage == null)
                networkLobbyMessage = FindObjectOfType<NetworkLobbyMessage>();
            
            networkLobbyMessage.LobbyMessages.ChatTextInput.onSubmit.AddListener((delegate(string arg0)
            {
                SendMessage(Runner.LocalPlayer);  
            }));
          
        }
 
        private void SetLocalObject()
        {
            if (Runner.LocalPlayer == Object.HasInputAuthority)
            {
                RPC_SetNickName(GlobalManager.Instance.PlayerData.PlayerName);
            }
        }

        [Rpc]
        private void RPC_ColorChange()
        {
            cardBackUI.GetComponent<Image>().color = UIData.Instance.PlayerColorList[colorId];
            carFrontUI.GetComponent<Image>().color = UIData.Instance.PlayerColorList[colorId];
        }

        [Rpc(RpcSources.InputAuthority, RpcTargets.StateAuthority)]
        private void RPC_SetNickName(NetworkString<_8> nickName)
        {
            playerName = nickName;
            if (!_isJoinMessageSend)
            {
                networkLobbyMessage.SendInLobbyRPCMessages(nickName.ToString(), " Joined");

                _isJoinMessageSend = true;
            }
        }

        private void SetPlayerNickName(NetworkString<_8> nickName)
        {
            playerNameTxt.GetComponent<TMP_Text>().text = nickName.ToString();
        }

        private static void OnChangedNickName(Changed<PlayerCard> changed)
        {
            var nickName = changed.Behaviour.playerName;
            changed.Behaviour.SetPlayerNickName(nickName);
        }

        public void PlayerLeft(PlayerRef player)
        {
            if (Object.HasStateAuthority)
            {
                if (Runner.TryGetPlayerObject(player, out var playerNetworkObject))
                {
                    if (playerNetworkObject == Object)
                        networkLobbyMessage.SendInLobbyRPCMessages(
                            playerNetworkObject.GetComponent<PlayerCard>().playerName.ToString(), " Left");
                }
            }
        }

        
        private void SendMessage(PlayerRef player)
        {
            if (!Runner.TryGetPlayerObject(player, out var playerNetworkObject)) return;
            if (playerNetworkObject != Object) return;
            if (!Input.GetKeyDown(KeyCode.Return) && !Input.GetKeyDown(KeyCode.KeypadEnter)) return;
            networkLobbyMessage.SendInLobbyRPCWithPlayerMessages(playerNetworkObject.GetComponent<PlayerCard>().playerName + " : ",
                networkLobbyMessage.LobbyMessages.ChatTextInput.text);
            networkLobbyMessage.LobbyMessages.ChatTextInput.text = "";
            networkLobbyMessage.LobbyMessages.ChatTextInput.Select();
            networkLobbyMessage.LobbyMessages.ChatTextInput.ActivateInputField();
            networkLobbyMessage.LobbyMessages.ChatTextInput.onFocusSelectAll = false;


        }
    }
}