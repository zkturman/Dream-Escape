using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cBoundaryConfig : MonoBehaviour
{
    List<GameObject> barriers = new List<GameObject>();
    public Material invisible;

    void setupWalls()
    {
        float xbounds = (this.transform.localScale.x / 2);
        float zbounds = (this.transform.localScale.z / 2);
        float wallHeight = 20;
        float wallThickness = 1;
        float xWallLength = (wallThickness + xbounds) * 2;
        float zWallLength = (wallThickness + zbounds) * 2;

        GameObject EWall = GameObject.CreatePrimitive(PrimitiveType.Cube);
        EWall.transform.Translate(xbounds, 0, 0);
        EWall.transform.localScale = new Vector3(wallThickness, wallHeight, zWallLength);
        EWall.GetComponent<MeshRenderer>().material = invisible;


        GameObject NWall = GameObject.CreatePrimitive(PrimitiveType.Cube);
        NWall.transform.Translate(0, 0, zbounds);
        NWall.transform.localScale = new Vector3(xWallLength, wallHeight, wallThickness);
        NWall.GetComponent<MeshRenderer>().material = invisible;


        GameObject WWall = GameObject.CreatePrimitive(PrimitiveType.Cube);
        WWall.transform.Translate(-xbounds, 0, 0);
        WWall.transform.localScale = new Vector3(1, wallHeight, zWallLength);
        WWall.GetComponent<MeshRenderer>().material = invisible;


        GameObject SWall = GameObject.CreatePrimitive(PrimitiveType.Cube);
        SWall.transform.Translate(0, 0, -zbounds);
        SWall.transform.localScale = new Vector3(xWallLength, wallHeight, wallThickness);
        SWall.GetComponent<MeshRenderer>().material = invisible;

        barriers.Add(EWall);
        barriers.Add(NWall);
        barriers.Add(WWall);
        barriers.Add(SWall);
    }
    // Start is called before the first frame update
    void Start()
    {
        setupWalls();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
