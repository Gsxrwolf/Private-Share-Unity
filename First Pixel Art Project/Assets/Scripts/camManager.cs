using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class camManager : MonoBehaviour
{
    [SerializeField] private GameObject player1;
    [SerializeField] private GameObject player2;
    [SerializeField] private GameObject camera1;
    [SerializeField] private GameObject camera2;
    [SerializeField] private Camera cam;
    [SerializeField] private Camera cam1;
    [SerializeField] private Camera cam2;
    [SerializeField] private Vector2 pos;
    [SerializeField] private Vector2 pos1;
    [SerializeField] private Vector2 pos2;
    [SerializeField] private float maxDistance = 15f;

    [SerializeField] private GameObject splitScreenUI;

    private void Start()
    {
    }
    void Update()
    {
        splitScreenUI.GetComponent<RectTransform>().sizeDelta = new Vector2(3, Screen.height);
        if (Mathf.Abs(player1.transform.position.x - player2.transform.position.x) < maxDistance)
        {
            cam.enabled = true;
            cam1.enabled = false;
            cam2.enabled = false;
            splitScreenUI.SetActive(false);
            followBoth();
        }
        else
        {
            cam1.enabled = true;
            cam2.enabled = true;
            splitScreenUI.SetActive(true);
            cam.enabled = false;
            if (player1.transform.position.x - player2.transform.position.x < 0)
            { 
                followP1(cam1);
                followP2(cam2);
            }
            if (player1.transform.position.x - player2.transform.position.x > 0)
            {
                followP1(cam2);
                followP2(cam1);
            }
        }
    }
    public void followP1(Camera _cam)
    {
        pos1 = player1.transform.position;
        pos1.y = 0;
        _cam.transform.position = pos1;
    }
    public void followP2(Camera _cam)
    {
        pos2 = player2.transform.position;
        pos2.y = 0;
        _cam.transform.position = pos2;
    }
    public void followBoth()
    {
        pos = ((player1.transform.position - player2.transform.position) / 2) + player2.transform.position;
        pos.y = 0;
        transform.position = pos;
    }
}
