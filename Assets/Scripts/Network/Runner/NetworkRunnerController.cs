using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Fusion;
using Fusion.Sockets;
using Network.NetworkData;
using Network.NetworkPlayer;
using Network.Room;
using Network.UI.LoadingScreen;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Network.Runner
{
    public class NetworkRunnerController : MonoBehaviour, INetworkRunnerCallbacks
    {
        [SerializeField] private NetworkRunner networkRunnerPrefab;
        private NetworkRunner _networkRunnerInstance;

        public NetworkRunner NetworkRunnerInstance => _networkRunnerInstance;

        public event Action OnStartedRunnerConnection;
        public event Action OnPlayerJoinedSuccessfully;

        public CustomSceneManager customSceneManager;

        private bool _isJoinLobby = false;


        private void Awake()
        {
            NetworkRunner networkRunnerInScene = FindObjectOfType<NetworkRunner>();

            if (networkRunnerInScene != null)
            {
                _networkRunnerInstance = networkRunnerInScene;
            }
        }

        private void Start()
        {
            if (_networkRunnerInstance == null)
            {
                _networkRunnerInstance = Instantiate(networkRunnerPrefab,  GlobalManager.Instance.ParentObj.transform);
            }
        }

        public async void StartGame(GameMode mode, string roomName, int roomPlayerCount, bool roomIsVisible,
            bool roomIsOpen, SceneRef sceneRef)
        {
            OnStartedRunnerConnection?.Invoke();

            if (_networkRunnerInstance == null)
            {
                _networkRunnerInstance = Instantiate(networkRunnerPrefab, GlobalManager.Instance.ParentObj.transform);
            }

            _networkRunnerInstance.AddCallbacks(this);

            _networkRunnerInstance.ProvideInput = true;

            customSceneManager = _networkRunnerInstance.GetComponent<CustomSceneManager>();
             var startGameArgs = new StartGameArgs()
            {
                GameMode = mode,
                IsVisible = roomIsVisible,
                IsOpen = roomIsOpen,
                Scene =  sceneRef,
                SessionName = roomName,
                PlayerCount = roomPlayerCount,
                CustomLobbyName = "LobbyID",
                SceneManager = customSceneManager
            };

            var result = await _networkRunnerInstance.StartGame(startGameArgs);
            if (result.Ok)
            {
                _networkRunnerInstance.SetActiveScene(sceneRef);
            }
            else
            {
                Debug.LogError($" Başlatılamadı : {result.ShutdownReason}");
            }
        }


        public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
        {
            OnPlayerJoinedSuccessfully?.Invoke();
        }

        public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
        {
        }

        public void OnInput(NetworkRunner runner, NetworkInput input)
        {
        }

        public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input)
        {
        }
  
        public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason)
        {
         Debug.Log($"OnShutdown {shutdownReason}");
 
            var message = "";
            switch (shutdownReason)
            {
                case ShutdownReason.IncompatibleConfiguration:
                    message = "This room already exist in a different game mode!";
                    break;
                case ShutdownReason.Ok:
                    message = "User terminated network session!";
                    break;
                case ShutdownReason.Error:
                    message = "Unknown network error!";
                    break;
                case ShutdownReason.ServerInRoom:
                    message = "There is already a server/host in this room";
                    break;
                case ShutdownReason.DisconnectedByPluginLogic:
                    message = "The Photon server plugin terminated the network session!";
                    break;
                case ShutdownReason.HostMigration:
                    
                    return;
                default:
                    message = shutdownReason.ToString();
                    break;
            }

        

            if (gameObject)
            {
                Debug.Log("Destroying Game!");
                //Destroy(gameObject);
            }
            SceneManager.LoadScene(0);

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

        public void OnJoinLobby()
        {
            if (!_isJoinLobby)
            {
                var clientTask = JoinLobby(_networkRunnerInstance);
            }
            else
            {
                Debug.Log("Zaten lobby desin");
            }
         
        }
        
        private async Task JoinLobby(NetworkRunner runner)
        {
            Debug.Log("JoinLobby Started");

            const string lobbyId = "LobbyID";
            var result = await runner.JoinSessionLobby(SessionLobby.Custom, lobbyId);
            
            Debug.Log(result);
           
            if (!result.Ok)
            {
                Debug.LogError($"Unable to  join lobby {lobbyId}");
            }
            else
            {
                _isJoinLobby = true;
                Debug.Log("JoinLobby Ok");
            }
        }

        
    }
}