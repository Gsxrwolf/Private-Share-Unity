using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class climbTrigger : MonoBehaviour
{
    private GameObject effectedPlayer;
    void Start()
    {
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        effectedPlayer = other.gameObject;
        effectedPlayer.GetComponent<Rigidbody2D>().gravityScale = 0.2f;
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        effectedPlayer = other.gameObject;
        effectedPlayer.GetComponent<Rigidbody2D>().gravityScale = 1;
    }
}
