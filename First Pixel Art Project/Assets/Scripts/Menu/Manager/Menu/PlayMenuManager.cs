using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMenuManager : MonoBehaviour
{
    public static PlayMenuManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance is not null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

    }
    public void OnLoadGameButtonPress()
    {

    }
    public void OnStartNewGameButtonPress()
    {

    }
    public void OnBackButtonPress()
    {
        GeneralMenuManager.Instance.ChangeToMainMenu();
    }

}
