using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class climbTrigger : MonoBehaviour
{
    private GameObject effectedPlayer;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player1") || other.CompareTag("Player2"))
        {
            effectedPlayer = other.gameObject;
            PlayerControli playerControli = effectedPlayer.GetComponent<PlayerControli>();
            Rigidbody2D rigidbody2D = effectedPlayer.GetComponent<Rigidbody2D>();
            if (playerControli != null)
            {
                rigidbody2D.gravityScale = 0;
                playerControli.climbing = true;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player1") || other.CompareTag("Player2"))
        {
            effectedPlayer = other.gameObject;
            PlayerControli playerManager = effectedPlayer.GetComponent<PlayerControli>();
            Rigidbody2D rigidbody2D = effectedPlayer.GetComponent<Rigidbody2D>();
            if (playerManager != null)
            {
                rigidbody2D.gravityScale = 1;
                playerManager.climbing = false;
            }
        }
    }
}
