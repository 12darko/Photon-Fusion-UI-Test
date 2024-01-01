using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyStarter : MonoBehaviour
{
    [SerializeField] private GameObject sceneCanvas;

    private void Start()
    {
        Instantiate(sceneCanvas, null);
    }
}
