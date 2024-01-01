using UnityEngine;
using UnityEngine.UI;

namespace Network.Room
{
    public class RoomIsOpen : RoomManager
    {
        public void RoomOpenControl()
        {
            var uiData = UIData.Instance;


            if (uiData.RoomIsOpenCheckMarksBtn.GetComponent<Toggle>().isOn)
            {
                RoomData.RoomIsOpen = uiData.RoomIsOpenCheckMarksBtn.GetComponent<Toggle>().isOn;
            }
            else
            {
                RoomData.RoomIsOpen = uiData.RoomIsOpenCheckMarksBtn.GetComponent<Toggle>().isOn;
            }
        }
    }
}