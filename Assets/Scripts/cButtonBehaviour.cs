using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class cButtonBehaviour : MonoBehaviour
{
    public cLevelManager changeLevel;

    public void gameStart()
    {
        Debug.Log("Load next level");
        changeLevel.LoadNextLevel(cLevelManager.specialLevel.next);
    }

    public void gameTutorial()
    {
        Debug.Log("Load the tutorial");
        changeLevel.LoadNextLevel(cLevelManager.specialLevel.tutorial);
    }

    public void gameSettings()
    {
        Debug.Log("Load the settings menu");
    }

    public void gameContinue()
    {
        Debug.Log("Re-load level 1");
        changeLevel.LoadNextLevel(cLevelManager.specialLevel.restart);
    }

    public void gameQuit()
    {
        Debug.Log("Return to main menu");
        Debug.Log("The cLeverManager " + changeLevel);
        changeLevel.LoadNextLevel(cLevelManager.specialLevel.start);
    }

    public void gameExit()
    {
        Debug.Log("Exit the game");
        Application.Quit();
    }
}
