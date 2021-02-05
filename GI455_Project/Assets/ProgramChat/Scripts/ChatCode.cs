using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using WebSocketSharp;

public class ChatCode : MonoBehaviour
{
    public GameObject createText;
    public InputField chatInputField;
    public Text chatTextInput;
    public Button chatButton;
    public Transform chatTransform;

    void Update()
    {
        if (string.IsNullOrEmpty(chatInputField.text))
        {
            chatButton.GetComponent<Image>().color = new Color32(152, 144, 144, 132);
            chatButton.GetComponent<Button>().interactable = false;
        }

        else
        {
            chatButton.GetComponent<Image>().color = Color.white;
            chatButton.GetComponent<Button>().interactable = true;
        }
    }

    public void Chating()
    {
        LoadScene.socket.Send(chatTextInput.GetComponent<Text>().text);
        chatInputField.GetComponent<InputField>().text = string.Empty;

    }
}
