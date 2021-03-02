using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameSceneCo : MonoBehaviour
{
    public bool homeButtonClick = false;
    public void RestartButtonClick()
    {
        SceneManager.LoadScene(1);
    }

    public void HomeButtonClick()
    {
        SceneManager.LoadScene(0);
    }

    public void ContinueButtonClcik()
    {
        if (homeButtonClick)
        {
            SceneManager.LoadScene(0);
        } else
        {
            SceneManager.LoadScene(1);
        }
    }
}
