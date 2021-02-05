using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using WebSocketSharp;

public class LoadScene : MonoBehaviour
{
    public Text nameInput, ipInput, protocolsInput;

    private string nameText, ipText, protocolsText;
    public static WebSocket socket;

    public void LoadSceneChatRoom()
    {
        nameText = nameInput.GetComponent<Text>().text;
        ipText = ipInput.GetComponent<Text>().text;
        protocolsText = protocolsInput.GetComponent<Text>().text;

        // Test Input Ip, Protocols and Socket
        //Debug.Log(ipText.ToString() + protocolsText.ToString());
        //socket = new WebSocket("ws://192.168.1.218:25500/");

        socket = new WebSocket("ws://" + ipText + ":" + protocolsText + "/");
        socket.Connect();
        socket.Send(nameText);
        SceneManager.LoadScene("ChatRoom");
    }
}
