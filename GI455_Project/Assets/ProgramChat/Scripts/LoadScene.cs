using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using WebSocketSharp;

public class LoadScene : MonoBehaviour
{
    public GameObject loginUI, roomUI, errorTextUI;
    public Text nameInput, ipInput, protocolsInput, createRoomInput, joinRoomInput,errorText;

    private string nameInputText, ipInputText, protocolsInputText;
    public static string createOrJoin, createRoomInputText, joinRoomInputText;
    public static WebSocket socket;

    public void ToCreateOrJoinRoom()
    {
        loginUI.SetActive(false);
        roomUI.SetActive(true);
        ipInputText = ipInput.GetComponent<Text>().text;
        protocolsInputText = protocolsInput.GetComponent<Text>().text;
        print("ws://" + ipInputText + ":" + protocolsInputText + "/");
        socket = new WebSocket("ws://" + ipInputText + ":" + protocolsInputText + "/");
        socket.Connect();
    }

    public void BackLogin()
    {
        loginUI.SetActive(true);
        roomUI.SetActive(false);
    }

    public void SendCreateRoom()
    {
        createOrJoin = "Create";
        ConnectSocketSetUp();
        string createRoom = createOrJoin + ";" + createRoomInputText + ";" + nameInputText;
        print(createRoom);
        socket.Send(createRoom);
    }

    public void SendJoinRoom()
    {
        createOrJoin = "Join";
        ConnectSocketSetUp();
        string joinRoom = createOrJoin + ";" + joinRoomInputText + ";" + nameInputText;
        print(joinRoom);
        socket.Send(joinRoom);
    }

    public void ConnectSocketSetUp()
    {
        nameInputText = nameInput.GetComponent<Text>().text;
        createRoomInputText = createRoomInput.GetComponent<Text>().text;
        joinRoomInputText = joinRoomInput.GetComponent<Text>().text;
        GetComponent<checkConnect>().enabled = true;
    }

    public void LoadSceneChatRoom()
    {
        //nameText = nameInput.GetComponent<Text>().text;
        //ipText = ipInput.GetComponent<Text>().text;
        //protocolsText = protocolsInput.GetComponent<Text>().text;

        // Test Input Ip, Protocols and Socket
        //Debug.Log(ipText.ToString() + protocolsText.ToString());
        //socket = new WebSocket("ws://192.168.1.218:25500/");

        //socket = new WebSocket("ws://" + ipText + ":" + protocolsText + "/");
        //socket.Connect();
        //socket.Send(nameText);
        //SceneManager.LoadScene("ChatRoom");
    }
}
