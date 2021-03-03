using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cPlayerCollider : MonoBehaviour
{
    public cSignalBeacon currentCheckpoint;
    public bool checker = false;
    public cLevelManager changeLevel;

    private void OnTriggerEnter(Collider other)
    {
        cSignalBeacon beaconCollide = null;
        cMonsterBehaviour monsterCollide = null;
        cGoalBeacon goalCollide = null;
        
        if (other.gameObject != null)
        {
            beaconCollide = other.gameObject.GetComponent<cSignalBeacon>();
            monsterCollide = other.gameObject.GetComponent<cMonsterBehaviour>();
            goalCollide = other.gameObject.GetComponent<cGoalBeacon>();
        }
        if (goalCollide != null)
        {
            if (goalCollide.lightOn == true)
            {
                changeLevel.LoadNextLevel(cLevelManager.specialLevel.next);
            }
        }
        if (monsterCollide != null && monsterCollide.isAwake == true)
        {
            monsterCollide.GetComponentInParent<AudioSource>().Play();
            changeLevel.LoadNextLevel(cLevelManager.specialLevel.gameover);
        }

        if (beaconCollide != null)
        {

            currentCheckpoint = beaconCollide; 
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
    }

    void triggerLantern()
    {
        if (currentCheckpoint != null) 
        {
            currentCheckpoint.shouldIgnite = true;
            currentCheckpoint = null;
        }
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        triggerLantern();
        GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
    }
}
