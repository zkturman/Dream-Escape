 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class cLevelManager : MonoBehaviour
{
    public enum specialLevel
    {
        next,
        start,
        restart,
        gameover,
        credits,
        tutorial
    }

    public float transitionTime = 1f;
    // Update is called once per frame
    void Update()
    {

    }

    public void LoadNextLevel(specialLevel check)
    {
        int nextSceneIndex = 0;
        string nextSceneName = null;
        Debug.Log("This is the check value " + check);
        if (check == specialLevel.next)
        {
            nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
            StartCoroutine(LoadLevelByIndex(nextSceneIndex));
        }
        else
        {
            if (check == specialLevel.start)
            {
                nextSceneName = "Title Screen";

            }
            if (check == specialLevel.restart)
            {
                nextSceneName = "Level 1";
            }
            if (check == specialLevel.gameover)
            {
                nextSceneName = "Game Over";
            }
            if (check == specialLevel.tutorial)
            {
                nextSceneName = "Tutorial";
            }
            StartCoroutine(LoadLevelByName(nextSceneName));
        }


    }

    IEnumerator LoadLevelByIndex(int LevelIndex)
    {
        //add transition information here

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(LevelIndex);
    }

    IEnumerator LoadLevelByName(string sceneName)
    {

        //add transition information here

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(sceneName);
    }
}

