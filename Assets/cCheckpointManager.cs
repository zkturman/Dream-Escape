using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cCheckpointManager : MonoBehaviour
{
    public cCheckpointBeacon[] lanterns;
    public int numberLit;
    public int totalLanterns;
    // Start is called before the first frame update
    void Start()
    {
        lanterns = FindObjectsOfType<cCheckpointBeacon>();
        if (lanterns != null)
        {
            totalLanterns = lanterns.Length;
        }
        numberLit = 0;
    }

    // Update is called once per frame
    void Update()
    {
        int count = 0;
        foreach (cCheckpointBeacon x in lanterns)
        {
            if (x.lightOn)
            {
                count++;
            }
        }
        numberLit = count;
    }
}
