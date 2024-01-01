using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;


public class BackgroundChangeEffect : MonoBehaviour
{

    [SerializeField] private CanvasGroup _canvasGroup;
    private void Start()
    {
        ChangeBackgroundStart();
    }

    private void ChangeBackgroundStart()
    {
        _canvasGroup.DOFade(0.3f, 4f).SetEase(Ease.Flash).OnComplete(ChangeBackgroundEnd);
    }
    
    private void ChangeBackgroundEnd()
    {
        _canvasGroup.DOFade(1f, 4f).SetEase(Ease.Flash).OnComplete(ChangeBackgroundStart);
    }
}
