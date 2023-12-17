using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIManager : Singleton<GUIManager>
{
    public Text scoreText;
    public Text scoreTextUI;
    int score = 0;

    public override void Awake()
    {
        MakeSingleton(false);
    }

    public void UpdateScore()
    {
        score++;
        scoreText.text = "Score: " + score;
        scoreTextUI.text = scoreText.text;
        //Debug.Log(score);
    }
}
