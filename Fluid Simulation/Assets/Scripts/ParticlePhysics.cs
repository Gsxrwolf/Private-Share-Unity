using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript1 : MonoBehaviour
{
    public float repelRadius = 2.0f;
    public float repelForce = 10.0f;
    public int framesBetweenUpdates = 5;

    private int frameCounter = 0;
    private Collider2D[] nearbyKugeln;

    private void Start()
    {
        nearbyKugeln = new Collider2D[1000]; // Anzahl an Kugeln, die in deinem Bereich sein könnten
    }

    private void FixedUpdate()
    {
        frameCounter++;
        if (frameCounter >= framesBetweenUpdates)
        {
            frameCounter = 0;

            // Vorab-Abfrage, um nur Kugeln in den Array aufzunehmen
            int numKugeln = Physics2D.OverlapCircleNonAlloc(transform.position, repelRadius, nearbyKugeln);

            for (int i = 0; i < numKugeln; i++)
            {
                if (nearbyKugeln[i].CompareTag("Particle"))
                {
                    Vector2 repelDirection = (nearbyKugeln[i].transform.position - transform.position).normalized;
                    float distance = Vector2.Distance(transform.position, nearbyKugeln[i].transform.position);
                    float force = repelForce / distance;

                    nearbyKugeln[i].attachedRigidbody.AddForce(repelDirection * force);
                }
            }
        }
    }
}
