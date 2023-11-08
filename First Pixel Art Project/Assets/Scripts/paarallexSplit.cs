using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class paarallexSplit : MonoBehaviour
{


    private float length;
    [SerializeField] private float globalOffset;
    [SerializeField] private float startPos;
    [SerializeField] public GameObject localCam;
    [SerializeField] public GameObject mainCam;
    [SerializeField] public parallexMmain mainScript;
    public float parallexEffect;
    public float localOffset;


    void OnEnable()
    {
        globalOffset = mainScript.tempLink;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void Update()
    {
        localOffset = localCam.transform.position.x - mainCam.transform.position.x;
        float temp = (localCam.transform.position.x * (1 - parallexEffect));
        //float dist = ();
        transform.position = new Vector2(startPos + localCam.transform.position.x - localOffset * parallexEffect, transform.position.y);

        if (temp > globalOffset + length)
        {
            globalOffset += length;
        }
        else if (temp < globalOffset - length)
        {
            globalOffset -= length;
        }
    }
}
