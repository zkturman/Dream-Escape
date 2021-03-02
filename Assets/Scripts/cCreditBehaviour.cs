using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cCreditBehaviour : MonoBehaviour
{
    public cLevelManager changeLevel;
    // Update is called once per frame
    void Update()
    {
        if(Input.anyKey == true)
        {
            changeLevel.LoadNextLevel(cLevelManager.specialLevel.start);
        }
    }


}
