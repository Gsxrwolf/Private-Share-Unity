using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsMenuManager : MonoBehaviour
{
    public static SettingsMenuManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance is not null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

    }
    [SerializeField] private GameObject settingsDisplay;
    [SerializeField] private GameObject leftArrowButton;
    [SerializeField] private GameObject rightArrowButton;

    public void OnLeftArrowButtonPress()
    {

    }
    public void OnRightArrowButtonPress()
    {

    }
    public void OnBackButtonPress()
    {
        GeneralMenuManager.Instance.ChangeToMainMenu();
    }
}
