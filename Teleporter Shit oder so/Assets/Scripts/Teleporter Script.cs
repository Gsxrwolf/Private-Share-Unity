using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleporterScript : MonoBehaviour
{
    [SerializeField] GameObject otherPad;
    [SerializeField] Vector3 endPoint;
    // Start is called before the first frame update
    void Start()
    {
        endPoint = otherPad.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            other.transform.position = endPoint;
        }
    }
}
