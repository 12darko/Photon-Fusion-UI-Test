using UnityEngine;
using UnityEngine.UI;

namespace Network.Room
{
    public class RoomIsVisible : RoomManager
    {
        public void RoomVisibleControl()
        {
            var uiData = UIData.Instance;
            if (uiData.RoomIsVisibleCheckMarksBtn.GetComponent<Toggle>().isOn)
            {
                RoomData.RoomIsVisible = uiData.RoomIsVisibleCheckMarksBtn.GetComponent<Toggle>().isOn;
            }
            else
            {
                RoomData.RoomIsVisible = uiData.RoomIsVisibleCheckMarksBtn.GetComponent<Toggle>().isOn;
            }
        }
    }
}