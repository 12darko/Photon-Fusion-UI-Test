using System;
using System.Collections.Generic;
using Fusion;
using Fusion.Sockets;
using Network.Room;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Network.NetworkPlayer
{
    public class PlayerSpawnController : NetworkBehaviour, INetworkRunnerCallbacks
    {
        [Header("In Game Object")]
        private Dictionary<PlayerRef, NetworkObject> _spawnedCharacters = new Dictionary<PlayerRef, NetworkObject>();

        private NetworkObject _networkPlayerObject;

        [SerializeField] private NetworkObject playersPrefab;
        [SerializeField] private Transform[] spawnPoints;
        [SerializeField] private RoomList roomList;
 
        private void Awake()
        {
            roomList = FindObjectOfType<RoomList>(true);
        }

        private void FixedUpdate()
        {
            if (roomList == null && SceneManager.GetActiveScene().name =="LobbyScenes")
                roomList = FindObjectOfType<RoomList>(true);
            
        }


        public void OnEnable()
        {
            if (Runner != null)
            {
                Runner.AddCallbacks(this);
            }
        }

        public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
        {
            //Biz şuan ki sunucu sahibiyiz farklı bir player katıldığında tetikleniyor

            if (runner.IsServer)
            {
                if (SceneManager.GetActiveScene().name == "RoomScene")
                {
                    Debug.Log("OnPlayerJoined, player spawn oluyor");
                    _networkPlayerObject = Runner.Spawn(playersPrefab, transform.position, Quaternion.identity, player,
                        onBeforeSpawned:
                        (runner, o) =>
                        {
                            var networkObject = o.GetComponent<NetworkObject>();

                            Runner.SetPlayerObject(player, networkObject);
                        });
                    runner.SetPlayerObject(player, _networkPlayerObject);

 
               
                    _spawnedCharacters.Add(player, _networkPlayerObject);
                    
                    
                }
            }
            else
            {
                Debug.Log("OnPlayerJoined");
            }
        }

        public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
        {
            // Find and remove the players avatar
            if (_spawnedCharacters.TryGetValue(player, out NetworkObject networkObject))
            {
                runner.Despawn(networkObject);

                _spawnedCharacters.Remove(player);
            }
        }

        public void OnInput(NetworkRunner runner, NetworkInput input)
        {
        }

        public void OnDisable()
        {
            if (Runner != null)
            {
                Runner.RemoveCallbacks(this);
            }
        }

        public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input)
        {
        }

        public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason)
        {
        }

        public void OnConnectedToServer(NetworkRunner runner)
        {
        }

        public void OnDisconnectedFromServer(NetworkRunner runner)
        {
        }

        public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request,
            byte[] token)
        {
        }

        public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason)
        {
        }

        public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message)
        {
        }

        public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList)
        {
            if (roomList == null)
                return;

            if (sessionList.Count == 0)
            {
                Debug.Log("Joined Lobby No Session Found");

                roomList.OnNoSessionFound();
            }
            else
            {
                roomList.ClearList();

                foreach (var sessionInfo in sessionList)
                {
                    roomList.AddToList(sessionInfo);

                    Debug.Log($"Found Session {sessionInfo.Name} player count {sessionInfo.PlayerCount}");
                }
            }
        }

        public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data)
        {
        }

        public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken)
        {
        }

        public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ArraySegment<byte> data)
        {
        }

        public void OnSceneLoadDone(NetworkRunner runner)
        {
        }

        public void OnSceneLoadStart(NetworkRunner runner)
        {
        }
    }
}