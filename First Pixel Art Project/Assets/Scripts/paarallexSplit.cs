using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class paarallexSplit : MonoBehaviour
{


    private float length;
    [SerializeField] private float startPos;
    [SerializeField] public GameObject localCam;
    [SerializeField] public GameObject mainCam;
    [SerializeField] public parallexMmain mainScript;
    public float parallexEffect;


    void OnEnable()
    {
        float camOffset = mainCam.transform.position.x-localCam.transform.position.x;
        startPos = mainScript.offset + camOffset;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void LateUpdate()
    {
        float temp = (localCam.transform.position.x * (1 - parallexEffect));
        float dist = (mainCam.transform.position.x * parallexEffect);

        transform.position = new Vector2(startPos + dist , transform.position.y);

        if (temp > startPos + length)
        {
            startPos += length;
        }
        else if (temp < startPos - length)
        {
            startPos -= length;
        }
    }
}
