using System.Collections;
using System.Collections.Generic;
using EMA.Scripts.PatternClasses;
using Fusion;
using UnityEngine;

public class PlayerData : NetworkBehaviour
{
    [SerializeField] private bool inRoom = false;
    [SerializeField] private bool isLeader = false;

    [SerializeField] private string playerName;
    [SerializeField] private string playerEmail;
    [SerializeField] private int playerLevel;


    #region Read Only Prop

    public bool InRoom => inRoom;
    public bool IsLeader => isLeader;

    public string PlayerName => playerName;

    public string PlayerEmail => playerEmail;

    public int PlayerLevel => playerLevel;

    #endregion



    public bool SetLeader(bool val)
    {
        return isLeader = val;
    }
    public bool SetRoom(bool val)
    {
        return inRoom = val;
    }
    public string SetPlayerName(string val)
    {
        return playerName = val;
    }
    public string SetPlayerEmail(string val)
    {
        return playerEmail = val;
    }
    public int SetPlayerLevel(int val)
    {
        return playerLevel = val;
    }

}