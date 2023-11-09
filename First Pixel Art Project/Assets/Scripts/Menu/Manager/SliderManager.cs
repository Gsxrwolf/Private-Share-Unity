using UnityEngine;

public class SliderManager : MonoBehaviour
{
    public static SliderManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance is not null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

    }


    private float value;
    private float editValue;
    private float minValue;
    private float maxValue;

    [SerializeField] private GameObject curValueCursor;
    [SerializeField] private GameObject editValueCursor;

    [SerializeField] private GameObject activePlayer;

    [SerializeField] private BoxCollider2D saveTrigger;
    [SerializeField] private BoxCollider2D editTrigger;
    [SerializeField] private bool editMode;

    private void Update()
    {
        curValueCursor.transform.position = new Vector3(value, curValueCursor.transform.position.y, curValueCursor.transform.position.z);
        if (editMode)
        {
            editValueCursor.transform.position = new Vector3(activePlayer.transform.position.x, editValueCursor.transform.position.y, editValueCursor.transform.position.z);
        }
        else
        {
            editValueCursor.transform.position = new Vector3(curValueCursor.transform.position.x, editValueCursor.transform.position.y, editValueCursor.transform.position.z);
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.IsTouching(saveTrigger))
        {
            editMode = false;

        }
        else if (other.IsTouching(editTrigger))
        {
            editMode = true;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if(activePlayer == null)
        {
            activePlayer = other.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.IsTouching(saveTrigger))
        {
            editMode = true;

        }
        else if (other.IsTouching(editTrigger))
        {
            activePlayer = null;
            editMode = false;
        }
    }
}
