using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace Network.Room
{
    public class RoomSettings : RoomManager
    {
        private void FixedUpdate()
        {
            var uiData = UIData.Instance;
            if (RoomData == null) return;
            RoomData.RoomIsOpen = uiData.RoomIsOpenCheckMarksBtn.GetComponent<Toggle>().isOn;
            RoomData.RoomIsVisible = uiData.RoomIsVisibleCheckMarksBtn.GetComponent<Toggle>().isOn;
        }
    }
}