using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cFaerieBehaviour : cPixieBehaviour
{
    protected override cSignalBeacon closestLantern(bool lightOn)
    {
        return base.closestLantern(!lightOn);
    }

    private void OnTriggerEnter(Collider other)
    {
        cWarningBeacon lantern = null;
        if (other.gameObject != null)
        {
            lantern = other.GetComponentInChildren<cWarningBeacon>();
        }
        Debug.Log(lantern);
        if (lantern != null && !lantern.lightOn)
        {
            lantern.shouldIgnite = true;
            leftTrigger = false;
        }
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        beacons = FindObjectsOfType<cWarningBeacon>();
        forestEnviron = FindObjectOfType<cBoundaryConfig>().gameObject;
        forestXLength = forestEnviron.transform.localScale.x;
        forestZLength = forestEnviron.transform.localScale.z;
        cruiseAltitude = 3;
        aggroDistance = 10;
    }
}
