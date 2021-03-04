using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cSpawnMonsters : MonoBehaviour
{
    public int numPixies, numFaeries;
    public bool initMonster, initGhoul;
    public GameObject pixiePrefab, faeriePrefab, monsterPrefab, ghoulPrefab;
    public GameObject forestEnviron;
    // Start is called before the first frame update
    void Start()
    {
        float xrange, zrange;
        forestEnviron = FindObjectOfType<cBoundaryConfig>().gameObject;
        xrange = forestEnviron.transform.localScale.x / 2;
        zrange = forestEnviron.transform.localScale.z / 2;

        for (int i = 0; i < numPixies; i++)
        {
            Instantiate(pixiePrefab, new Vector3(Random.Range(-xrange, xrange), 3, Random.Range(-zrange, zrange)), Quaternion.identity);
        }
        for (int i = 0; i < numFaeries; i++)
        {
            Instantiate(faeriePrefab, new Vector3(Random.Range(-xrange, xrange), 3, Random.Range(-zrange, zrange)), Quaternion.identity);
        }
        if (initGhoul)
        {
            Instantiate(ghoulPrefab, new Vector3(Random.Range(-xrange, xrange), 10, Random.Range(-zrange, zrange)), Quaternion.identity);
        }
        if (initMonster)
        {
            Instantiate(monsterPrefab, new Vector3(Random.Range(-xrange, xrange), 0, Random.Range(-zrange, zrange)), Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
