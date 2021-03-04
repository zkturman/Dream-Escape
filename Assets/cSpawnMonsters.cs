using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cSpawnMonsters : MonoBehaviour
{
    public int numPixies, numFaeries;
    public bool initMonster, initGhoul;
    public GameObject pixiePrefab, faeriePrefab, monsterPrefab, ghoulPrefab;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < numPixies; i++)
        {
            Instantiate(pixiePrefab, new Vector3(Random.Range(-50, 50), 3, Random.Range(-50, 50)), Quaternion.identity);
        }
        for (int i = 0; i < numFaeries; i++)
        {
            Instantiate(faeriePrefab, new Vector3(Random.Range(-50, 50), 3, Random.Range(-50, 50)), Quaternion.identity);
        }
        if (initGhoul)
        {
            Instantiate(ghoulPrefab, new Vector3(Random.Range(-50, 50), 10, Random.Range(-50, 50)), Quaternion.identity);
        }
        if (initMonster)
        {
            Instantiate(monsterPrefab, new Vector3(Random.Range(-50, 50), 0, Random.Range(-50, 50)), Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
