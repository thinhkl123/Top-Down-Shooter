using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameOverMenu : MonoBehaviour
{
    public void EndGame()
    {
        if (AudioController.Ins)
        {
            AudioController.Ins.PlayPressSound();
        }
        Application.Quit();
    }

    public void PlayAgainGame()
    {
        if (AudioController.Ins)
        {
            AudioController.Ins.PlayPressSound();
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
