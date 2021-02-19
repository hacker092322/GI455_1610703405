using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using WebSocketSharp;

public class TextFromSever : MonoBehaviour
{
    public GameObject createText;
    public Transform chatTransform;

    string severText, senderName;
    int typeText;
    bool sendText;

    // typeText = 0 = text form me
    // typeText = 1 = text form other
    // typeText = 2 = connect text
    // typeText = 3 = disconnect text

    //List<string> data = new List<string>();

    string sendData;
    string[] splitData;

    void Start()
    {
        LoadScene.socket.OnMessage += OnMessage;
    }

    void Update()
    {
        CreateText();
    }

    public void OnMessage(object sender, MessageEventArgs messageEventArgs)
    {
        sendData = messageEventArgs.Data;
        print(sendData);
        splitData = sendData.Split(';');
        print(splitData);
        print(splitData[0]);

        /*data.Add(messageEventArgs.Data);
        for (int i = 0; i < data.Count; i++)
        {
            if (i == 0)
            {
                switch (data[i])
                {
                    case "1":
                        typeText = 1;
                        break;
                    case "0":
                        typeText = 0;
                        break;
                    case "2":
                        typeText = 2;
                        break;
                    case "3":
                        typeText = 3;
                        break;
                }
            }

            else if (i == 1)
            {
                severText = data[i];
            }

            else if (i == 2)
            {
                senderName = data[i];
            }
        }   */
        sendText = true;
    }

    void CreateText()
    {
        if(sendText == true)
        {
            foreach (GameObject textInScene in GameObject.FindGameObjectsWithTag("ChatTexts"))
                {
                    textInScene.transform.Translate(0, 20, 0);
                }

            if (splitData[0] == 0.ToString())
            {
                createText.GetComponent<Text>().text = splitData[1];
                createText.GetComponent<Text>().alignment = TextAnchor.LowerRight;
            }

            else if (splitData[0] == 1.ToString())
            {
                createText.GetComponent<Text>().text = "<" + splitData[1] + "> " + splitData[2];
                createText.GetComponent<Text>().alignment = TextAnchor.LowerLeft;
            }

            else if (splitData[0] == 2.ToString())
            {
                createText.GetComponent<Text>().text = splitData[1] + " is connecting";
                createText.GetComponent<Text>().alignment = TextAnchor.LowerCenter;
                createText.GetComponent<Text>().color = Color.green;
            }

            else if (splitData[0] == 3.ToString())
            {
                createText.GetComponent<Text>().text = splitData[1] + " is disconnecting";
                createText.GetComponent<Text>().alignment = TextAnchor.LowerCenter;
                createText.GetComponent<Text>().color = Color.red;
            }

            /*if (typeText == 1)
            {
                createText.GetComponent<Text>().text = "<"+ senderName + "> " + severText;
                createText.GetComponent<Text>().alignment = TextAnchor.LowerLeft;
            }

            else if (typeText == 0)
            {
                createText.GetComponent<Text>().text = severText;
                createText.GetComponent<Text>().alignment = TextAnchor.LowerRight;
            }

            else if (typeText == 2)
            {
                createText.GetComponent<Text>().text = severText + " is connecting";
                createText.GetComponent<Text>().alignment = TextAnchor.LowerCenter;
                createText.GetComponent<Text>().color = Color.green;
            }

            else if (typeText == 3)
            {
                createText.GetComponent<Text>().text = severText + " is disconnecting";
                createText.GetComponent<Text>().alignment = TextAnchor.LowerCenter;
                createText.GetComponent<Text>().color = Color.red;
            }
            */

            GameObject chatText = Instantiate<GameObject>(createText, new Vector3(-1.049e-05f, -275.89f, 0), Quaternion.identity);
            chatText.transform.SetParent(chatTransform, false);
            createText.GetComponent<Text>().color = Color.black;
            sendText = false;
        }
    }
}
