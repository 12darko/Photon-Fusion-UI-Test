using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using DG.Tweening;

public class BıuttonHoverEffect : MonoBehaviour
{
    [SerializeField] private float startScaleValue;
    [SerializeField] private float endScaleValue;
    [SerializeField] private float effectDuration;
    [SerializeField] private Ease effect;
    
   public void ButtonHover()
    {
        gameObject.transform.DOScale(startScaleValue, effectDuration).SetEase(effect);
    }

   public void ButtonExit()
    {
        gameObject.transform.DOScale(endScaleValue, effectDuration).SetEase(effect);
    }
}
