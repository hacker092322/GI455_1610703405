using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using WebSocketSharp;

public class LoadLoginScene : MonoBehaviour
{

    public void LoadSceneLoginChat()
    {
        LoadScene.socket.Close();
        SceneManager.LoadScene("LoginChat");
    }

    public void OnDestroy()
    {
        if (LoadScene.socket != null)
        {
            LoadScene.socket.Close();
        }
    }
}
