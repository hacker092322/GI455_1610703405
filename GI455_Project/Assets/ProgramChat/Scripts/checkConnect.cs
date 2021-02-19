using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using WebSocketSharp;

public class checkConnect : MonoBehaviour
{
    public GameObject errorTextUI;
    public Text errorText;


    bool sendText;
    string sendData;

    void Start()
    {
        LoadScene.socket.OnMessage += OnMessage;
    }

    
    void Update()
    {
       sendDataCheck();
    }

    public void OnMessage(object sender, MessageEventArgs messageEventArgs)
    {
        sendData = messageEventArgs.Data;
        sendText = true;
    }

    void sendDataCheck()
    {
        if (sendText == true)
        {
            print(sendData);
            sendText = false;
            if (sendData == "connect")
            {
                if (LoadScene.createOrJoin == "Create")
                {
                    PlayerPrefs.SetString("nameRoom", LoadScene.createRoomInputText);
                }
                else if(LoadScene.createOrJoin == "Join")
                {
                    PlayerPrefs.SetString("nameRoom", LoadScene.joinRoomInputText);
                }

                SceneManager.LoadScene("ChatRoom");
            }

            else if (sendData == "haveSameRoomName")
            {
                errorTextUI.SetActive(true);
                errorText.text = "Your room name have already create";
                StartCoroutine(WaitTextPopup());
            }

            else if (sendData == "don'tFountRoom")
            {
                errorTextUI.SetActive(true);
                errorText.text = "Your input name is't found";
                StartCoroutine(WaitTextPopup());
            }
        }
    }

    IEnumerator WaitTextPopup()
    {
        yield return new WaitForSeconds(5);
        errorTextUI.SetActive(false);

    }
}
