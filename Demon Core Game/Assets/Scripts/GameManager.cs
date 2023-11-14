using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    void Awake()
    {
        if (Instance != null)
        {
            Destroy(this.gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(Instance);
    }


    public int curLevel = 1;

    public void loadCurLevel()
    {
        SceneManager.LoadScene(curLevel);
    }
    public void loadMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void loadLevel(int _sceneIndex)
    {
        SceneManager.LoadScene(_sceneIndex);
    }
    public void Quit()
    {
        Application.Quit();
    }
}
