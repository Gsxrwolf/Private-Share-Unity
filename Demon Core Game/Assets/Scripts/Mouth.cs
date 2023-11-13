using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouth : MonoBehaviour
{

    [SerializeField] private GameObject explo1;
    [SerializeField] private GameObject explo2;
    [SerializeField] private GameObject explo3;
    [SerializeField] private GameObject explo4;
    [SerializeField] private GameObject explo5;
    [SerializeField] private GameObject explo6;
    [SerializeField] private Vector3 exploUpscaleMultipyer;
    private int exploState = 0;
    [SerializeField] private AudioSource exploSound;

    [SerializeField] private GameObject screwdriverRotationPoint;
    [SerializeField] private Rigidbody2D sRB;
    [SerializeField] private Rigidbody2D myRB;
    [SerializeField] private float mouthRotation;
    [SerializeField] private float multiplyer = -0.01f;
    [SerializeField] private float resetSpeed = 1f;
    [SerializeField] private float dropThreshold = -0.9f;
    [SerializeField] private bool droped = false;



    int i = 0;

    private void Start()
    {
        screwdriverRotationPoint.transform.GetChild(0).gameObject.GetComponent<PolygonCollider2D>().enabled = false;
    }
    void Update()
    {
        if (myRB.velocity.y < 0)
        {
            mouthRotation += myRB.velocity.y * multiplyer * -0.001f;
        }
        else if ((myRB.velocity.y == 0 && droped && mouthRotation > 0f) || (myRB.velocity.y == 0 && !droped && mouthRotation > 3.3f))
        {
            mouthRotation -= resetSpeed;
        }
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, mouthRotation);

        if (!droped)
        {
            screwdriverRotationPoint.transform.rotation = Quaternion.Euler(screwdriverRotationPoint.transform.rotation.eulerAngles.x, screwdriverRotationPoint.transform.rotation.eulerAngles.y, (-mouthRotation * 3.5f) + 10.5f);
            if (screwdriverRotationPoint.transform.rotation.z < dropThreshold)
            {
                droped = true;
                screwdriverRotationPoint.transform.GetChild(0).gameObject.GetComponent<PolygonCollider2D>().enabled = true;
                sRB = screwdriverRotationPoint.transform.GetChild(0).gameObject.AddComponent<Rigidbody2D>();
                sRB.gravityScale = 10;
            }
        }

        if (mouthRotation <= 0f)
        {


            if (explo6.transform.localScale.x > 4 && exploState == 0)
            {
                exploState = 1;
            }
            else if (explo6.transform.localScale.x < 0 && exploState == 1)
            {
                exploState = 2;
            }


            if (exploState == 0)
            {
                if (i == 0)
                {
                    exploSound.Play();
                    i++;
                }
                Debug.Log(exploState);
                explo1.transform.localScale += exploUpscaleMultipyer * 3f * Time.deltaTime;
                explo2.transform.localScale += exploUpscaleMultipyer * 2.5f * Time.deltaTime;
                explo3.transform.localScale += exploUpscaleMultipyer * 2f * Time.deltaTime;
                explo4.transform.localScale += exploUpscaleMultipyer * 1.5f * Time.deltaTime;
                explo5.transform.localScale += exploUpscaleMultipyer * 1f * Time.deltaTime;
                explo6.transform.localScale += exploUpscaleMultipyer * 0.5f * Time.deltaTime;
            }
            else if (exploState == 1)
            {
                Debug.Log(exploState);
                explo1.transform.localScale -= exploUpscaleMultipyer * 3f * Time.deltaTime;
                explo2.transform.localScale -= exploUpscaleMultipyer * 2.5f * Time.deltaTime;
                explo3.transform.localScale -= exploUpscaleMultipyer * 2f * Time.deltaTime;
                explo4.transform.localScale -= exploUpscaleMultipyer * 1.5f * Time.deltaTime;
                explo5.transform.localScale -= exploUpscaleMultipyer * 1f * Time.deltaTime;
                explo6.transform.localScale -= exploUpscaleMultipyer * 0.5f * Time.deltaTime;
            }
            else if (exploState == 2)
            {
                Debug.Log(exploState);
                explo1.transform.localScale += exploUpscaleMultipyer * 42f * Time.deltaTime;
                explo2.transform.localScale += exploUpscaleMultipyer * 30f * Time.deltaTime;
                explo3.transform.localScale += exploUpscaleMultipyer * 20f * Time.deltaTime;
                explo4.transform.localScale += exploUpscaleMultipyer * 12f * Time.deltaTime;
                explo5.transform.localScale += exploUpscaleMultipyer * 8f * Time.deltaTime;
                explo6.transform.localScale += exploUpscaleMultipyer * 4f * Time.deltaTime;
            }
        }
    }
}
