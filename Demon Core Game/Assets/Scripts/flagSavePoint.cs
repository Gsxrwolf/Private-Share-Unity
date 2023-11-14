using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flagSavePoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameManager.Instance.curLevel++;
        GameManager.Instance.loadCurLevel();
    }
}
