using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fallTrigger : MonoBehaviour
{
    public BoxCollider2D fC; 
    private GameObject effectedPlayer;
    void Start()
    {
        fC = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        effectedPlayer = other.gameObject;
        Vector3 teleportPoint = new Vector3(fC.transform.position.x - 2.5f, fC.transform.position.y + 10f);
        effectedPlayer.transform.position = teleportPoint;
    }
}
