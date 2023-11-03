using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class parallex : MonoBehaviour
{

    private float length;
    [SerializeField] private float startPos;
    [SerializeField] public GameObject camera;
    public GameObject localCam;
    public float parallexEffect;

    void Start()
    {
        if (tag == "SplitCam")
        {
            startPos = camera.transform.position.x;
        }
        else
        {
            startPos = transform.position.x;
        }
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void FixedUpdate()
    {

        float temp = (localCam.transform.position.x * (1 - parallexEffect));
        float dist;


        if (tag == "SplitCam")
        {
            dist = (localCam.transform.position.x * parallexEffect);
        }
        else
        {
            dist = (localCam.transform.position.x * parallexEffect * 2);
        }
        transform.position = new Vector2(startPos + dist, transform.position.y);

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
