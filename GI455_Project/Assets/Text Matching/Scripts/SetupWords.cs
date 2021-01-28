using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetupWords : MonoBehaviour
{
    public static List<string> words = new List<string>();

    string wordInTag;

    void Start()
    {
        
        foreach (GameObject textTag in GameObject.FindGameObjectsWithTag("SearchWords"))
        {
            wordInTag = textTag.GetComponent<Text>().text;
            words.Add(wordInTag);
        }

        //Test Add List and print
        //for (int i = 0; i < words.Count; i++)
        //{
        //    print(words[i]);
        //}
        
    }
}
