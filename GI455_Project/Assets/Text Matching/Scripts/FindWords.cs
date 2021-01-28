using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FindWords : MonoBehaviour
{
    public Text inputWord, answerText;

    string checkWord;
    bool isCorrect = false;

    public void FindButton()
    {
        isCorrect = false;
        checkWord = inputWord.GetComponent<Text>().text;

        for (int i = 0; i < SetupWords.words.Count; i++)
        {
            if (checkWord == SetupWords.words[i])
            {
                isCorrect = true;
                break;
            }
        }

        if (isCorrect == true)
        {
            answerText.text = "[ <color=green>" + checkWord + "</color> ] is found.";
        }

        else
        {
            answerText.text = "[ <color=red>" + checkWord + "</color> ] is not found.";
        }
    }
}
