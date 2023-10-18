using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class cameraMovement : MonoBehaviour
{
    [SerializeField] private GameObject player1;
    [SerializeField] private GameObject player2;
    [SerializeField] private Camera cam;
    [SerializeField] private Vector2 pos;
    [SerializeField] private float maxDistance = 15f;

    private void Start()
    {
    }
    void Update()
    {
        if (Mathf.Abs(player1.transform.position.x - player2.transform.position.x) < maxDistance)
        {
            cam.enabled = false;
        }
        else
        {
            cam.enabled = true;
            pos = player1.transform.position;
            pos.y = 0;
            transform.position = pos;
        }
    }
}
