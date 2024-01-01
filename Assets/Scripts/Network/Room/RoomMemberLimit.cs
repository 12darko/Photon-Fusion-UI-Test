using TMPro;
using Unity.VisualScripting;
using UnityEngine;

namespace Network.Room
{
    public class RoomMemberLimit : RoomManager
    {
        private int playerCount = 2;

        private void FixedUpdate()
        {
            if (RoomData.RoomMaxPlayerCount == 2)
            {
                RoomData.RoomMaxPlayerCount = byte.Parse(UIData.Instance.RoomMemberLimit.GetComponent<TMP_Dropdown>()
                    .options[playerCount].text);
            }
        }

        public void MemberLimit(int playerCount)
        {
            this.playerCount = playerCount;

            RoomData.RoomMaxPlayerCount =
                byte.Parse(UIData.Instance.RoomMemberLimit.GetComponent<TMP_Dropdown>().options[playerCount].text);
        }
    }
}