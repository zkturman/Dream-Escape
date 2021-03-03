using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cPixieBehaviour : cFlyingMonsterBehaviour
{
    cCheckpointBeacon[] checkpoints;
    public bool leftTrigger = true;
    protected override void moveMonster()
    {
        if (!isMoving)
        {
            setTargetCoords(Random.Range(-50, 50), cruiseAltitude, Random.Range(-50, 50));
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
        cCheckpointBeacon lantern = closestLantern();
        if (lantern == null)
        {
            return false;
        }
        Vector3 target = lantern.transform.position;
        float targetDistance = Vector3.Distance(transform.position, target);
        if (isChasing)
        {
            if (targetDistance > aggroRadius[4])
            {
                setTargetCoords(Random.Range(-50, 50), cruiseAltitude, Random.Range(-50, 50));
                return false;
            }
            setTargetCoords(target.x, target.y, target.z);
            return true;
        }
        else
        {
            if (targetDistance < aggroRadius[4]){
                setTargetCoords(target.x, target.y, target.z);
                return true;
            }
            return false;
        }
    }

    protected virtual cCheckpointBeacon closestLantern()
    {
        bool found = false;
        int i = 0;
        cCheckpointBeacon minLantern = null;
        while (!found && i < checkpoints.Length)
        {
            if (checkpoints[i].lightOn)
            {
                minLantern = checkpoints[i];
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
        for(i = 0; i < checkpoints.Length; i++)
        {
            if (checkpoints[i].lightOn)
            {
                if (Vector3.Distance(transform.position, checkpoints[i].transform.position) < min)
                {
                    minLantern = checkpoints[i];
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
        Debug.Log("We logged a collision");
        if (other.gameObject != null)
        {
            lantern = other.GetComponentInChildren<cCheckpointBeacon>();
        }
        if (lantern != null)
        {
            Debug.Log("We logged a collision with a lantern");
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
        checkpoints = FindObjectsOfType<cCheckpointBeacon>();
        forestEnviron = FindObjectOfType<cBoundaryConfig>().gameObject;
        cruiseAltitude = 3;
    }

    // Update is called once per frame
    protected override void Update()
    {
        moveMonster();
    }
}
