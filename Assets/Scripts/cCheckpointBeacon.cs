using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cCheckpointBeacon : cSignalBeacon
{
    public GameObject lightObject;
    public Light spotlight;

    public override void igniteLight()
    {
        if (spotlight != null)
        {
            spotlight.enabled = true;
            lightOn = true;
            GetComponentInParent<AudioSource>().Play();
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        spotlight = lightObject.GetComponent<Light>();
        if (spotlight != null)
        {
            spotlight.enabled = false;
        }
    }
}
