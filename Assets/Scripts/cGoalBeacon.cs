using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cGoalBeacon : cSignalBeacon
{
    public cCheckpointBeacon[] checkpoints;
    public GameObject[] goalLanterns;
    public Light[] goalLights;

    public bool allCheckpointsClear()
    {
        checkpoints = FindObjectsOfType<cCheckpointBeacon>();
        foreach (cCheckpointBeacon check in checkpoints)
        {
            if (check.lightOn == false)
            {
                return false;
            }
        }
        return true;
    }

    public override void igniteLight()
    {
        if (goalLights != null)
        {
            foreach (Light light in goalLights)
            {
                light.enabled = true;
            }
            lightOn = true;
            GetComponentInParent<AudioSource>().Play();
        }
    }

        // Start is called before the first frame update
        void Start()
    {
        int i;
        goalLights = new Light[goalLanterns.Length];
        for (i = 0; i < goalLanterns.Length; i++)
        {
            goalLights[i] = goalLanterns[i].GetComponent<Light>();
            if (goalLights[i] != null)
            {
                goalLights[i].enabled = false;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        shouldIgnite = allCheckpointsClear();
        if (lightOn == false && shouldIgnite == true)
        {
            igniteLight();
        }
    }
}
