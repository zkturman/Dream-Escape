using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cPlayerUI : MonoBehaviour
{
    public TMPro.TextMeshProUGUI checkpointGUI;
    public cCheckpointManager checkpoints;
    // Start is called before the first frame update
    void Start()
    {
        checkpointGUI = GetComponentInChildren<TMPro.TextMeshProUGUI>();
        if (checkpointGUI == null)
        {
            Debug.LogError("Not able to get text mesh for player GUI.");
        }
        checkpoints = FindObjectOfType<cCheckpointManager>();
        if (checkpoints == null)
        {
            Debug.LogError("Not able to get checkpoint object from cCheckpointManager");
        }
    }

    // Update is called once per frame
    void Update()
    {
        checkpointGUI.text = checkpoints.numberLit + " of " + checkpoints.totalLanterns;
    }
}
