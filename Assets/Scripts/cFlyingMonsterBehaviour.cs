using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cFlyingMonsterBehaviour : cMonsterBehaviour
{
    public float cruiseAltitude;

    protected override bool chasePlayer(bool isChasing)
    {
        GameObject player = FindObjectOfType<pMovement>().gameObject;
        float playerDist = Vector3.Distance(this.transform.position, player.transform.position);
        if (isChasing)
        {
            if (playerDist > aggroRadius[(int)monsterState])
            {
                setTargetCoords(Random.Range(-50, 50), cruiseAltitude, Random.Range(-50, 50));
                return false;
            }
            setTargetCoords(player.transform.position.x, player.transform.position.y, player.transform.position.z);
            return true;
        }

        else
        {
            if (playerDist < aggroRadius[(int)monsterState])
            {
                setTargetCoords(player.transform.position.x, player.transform.position.y, player.transform.position.z);
                return true;
            }
            return false;
        }
    }
    protected void setTargetCoords(float newX, float newY, float newZ)
    {
        prevTarget = this.transform.position;
        moveTarget = getCoords(newX, newY, newZ);
        this.transform.LookAt(moveTarget);
    }

    protected Vector3 getCoords(float x, float y, float z)
    {
        return new Vector3(x, y, z);
    }

    protected override void Start()
    {
        base.Start();
        cruiseAltitude = transform.position.y;
    }
}
