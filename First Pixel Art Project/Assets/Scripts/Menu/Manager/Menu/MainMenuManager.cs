using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MainMenuManager : MonoBehaviour
{
    public static MainMenuManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance is not null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

    }


    [SerializeField] private GameObject playButton;
    [SerializeField] private GameObject settingsButton;
    [SerializeField] private GameObject quitButton;



    public void OnPlayButtonPress()
    {
        GeneralMenuManager.Instance.ChangeToPlayMenu();
    }
    public void OnSettingsButtonPress()
    {
        GeneralMenuManager.Instance.ChangeToSettingsMenu();
    }
    public void OnQuitButtonPress()
    {
        Application.Quit();
    }
}
