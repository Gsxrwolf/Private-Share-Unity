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
            PlayerManager playerManager = effectedPlayer.GetComponent<PlayerManager>();
            if (playerManager != null)
            {
                playerManager.climbing = true;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player1") || other.CompareTag("Player2"))
        {
            effectedPlayer = other.gameObject;
            PlayerManager playerManager = effectedPlayer.GetComponent<PlayerManager>();
            if (playerManager != null)
            {
                playerManager.climbing = false;
            }
        }
    }
}
