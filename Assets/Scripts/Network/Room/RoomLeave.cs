using System;
using Fusion;
using Network.NetworkPlayer;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Network.Room
{
    public class RoomLeave : NetworkBehaviour
    {
        public void LeaveRoom()
        {
            Runner.Shutdown(false);
        }
    }
}