using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    [SerializeField] private string menuName;

    [SerializeField]  private bool isOpen;
    public string MenuName => menuName;

    public bool IsOpen
    {
        get => isOpen;
        set => isOpen = value;
    }

    public void Open()
    {
        isOpen = true;
        gameObject.SetActive(true);
    }

    public void Close()
    {
        isOpen = false;
        gameObject.SetActive(false);
    }
}