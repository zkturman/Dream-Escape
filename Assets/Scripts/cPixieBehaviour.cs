using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cPixieBehaviour : cFlyingMonsterBehaviour
{
    public cSignalBeacon[] beacons;
    public bool leftTrigger = true;
    public float aggroDistance = 20;
    protected override void moveMonster()
    {
        if (!isMoving)
        {
            setTargetCoords(Random.Range(-forestXLength, forestXLength), cruiseAltitude, Random.Range(-forestZLength, forestZLength));
            isMoving = true;
        }

        if (leftTrigger)
        {
            isChasing = chaseLantern(isChasing);
        }

        if (Vector3.Distance(this.transform.position, prevTarget) <= Vector3.Distance(moveTarget, prevTarget))
        {
            setMoveBehaviour();
        }
        else
        {
            isMoving = false;
        }
    }

    protected virtual bool chaseLantern(bool isChasing)
    {
        cSignalBeacon lantern = closestLantern(true);
        if (lantern == null)
        {
            return false;
        }
        Vector3 target = lantern.transform.position;
        float targetDistance = Vector3.Distance(transform.position, target);
        if (isChasing)
        {
            if (targetDistance > aggroRadius[2])
            {
                setTargetCoords(Random.Range(-forestXLength, forestXLength), cruiseAltitude, Random.Range(-forestZLength, forestZLength));
                return false;
            }
            setTargetCoords(target.x, target.y, target.z);
            return true;
        }
        else
        {
            if (targetDistance < aggroRadius[2]){
                setTargetCoords(target.x, target.y, target.z);
                return true;
            }
            return false;
        }
    }

    protected virtual cSignalBeacon closestLantern(bool lightOn)
    {
        bool found = false;
        int i = 0;
        cSignalBeacon minLantern = null;
        while (!found && i < beacons.Length)
        {
            if (lightOn == beacons[i].lightOn)
            {
                minLantern = beacons[i];
                found = true;
            }
            else
            {
                i++;
            }
        }

        if (minLantern == null)
        {
            return null;
        }

        float min = Vector3.Distance(transform.position, minLantern.transform.position);
        for(i = 0; i < beacons.Length; i++)
        {
            if (lightOn == beacons[i].lightOn)
            {
                if (Vector3.Distance(transform.position, beacons[i].transform.position) < min)
                {
                    minLantern = beacons[i];
                }
            }
        }
        return minLantern;
    }

    protected override void setMoveBehaviour()
    {
        this.transform.Translate(Vector3.forward * (float)0.05);
    }

    private void OnTriggerEnter(Collider other)
    {
        cCheckpointBeacon lantern = null;
        if (other.gameObject != null)
        {
            lantern = other.GetComponentInChildren<cCheckpointBeacon>();
        }
        if (lantern != null && lantern.lightOn)
        {
            lantern.shouldExtinguish = true;
            leftTrigger = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        leftTrigger = true;
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        beacons = FindObjectsOfType<cCheckpointBeacon>();
        forestEnviron = FindObjectOfType<cBoundaryConfig>().gameObject;
        forestXLength = forestEnviron.transform.localScale.x;
        forestZLength = forestEnviron.transform.localScale.z;
        cruiseAltitude = 3;
    }

    // Update is called once per frame
    protected override void Update()
    {
        moveMonster();
    }
}
