using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GeneralMenuManager : MonoBehaviour
{
    public static GeneralMenuManager Instance { get; private set; }
    private void Awake()
    {
        if(Instance is not null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

    }

    [SerializeField] private int curLevel;
    [SerializeField] private GameObject currentMenu;

    [SerializeField] private GameObject mainMenuManager;
    [SerializeField] private GameObject playMenuManager;
    [SerializeField] private GameObject settingsMenuManager;

    private void Start()
    {
        playMenuManager.SetActive(false);
        settingsMenuManager.SetActive(false);
        currentMenu = mainMenuManager;
        currentMenu.SetActive(true);
    }

    public void ChangeToMainMenu()
    {
        currentMenu.SetActive(false);
        currentMenu = mainMenuManager;
        currentMenu.SetActive(true);
    }
    public void ChangeToPlayMenu()
    {
        currentMenu.SetActive(false);
        currentMenu = playMenuManager; 
        currentMenu.SetActive(true);
    }
    public void ChangeToSettingsMenu()
    {
        currentMenu.SetActive(false);
        currentMenu = settingsMenuManager;
        currentMenu.SetActive(true);
    }



}
