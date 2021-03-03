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
            base.igniteLight();
            spotlight.enabled = true;
            GetComponentInParent<AudioSource>().Play();
        }
    }

    public override void extinguishLight()
    {
        Debug.Log("extinguishLight was called");
        if(spotlight != null)
        {
            base.extinguishLight();
            spotlight.enabled = false;
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
