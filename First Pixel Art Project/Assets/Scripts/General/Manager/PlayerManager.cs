using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Timers;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{

    public static PlayerManager Instance { get; private set;}

    Stopwatch stopwatch = new Stopwatch();
    [SerializeField] public GameObject p1;
    [SerializeField] public GameObject p2;
    [SerializeField] public int player1Time;
    [SerializeField] public int player2Time;
    [SerializeField] public int curP;

    [SerializeField] public int timeReduction;

    [SerializeField] public GameObject uiText;


    System.Random rnd = new System.Random();

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(this.gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(this.gameObject);
    }
    private void Start()
    {
        curP = rnd.Next(1,3);
    }
    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(p1.transform.position.x - p2.transform.position.x) < 8.9)
        {
            stopwatch.Stop();
            p1.GetComponent<PlayerControli>().lock1 = false;
            p1.transform.GetChild(2).gameObject.SetActive(true);
            p2.GetComponent<PlayerControli>().lock2 = false;
            p2.transform.GetChild(2).gameObject.SetActive(true);
            uiText.GetComponent<TextMeshProUGUI>().text = null;

        }
        else
        {
            RunTimers();
        }
    }

    private void RunTimers()
    {
        if (curP == 1)
        {
            p1.GetComponent<PlayerControli>().lock1 = false;
            p1.transform.GetChild(2).gameObject.SetActive(true);
            p2.GetComponent<PlayerControli>().lock2 = true;
            p2.transform.GetChild(2).gameObject.SetActive(false);

            stopwatch.Start();

            uiText.GetComponent<TextMeshProUGUI>().text = (player1Time - (int)stopwatch.Elapsed.TotalSeconds).ToString();

            if (player1Time - (int)stopwatch.Elapsed.TotalSeconds == 0)
            {
                stopwatch.Stop();
                player1Time -= timeReduction;
                curP = 2;
                stopwatch.Reset();
            }
        }
        if (curP == 2)
        {
            p2.GetComponent<PlayerControli>().lock2 = false;
            p2.transform.GetChild(2).gameObject.SetActive(true);
            p1.GetComponent<PlayerControli>().lock1 = true;
            p1.transform.GetChild(2).gameObject.SetActive(false);

            stopwatch.Start();

            uiText.GetComponent<TextMeshProUGUI>().text = (player1Time - (int)stopwatch.Elapsed.TotalSeconds).ToString();

            if (player2Time - (int)stopwatch.Elapsed.TotalSeconds == 0)
            {
                stopwatch.Stop();
                player2Time -= timeReduction;
                curP = 1;
                stopwatch.Reset();
            }
        }
    }
}
