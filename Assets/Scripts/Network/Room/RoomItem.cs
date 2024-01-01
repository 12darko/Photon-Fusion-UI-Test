using System;
using Fusion;
using Fusion.Editor;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Network.Room
{
    public class RoomItem : MonoBehaviour
    {
        public TMP_Text roomNameTxt;
        public TMP_Text roomNameTitle;
        public TMP_Text roomPlayerCountText;
        public Button roomJoinBtn;

        private SessionInfo sessionInfo;

        public event Action<SessionInfo> OnJoinSession;
        

        

        public void SetInformation(SessionInfo sessionInfo)
        {
            this.sessionInfo = sessionInfo;

            roomNameTxt.text = sessionInfo.Name;
            roomNameTitle.text = sessionInfo.Region;
            roomPlayerCountText.text = sessionInfo.PlayerCount.ToString() + " / " + sessionInfo.MaxPlayers.ToString();

            var isJoinButtonActive = true;

            if (sessionInfo.PlayerCount >= sessionInfo.MaxPlayers)
                isJoinButtonActive = false;
            
            roomJoinBtn.gameObject.SetActive(isJoinButtonActive);
        }

        public void OnClickJoinRoom()
        {           
          
            OnJoinSession?.Invoke(sessionInfo);
        }
    }
}