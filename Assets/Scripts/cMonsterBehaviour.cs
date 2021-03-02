using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cMonsterBehaviour : MonoBehaviour
{
    public GameObject monster;
    public cWarningBeacon[] lanterns;
    public int litLanterns = 0;
    public bool isAwake = false;
    public bool isMoving = false;
    public bool isChasing = false;
    public Vector3 prevTarget = new Vector3(0, 0, 0);
    public Vector3 moveTarget = new Vector3(0, 0, 0);
    public int[] aggroRadius = new int[(int)status.ravenous + 1] { 10, 15, 20, 30, 50, 100 };
    public enum status
    {
        absent,
        normal,
        alert,
        angry,
        crazed,
        ravenous
    }

    public int numStates = 5;

    public status monsterState = status.absent;

    void updateMonsterState()
    {
        litLanterns = 0;
        foreach (cWarningBeacon lan in lanterns)
        {
            if (lan.shouldIgnite == true)
            {
                litLanterns++;
                //this.GetComponentInChildren<Animator>().SetTrigger("MakeWake");
                //this.GetComponentInParent<AudioSource>().Play();
            }
        }
        float ratio = (float)litLanterns / (float)lanterns.Length;
        monsterState = (status)((float)ratio * (float)numStates);
    }

    void wakeMonster()
    {
        Debug.Log("We're waking now");
        this.GetComponentInChildren<Renderer>().enabled = true;
        this.GetComponentInChildren<AudioSource>().enabled = true;
        isAwake = true;
        this.GetComponentInChildren<Animator>().SetTrigger("MakeWake");
        this.GetComponentInParent<AudioSource>().Play();
    }

    void moveMonster()
    {
        Debug.Log("Monster is moving");
        if (!isMoving)
        {
            prevTarget = getCoords(this.transform.position.x, this.transform.position.z);
            moveTarget = getCoords(Random.Range(-50, 50), Random.Range(-50, 50));
            Debug.Log("X coord: " + moveTarget.x + " Z coord: " + moveTarget.z);
            Debug.Log("Prev X coord: " + prevTarget.x + " Prev Z coord: " + prevTarget.z);
            this.transform.LookAt(moveTarget);
            isMoving = true;

        }
        GameObject player = FindObjectOfType<pMovement>().gameObject;
        float playerDist = Vector3.Distance(this.transform.position, player.transform.position);
        if (playerDist < aggroRadius[(int)monsterState])
        {
            Debug.Log("Now Monster will chase Player");
            moveTarget = getCoords(player.transform.position.x, player.transform.position.z);
            this.transform.LookAt(moveTarget);
            isChasing = true;
        }
        if (isChasing && playerDist > aggroRadius[(int)monsterState])
        {
            Debug.LogError("We've messed up here");
            moveTarget = getCoords(Random.Range(-50, 50), Random.Range(-50, 50));
            this.transform.LookAt(moveTarget);
            isChasing = false;
        }
        if (!this.GetComponentInChildren<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Scream")){
            Debug.LogError("going wrong here");
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

    private Vector3 getCoords(float x, float z)
    {
        Vector3 coords = new Vector3(x, 0, z);
        return coords;
    }
    
    private void setMoveBehaviour()
    {
        if (monsterState == status.normal)
        {
            this.GetComponentInChildren<Animator>().SetTrigger("MakeWalk");
            this.transform.Translate(Vector3.forward * (float)0.05);
        }
        if (monsterState >= status.angry)
        {
            Debug.Log("We should be running now");
            this.GetComponentInChildren<Animator>().SetTrigger("MakeRun");
            this.transform.Translate(Vector3.forward * (float)0.1);
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        monster.GetComponentInChildren<Renderer>().enabled = false;
        monster.GetComponent<AudioSource>().enabled = false;
        lanterns = FindObjectsOfType<cWarningBeacon>();
    }

    // Update is called once per frame
    void Update()
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
