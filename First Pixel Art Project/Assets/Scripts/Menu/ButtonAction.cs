using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ButtonAction : MonoBehaviour
{
    [SerializeField] private BoxCollider2D clickTrigger;
    [SerializeField] private BoxCollider2D highlightTrigger;
    private SpriteRenderer sR;

    [SerializeField] private Color highlightColor;
    [SerializeField] private Color baseColor;


    [SerializeField] UnityEvent OnHeroTouch;

    private void Start()
    {
        sR = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.IsTouching(clickTrigger))
        {
            Debug.Log("enter");
            OnHeroTouch.Invoke();
        }
        else if (collision.IsTouching(highlightTrigger))
        {
            Debug.Log("highlight");
            sR.color = highlightColor;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        sR.color = baseColor;
    }
}
