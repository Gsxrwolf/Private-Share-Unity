using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using static Unity.VisualScripting.Member;

public class WhipThingy : MonoBehaviour
{
    [SerializeField] private bool mouseTracking;
    [SerializeField] private GameObject grabber;
    [SerializeField] private Vector3 mousePos;
    [SerializeField] private float leachLeangth;
    [SerializeField] private float distance;
    [SerializeField] private Rigidbody2D RB;
    [SerializeField] private float velocity;

    // Start is called before the first frame update
    void Start()
    {
        RB = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetMouseButton(0))
        {
            mouseTracking = true;
        }
        else
        {
            mouseTracking = false;
        }
        if (mouseTracking)
        {
            grabber.SetActive(false);
            Vector2 direction = (mousePos - transform.position).normalized;
            distance = Vector2.Distance(transform.position, mousePos)+2;
            velocity = (-leachLeangth  + distance );

            RB.AddForce(direction * velocity);
        }
        else
        {
            grabber.SetActive(true);
            Vector2 direction = (grabber.transform.position - transform.position).normalized;
            distance = Vector2.Distance(transform.position, grabber.transform.position);
            velocity = -leachLeangth + distance;

            RB.AddForce(direction * velocity);
        }
    }
}
