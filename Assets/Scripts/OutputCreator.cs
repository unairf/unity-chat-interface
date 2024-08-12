using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OutputCreator : MonoBehaviour
{
    [SerializeField] private Button sendButton;
    [SerializeField] private TMPro.TMP_InputField inputField;
    [SerializeField] private ChatHandler chatHandler;

    private void Start()
    {
        sendButton.onClick.AddListener(OnSendButtonClicked);
        inputField.onSubmit.AddListener((value) => OnSendButtonClicked());
    }

    private void OnDestroy()
    {
        sendButton.onClick.RemoveListener(OnSendButtonClicked);
        inputField.onSubmit.RemoveListener((value) => OnSendButtonClicked());
    }

    private void OnSendButtonClicked()
    {
        string message = inputField.text;
        if (string.IsNullOrEmpty(message))
            return;
        
        Debug.Log($"Message: {message}");
        inputField.text = string.Empty;
        chatHandler.AddMessageToChat(message, false);
    }
}
