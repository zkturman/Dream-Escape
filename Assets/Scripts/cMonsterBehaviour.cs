using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class cMonsterBehaviour : MonoBehaviour
{
    public enum status
    {
        absent,
        normal,
        alert,
        angry,
        crazed,
        ravenous
    }

    public GameObject monster;
    public cWarningBeacon[] lanterns;
    public int litLanterns = 0;
    public bool isAwake = false;
    public bool isMoving = false;
    public bool isChasing = false;
    public Vector3 prevTarget = new Vector3(0, 0, 0);
    public Vector3 moveTarget = new Vector3(0, 0, 0);
    public int[] aggroRadius = new int[(int)status.ravenous + 1] { 10, 15, 20, 30, 50, 100 };
    public string wakeAnimTrigger = "MakeWake";
    public string slowAnimTrigger = "MakeWalk";
    public string fastAnimTrigger = "MakeRun";
    public status monsterState = status.absent;
    protected GameObject forestEnviron;
    protected float forestXLength;
    protected float forestZLength;

    public void updateMonsterState()
    {
        litLanterns = 0;
        foreach (cWarningBeacon lan in lanterns)
        {
            if (lan.shouldIgnite == true)
            {
                litLanterns++;
            }
        }
        if (lanterns.Length != 0)
        {
            float ratio = (float)litLanterns / (float)lanterns.Length;
            float newState = ((float)ratio * ((float)Enum.GetNames(typeof(status)).Length - 1));
            monsterState = (status)newState;
        }
    }

    public void wakeMonster()
    {
        isAwake = true;
        renderMonster(true);
        this.GetComponentInChildren<Animator>().SetTrigger(wakeAnimTrigger);
        this.GetComponentInParent<AudioSource>().Play();
    }

    protected virtual void moveMonster()
    {

        if (!isMoving)
        {
            setTargetCoords(Random.Range(-forestXLength, forestXLength), Random.Range(-forestZLength, forestZLength));
            isMoving = true;

        }

        isChasing = chasePlayer(isChasing);

        if (!GetComponentInChildren<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Scream"))
        {
            if (Vector3.Distance(this.transform.position, prevTarget) <= Vector3.Distance(moveTarget,prevTarget))
            {
                setMoveBehaviour();
            }
            else
            {
                isMoving = false;
            }

        }
    }

    protected virtual bool chasePlayer(bool isChasing)
    {
        GameObject player = FindObjectOfType<pMovement>().gameObject;
        float playerDist = Vector3.Distance(this.transform.position, player.transform.position);
        if (isChasing)
        {
            if (playerDist > aggroRadius[(int)monsterState])
            {
                setTargetCoords(Random.Range(-forestXLength, forestXLength), Random.Range(-forestZLength, forestZLength));
                return false;
            }
            setTargetCoords(player.transform.position.x, player.transform.position.z);
            return true;
        }

        else
        {
            if (playerDist < aggroRadius[(int)monsterState])
            {
                setTargetCoords(player.transform.position.x, player.transform.position.z);
                return true;
            }
            return false;
        }
    }

    protected virtual void setTargetCoords(float newX, float newZ)
    {
        prevTarget = getCoords(this.transform.position.x, this.transform.position.z);
        moveTarget = getCoords(newX, newZ);
        this.transform.LookAt(moveTarget);
    }
    protected virtual Vector3 getCoords(float x, float z)
    {
        Vector3 coords = new Vector3(x, 0, z);
        return coords;
    }
    
    protected virtual void setMoveBehaviour()
    {
        if (monsterState <= status.angry)
        {
            this.GetComponentInChildren<Animator>().SetTrigger(slowAnimTrigger);
            this.transform.Translate(Vector3.forward * (float)0.05);
        }
        if (monsterState >= status.angry)
        {
            this.GetComponentInChildren<Animator>().SetTrigger(fastAnimTrigger);
            this.transform.Translate(Vector3.forward * (float)0.1);
        }

    }

    protected void renderMonster(bool showRenderer)
    {
        Renderer[] allRenders;

        allRenders = monster.GetComponentsInChildren<Renderer>();
        foreach (Renderer r in allRenders)
        {
            r.enabled = showRenderer;
        }
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        renderMonster(false);
        lanterns = FindObjectsOfType<cWarningBeacon>();
        forestEnviron = FindObjectOfType<cBoundaryConfig>().gameObject;
        forestXLength = forestEnviron.transform.localScale.x / 2;
        forestZLength = forestEnviron.transform.localScale.z / 2;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        updateMonsterState();
        if (isAwake == false && monsterState > status.absent)
        {
            wakeMonster();
        }
        if (isAwake)
        {
            moveMonster();
        }
    }
}
