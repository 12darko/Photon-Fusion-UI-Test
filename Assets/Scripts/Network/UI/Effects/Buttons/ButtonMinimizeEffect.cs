using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ButtonMinimizeEffect : MonoBehaviour
{
    [SerializeField] private float startScaleValue;
    [SerializeField] private float endScaleValue;
    [SerializeField] private float effectDuration;
    [SerializeField] private Ease effect;
    
    public void ButtonMove()
    {
        gameObject.transform.DOMoveX(startScaleValue, effectDuration).SetEase(effect);
    }

    public void ButtonMoveBack()
    {
        gameObject.transform.DOMoveX(endScaleValue, effectDuration).SetEase(effect);
    }
}
