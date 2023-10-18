using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillParticles : MonoBehaviour
{
    [SerializeField] bool triggered;
    private void OnTriggerEnter2D(Collider2D other)
    {
        triggered = true;
        if (other.CompareTag("Particle"))
        {
            Destroy(other.gameObject);
        }
    }
}
