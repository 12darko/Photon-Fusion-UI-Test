using DG.Tweening;
using EMA.Scripts.PatternClasses;
using UnityEngine;

public class UIManager : MonoSingleton<UIManager>
{
    public void OpenFriendList()
    {
        var friendList = UIData.Instance.FriendListObject.GetComponent<RectTransform>();
        friendList.DOLocalMoveX(0, 0.5f).SetEase(Ease.Linear);
    }

    public void CloseFriendList()
    {
        var friendList = UIData.Instance.FriendListObject.GetComponent<RectTransform>();
        friendList.DOLocalMoveX(600, 0.5f).SetEase(Ease.Linear);
    }
    public void OpenRoomList()
    {
        var uiData = UIData.Instance;
        uiData.MainPanel.SetActive(false);
        uiData.LobbyPanel.SetActive(true);
    }
 
}