using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class cSignalBeacon : MonoBehaviour
{
    public bool lightOn = false;
    public bool shouldIgnite = false;
    public bool shouldExtinguish = false;

    public void lightSignal()
    {
        if (shouldIgnite == true)
        {
            igniteLight();
        }
    }

    public virtual void igniteLight()
    {
        lightOn = true;
        shouldIgnite = false;
    }

    public virtual void extinguishLight()
    {
        lightOn = false;
        shouldExtinguish = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
   void Update()
    {
        if (lightOn == false && shouldIgnite == true)
        {
            igniteLight();
        }

        if (lightOn == true && shouldExtinguish == true)
        {
            extinguishLight();
        }
    }
}
