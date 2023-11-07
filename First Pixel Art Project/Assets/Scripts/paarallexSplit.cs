using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class paarallexSplit : MonoBehaviour
{


    private float length;
    [SerializeField] private float globalOffset;
    [SerializeField] public GameObject localCam;
    [SerializeField] public GameObject mainCam;
    [SerializeField] public parallexMmain mainScript;
    public float parallexEffect;
    public float localOffset;
    public float camPos;


    void OnEnable()
    {
        localOffset = localCam.transform.position.x-mainCam.transform.position.x;
        globalOffset = mainScript.offset;
        camPos = globalOffset + localOffset;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void LateUpdate()
    {
        float temp = (localCam.transform.position.x * (1 - parallexEffect));
        float dist = (localOffset * parallexEffect);
        transform.position = new Vector2(globalOffset + dist , transform.position.y);

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
