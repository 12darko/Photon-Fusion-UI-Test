using System;
using DG.Tweening;
using UnityEngine;

namespace Network.UI
{
    public class UIMainMenu : MonoBehaviour
    {
        private void OnEnable()
        {
            MenuElementsEffect();
        }
 
 
        private void MenuElementsEffect()
        {
            var menuBackground = UIData.Instance.MainPanel.transform.GetChild(0).GetChild(0).GetChild(2)
                .GetComponent<RectTransform>();
            var menuHeader = UIData.Instance.MainPanel.transform.GetChild(0).GetChild(1).GetChild(0)
                .GetComponent<RectTransform>();

            menuBackground.DOLocalMoveX(0, 0.2f).SetEase(Ease.Linear);
            menuHeader.DOLocalMoveY(0, 0.2f).SetEase(Ease.Linear);
        }
 
    }
}