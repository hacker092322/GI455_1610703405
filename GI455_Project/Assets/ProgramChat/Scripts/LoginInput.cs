using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginInput : MonoBehaviour
{
    public InputField nameInput, ipInput, protocolsInput;
    public Button loginButton;
    
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
    }
}
