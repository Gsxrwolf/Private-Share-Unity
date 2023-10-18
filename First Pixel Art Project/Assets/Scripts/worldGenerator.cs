using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class worldGenerator : MonoBehaviour
{
    [SerializeField] private Object mapPrefab;
    [SerializeField] private GameObject mapL;
    [SerializeField] private GameObject mapC;
    [SerializeField] private GameObject mapR;
    [SerializeField] private GameObject mapTemp;
    [SerializeField] private GameObject player;
    [SerializeField] private float pXPos;
    [SerializeField] private int curChunk;
    [SerializeField] private int lastChunk;
    [SerializeField] private Vector3 spawn;

    void Start()
    {
        pXPos = player.transform.position.x;
        spawn = new Vector2(curChunk * 18, 0);
        mapC = (GameObject)Instantiate(mapPrefab, spawn, transform.rotation);

        curChunk -= 1;
        spawn = new Vector2(curChunk * 18, 0);
        mapL = (GameObject)Instantiate(mapPrefab, spawn, transform.rotation);

        curChunk += 2;
        spawn = new Vector2(curChunk * 18, 0);
        mapR = (GameObject)Instantiate(mapPrefab, spawn, transform.rotation);


        curChunk = Mathf.RoundToInt(pXPos / 18f);
    }

    void Update()
    {
        lastChunk = curChunk;
        pXPos = player.transform.position.x;
        curChunk = Mathf.RoundToInt(pXPos / 18f);

        if (curChunk != lastChunk)
        {
            if (curChunk > lastChunk)
            {
                Debug.Log("Right");
                spawn = new Vector3((curChunk * 18) + 18, 0, 0);
                mapL.transform.position = spawn;

                mapTemp = mapR;
                mapR = mapL;
                mapL = mapC;
                mapC = mapTemp;
            }
            if (curChunk < lastChunk)
            {
                Debug.Log("Left");
                spawn = new Vector3((curChunk * 18) - 18, 0, 0);
                mapR.transform.position = spawn;

                mapTemp = mapL;
                mapL = mapR;
                mapR = mapC;
                mapC = mapTemp;
            }

        }
    }
}
