using System;
using EMA.Scripts.PatternClasses;
using Network.NetworkPlayer;
using Network.PlayFab;
using Network.Room;
using Network.Runner;
using UnityEngine;
using Utils;

namespace Network
{
    public class GlobalManager : MonoBehaviour
    {
        public static  GlobalManager Instance { get; private set; }

        [SerializeField] private DDOL parentObj;
        [field: SerializeField] public NetworkRunnerController NetworkRunnerController { get; private set; }
        [field: SerializeField] public PlayerData PlayerData { get; private set; }

        
        [field: SerializeField] public PlayerSpawnController PlayerSpawnController { get; private set; }
        
 
        public DDOL ParentObj
        {
            get => parentObj;
            set => parentObj = value;
        }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(parentObj.gameObject);
            }
        }

        private void Start()
        {
            PlayerSpawnController = FindObjectOfType<PlayerSpawnController>();
        }
    }
}