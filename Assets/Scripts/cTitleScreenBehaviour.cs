using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cTitleScreenBehaviour : MonoBehaviour
{
    public cLevelManager changeLevel;
    // Update is called once per frame
    void Update()
    {
        if(Input.anyKey == true)
        {
            changeLevel.LoadNextLevel(cLevelManager.specialLevel.next);
        }
    }


}
