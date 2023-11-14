using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{

    public static MenuManager Instance;
    void Awake()
    {
        if (Instance != null)
        {
            Destroy(this.gameObject);
            return;
        }
        Instance = this;
    }

    [SerializeField] public static int curLevel;

    private void Start()
    {
        curLevel = GameManager.Instance.curLevel;
    }
    public void onStart()
    {
        GameManager.Instance.loadCurLevel();
    }
    public void onSettings()
    {

    }
    public void onQuit()
    {
        GameManager.Instance.Quit();
    }
}
