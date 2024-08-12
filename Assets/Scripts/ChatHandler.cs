using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChatHandler : MonoBehaviour
{
    [SerializeField] private TMPro.TMP_InputField chatPromptInput;
    [SerializeField] private Button sendButton;
    [SerializeField] private Transform chatContent;
    [SerializeField] private GameObjectPool chatMessagePool;
    [SerializeField] private ScrollRect chatScrollRect;

    private void Awake()
    {
        sendButton.onClick.AddListener(OnSendButtonClicked);
        chatPromptInput.onSubmit.AddListener((value) => OnSendButtonClicked());
    }

    private void OnDestroy()
    {
        sendButton.onClick.RemoveListener(OnSendButtonClicked);
        chatPromptInput.onSubmit.RemoveListener((value) => OnSendButtonClicked());
    }

    private void OnSendButtonClicked()
    {
        string message = chatPromptInput.text;
        if (string.IsNullOrEmpty(message))
            return;
        
        Debug.Log($"Message: {message}");
        chatPromptInput.text = string.Empty;
        AddMessageToChat(message);
    }
    
    public void AddMessageToChat(string message, bool isInput = true)
    {
        // Add message to chat
        PooledObject pooledObject = chatMessagePool.objectPool.Get();
        MessageOutput messageOutput = pooledObject.GetComponent<MessageOutput>();
        messageOutput.SetMessage(message, isInput);
        pooledObject.transform.SetParent(chatContent, false);
        
        StartCoroutine(UpdateAfterFrame());
    }
    
    private IEnumerator UpdateAfterFrame()
    {
        yield return new WaitForSeconds(0.1f);
        ScrollToBottom();
        chatContent.gameObject.SetActive(false);
        chatContent.gameObject.SetActive(true);
    }
    
    private void ScrollToBottom()
    {
        // chatScrollRect.verticalNormalizedPosition = 0;
        chatScrollRect.normalizedPosition = new Vector2(0, 0);
    }
}
