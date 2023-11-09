using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class parallexMmain : MonoBehaviour
{


    private float length;
    private float dist;
    [SerializeField] private float startPos;
    [SerializeField] public GameObject cam;
    public float parallexEffect;

    public float tempLink;

    void Start()
    {
        startPos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void Update()
    {

        float temp = (cam.transform.position.x * (1 - parallexEffect));
        dist = (cam.transform.position.x * parallexEffect);

        transform.position = new Vector2(dist, transform.position.y);

        if (temp > startPos + length)
        {
            startPos += length;
        }
        else if (temp < startPos - length)
        {
            startPos -= length;
        }

    }
    private void OnDisable()
    {
        tempLink = dist - cam.transform.position.x;
    }
}
