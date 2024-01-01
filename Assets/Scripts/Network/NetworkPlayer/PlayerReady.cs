using System;
using Fusion;
using Network.Room;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
 

namespace Network.NetworkPlayer
{
    public class PlayerReady : NetworkBehaviour
    {        
        [Networked(OnChanged = nameof(OnReadySectionCompleteDone))]
        public NetworkBool isCompleteRoomSection { get; set; }
               
        [SerializeField] private bool isReady;
        [SerializeField] private TMP_Text readyBtnText;
     
        [SerializeField] private Sprite readyBtnTemplate;
        [SerializeField] private Sprite notReadyBtnTemplate;

        [SerializeField]
        private RoomInManager roomInManager;
 
        public Button readyBtn;

        
        public Sprite ReadyBtnTemplate
        {
            get => readyBtnTemplate;
            set => readyBtnTemplate = value;
        }

        public Sprite NotReadyBtnTemplate
        {
            get => notReadyBtnTemplate;
            set => notReadyBtnTemplate = value;
        }

        private void Awake()
        {
            roomInManager = FindObjectOfType<RoomInManager>();
        }

        public override void Spawned()
        {
            if (roomInManager == null)
                roomInManager = FindObjectOfType<RoomInManager>();
        }

        private void Start()
        {
            readyBtn.onClick.AddListener(OnReady);
        }


        private void OnReady()
        {
            if (isReady)
            {
                isReady = false;
  
            }
            else
            {
                isReady = true;
 
            }
            if (Runner.IsServer)
            {
                //StartGame   
                if (isReady && GlobalManager.Instance.PlayerData.IsLeader)
                {
                    //Countdown Saydırılabilir
                   roomInManager.StartGameButton.gameObject.SetActive(true);
                    Debug.Log("Başlama butonu aktif");
                }
                else
                {
                    roomInManager.StartGameButton.gameObject.SetActive(false);
                }
            }
            OnReady(isReady);
        }
        
        
        public void OnReady(bool isReady)
        {
            if (HasInputAuthority)
            {
                RPC_SetReady(isReady);
            }
        }

        [Rpc(RpcSources.InputAuthority, RpcTargets.StateAuthority)]
        private void RPC_SetReady(NetworkBool isReady, RpcInfo rpcInfo = default)
        {
            isCompleteRoomSection = isReady;
        }

        private static void OnReadySectionCompleteDone(Changed<PlayerReady> changed)
        {
            changed.Behaviour.IsCompleteDone();
        }

        private void IsCompleteDone()
        {
            if (isCompleteRoomSection)
            {
                readyBtn.GetComponent<Image>().sprite =  readyBtnTemplate;
                readyBtnText.text = "Hazır";
            }
            else
            {
                readyBtn.GetComponent<Image>().sprite =  notReadyBtnTemplate;
                readyBtnText.text = "Hazır Değil";
            }
        }

    }
}