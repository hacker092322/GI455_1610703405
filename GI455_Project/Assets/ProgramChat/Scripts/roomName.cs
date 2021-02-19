using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class roomName : MonoBehaviour
{
    public Text roomText;

    void Start()
    {
        roomText.text = "Room: " + PlayerPrefs.GetString("nameRoom");
    }
}
