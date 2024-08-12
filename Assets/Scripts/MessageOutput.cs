using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageOutput : MonoBehaviour
{
    [SerializeField] private TMPro.TMP_InputField text;
    [SerializeField] private TMPro.TextMeshProUGUI textToEnableWrapping;
    [SerializeField] private Image background;
    [SerializeField] private Color inputBackgroundColor, outputBackgroundColor;

    public void SetMessage(string message, bool isInput = false)
    {
        text.text = message;
        textToEnableWrapping.enableWordWrapping = true;
        background.color = isInput ? inputBackgroundColor : outputBackgroundColor;
    }
}
