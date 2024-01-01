using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LobbyMessages : MonoBehaviour
{
    [SerializeField] private TMP_Text chatTextContent;
    [SerializeField] private TMP_InputField chatTextInput;

    public TMP_InputField ChatTextInput => chatTextInput;


    public void OnLobbyMessageReceived(string message)
    {
        Debug.Log($"LobbyMessages {message}");

        var messageInReceived = message;
        
        chatTextContent.text += messageInReceived;
    }

   
    
}