using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField] private GameObject particle;
    [SerializeField] private GameObject parent;
    [SerializeField] private int particleNum = 5;
    [SerializeField] private int curParticleNum;
    private Vector2 pos;
    private Quaternion rot;
    // Start is called before the first frame update
    void Start()
    {
        pos = transform.position;
        rot = transform.rotation;
        for (int y = 0; particleNum > curParticleNum; y++)
        {
            for (int x = 0; x < 20 && particleNum > curParticleNum; x++)
            {
                Vector2 offset = new Vector2(x / 2.5f - 3.8f, y / 2.5f);
                GameObject Particle =  Instantiate(particle, pos + offset, rot);
                Particle.transform.parent = parent.transform;
                curParticleNum++;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
