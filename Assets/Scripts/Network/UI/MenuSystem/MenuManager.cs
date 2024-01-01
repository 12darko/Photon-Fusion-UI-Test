using System.Collections;
using System.Collections.Generic;
using EMA.Scripts.PatternClasses;
using UnityEngine;

public class MenuManager : MonoSingleton<MenuManager>
{
    [SerializeField] private Menu[] menus;


    public void OpenMenu(string menuName)
    {
        foreach (var menuVariables in menus)
        {
            if (menuVariables.MenuName == menuName)
            {
                OpenMenu(menuVariables);
            }
            else if (menuVariables.IsOpen)
            {
                CloseMenu(menuVariables);
            }
        }
    }

    private void OpenMenu(Menu menu)
    {
        foreach (var menuVariables in menus)
        {
            if (menuVariables.IsOpen)
            {
                CloseMenu(menuVariables);
            }
        }

        menu.Open();
    }

    public void CloseMenu(Menu menu)
    {
        menu.Close();
    }
}