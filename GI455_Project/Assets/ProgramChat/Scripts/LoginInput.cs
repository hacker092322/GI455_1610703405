using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginInput : MonoBehaviour
{
    public InputField nameInput, ipInput, protocolsInput, createRoomInput, joinRoomInput;
    public Button loginButton, createRoomButton, joinRoomButton;
    
    void Update()
    {
        if (string.IsNullOrEmpty(nameInput.text) || string.IsNullOrEmpty(ipInput.text) 
            || string.IsNullOrEmpty(protocolsInput.text))
        {
            loginButton.GetComponent<Image>().color = new Color32(152, 144, 144, 132);
            loginButton.GetComponent<Button>().interactable = false;
        }

        else
        {
            loginButton.GetComponent<Image>().color = Color.white;
            loginButton.GetComponent<Button>().interactable = true;
        }

        if (string.IsNullOrEmpty(createRoomInput.text))
        {
            createRoomButton.GetComponent<Image>().color = new Color32(152, 144, 144, 132);
            createRoomButton.GetComponent<Button>().interactable = false;
        }

        else
        {
            createRoomButton.GetComponent<Image>().color = Color.white;
            createRoomButton.GetComponent<Button>().interactable = true;
        }

        if (string.IsNullOrEmpty(joinRoomInput.text))
        {
            joinRoomButton.GetComponent<Image>().color = new Color32(152, 144, 144, 132);
            joinRoomButton.GetComponent<Button>().interactable = false;
        }

        else
        {
            joinRoomButton.GetComponent<Image>().color = Color.white;
            joinRoomButton.GetComponent<Button>().interactable = true;
        }
    }
}
