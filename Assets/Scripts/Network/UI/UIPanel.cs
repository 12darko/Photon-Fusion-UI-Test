using System;
using Unity.VisualScripting;
using UnityEngine;

namespace Network.UI
{
    public class UIPanel : MonoBehaviour
    {
        public GameObject currentPanel;
        public GameObject nextPanel;
       
        
        public void ChangePanel(UIPanel panel)
        { 
            panel.currentPanel.SetActive(false);
            panel.nextPanel.SetActive(true);
        }
    }
}