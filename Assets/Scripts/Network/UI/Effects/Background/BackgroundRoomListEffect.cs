using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class BackgroundRoomListEffect : MonoBehaviour
{
    private void OnEnable()
    {
        if(!UIData.Instance.LobbyPanel.activeSelf)
        { 
            var roomListBack = UIData.Instance.LobbyPanel.transform.GetChild(0).GetChild(3)
                .GetComponent<RectTransform>();
            roomListBack.DOLocalMoveX(900, 0.5f).SetEase(Ease.Linear);
        }
        else
        {
            var roomListBack = UIData.Instance.LobbyPanel.transform.GetChild(0).GetChild(3)
                .GetComponent<RectTransform>();
            roomListBack.DOLocalMoveX(0, 0.5f).SetEase(Ease.Linear);
        }
    
    }

    private void OnDisable()
    {    
        if(UIData.Instance.LobbyPanel.activeSelf)
        { 
            
            var roomListBack = UIData.Instance.LobbyPanel.transform.GetChild(0).GetChild(3)
                .GetComponent<RectTransform>();
            roomListBack.DOLocalMoveX(0, 0.5f).SetEase(Ease.Linear);
           
        }
        else
        {
            var roomListBack = UIData.Instance.LobbyPanel.transform.GetChild(0).GetChild(3)
                .GetComponent<RectTransform>();
            roomListBack.DOLocalMoveX(900, 0.5f).SetEase(Ease.Linear);
        }
       
    }
}