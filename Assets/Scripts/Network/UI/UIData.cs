using System.Collections;
using System.Collections.Generic;
using EMA.Scripts.PatternClasses;
using Network;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIData : MonoSingleton<UIData>
{
    [Header("Main Panel Comp")] [SerializeField]
    private GameObject mainPanel;

    [Header("Login Panel")] [SerializeField]
    private GameObject loginPanel;
 
 
    [Header("Lobby Panel Comp")] [SerializeField]
    private GameObject lobbyPanel;
    

    [SerializeField] private Button joinRandomRoomBtn;
    [SerializeField] private Button createRoomBtn;
    [SerializeField] private Button roomListBtn;
    [SerializeField] private GameObject roomInputFields;
    [SerializeField] private GameObject contentObject;
   // [SerializeField] private RoomItem roomItemPrefab;

    [Header("Room Panel Comp")] [SerializeField]
    private GameObject roomPanel;

   // [SerializeField] private RoomUserControlItem roomUserControlItem;
    [SerializeField] private TMP_InputField roomName;
    [SerializeField] private GameObject playerContentPrefab;
  //  [SerializeField] private PlayerItem playerItemPrefab;

    [SerializeField] private GameObject gameStartButton;
    //Friend List
    [SerializeField] private GameObject friendListObject;

    [Header("Room Settings Panel Comp")] [SerializeField]
    private GameObject roomSettingsPanel;

    [SerializeField] private GameObject roomIsVisibleCheckMarksBtn;
    [SerializeField] private GameObject roomIsOpenCheckMarksBtn;
    [SerializeField] private GameObject roomMemberLimit;
    [SerializeField] private GameObject roomMemberCountText;
    

    [SerializeField] private Color[] playerColorList;

    #region Props

    public GameObject LoginPanel
    {
        get => loginPanel;
        set => loginPanel = value;
    }
 

    public Color[] PlayerColorList
    {
        get => playerColorList;
        set => playerColorList = value;
    }

    public Button JoinRandomRoomBtn
    {
        get => joinRandomRoomBtn;
        set => joinRandomRoomBtn = value;
    }

    public Button CreateRoomBtn
    {
        get => createRoomBtn;
        set => createRoomBtn = value;
    }

    public Button RoomListBtn
    {
        get => roomListBtn;
        set => roomListBtn = value;
    }

    #endregion
   


    //Main Panel Comp
    public GameObject MainPanel => mainPanel;

    //Lobby Panel Comp
    public GameObject LobbyPanel => lobbyPanel;
    public GameObject RoomInputFields => roomInputFields;
    public GameObject ContentObject => contentObject;
   // public RoomItem RoomItemPrefab => roomItemPrefab;

    //Room Panel Comp
    public GameObject RoomPanel => roomPanel;
  //  public RoomUserControlItem RoomUserControlItem => roomUserControlItem;
    public TMP_InputField RoomName => roomName;
    public GameObject PlayerContentPrefab => playerContentPrefab;
   // public PlayerItem PlayerItemPrefab => playerItemPrefab;

    public GameObject FriendListObject
    {
        get => friendListObject;
        set => friendListObject = value;
    }
    

    public GameObject GameStartButton
    {
        get => gameStartButton;
        set => gameStartButton = value;
    }


    //Room Settings Comp
    public GameObject RoomSettingsPanel => roomSettingsPanel;
    public GameObject RoomIsVisibleCheckMarksBtn => roomIsVisibleCheckMarksBtn;
    public GameObject RoomIsOpenCheckMarksBtn => roomIsOpenCheckMarksBtn;
    public GameObject RoomMemberLimit => roomMemberLimit;
    public GameObject RoomMemberCountText => roomMemberCountText;
}