using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;
using static worldGenerator;

public class worldGenerator : MonoBehaviour
{
    [SerializeField] private Object mapPrefab1;
    [SerializeField] private Object mapPrefab2;
    [SerializeField] private Object mapPrefab3;
    [SerializeField] private Object mapPrefab4;
    [SerializeField] private GameObject p1;
    [SerializeField] private GameObject p2;
    [SerializeField] public int mapSize;
    public Player player1;
    public Player player2;
    public Chunks[] chunks;
    public List<int> chunksToRender = new List<int>();

    public class Player
    {
        public GameObject player;
        public float pXPos;
        public int curChunk;
        public Player(GameObject _player, float _pXPos)
        {
            player = _player;
            pXPos = _pXPos;
            curChunk = Mathf.RoundToInt(pXPos / 18f);
        }
    }
    public class Chunks
    {
        public Object map;
        public float mXPos;
        public Chunks(Object _map, float _mXPos)
        {
            map = _map;
            mXPos = _mXPos;
        }
    }

    void Start()
    {
        player1 = new Player(p1, 0f);
        player2 = new Player(p2, 0f);
        chunks = new Chunks[mapSize];
        for (int i = 0; i < mapSize; i++)
        {
            Debug.Log("0 i = " + i);
            if (i / 4 == 0)
            {
                Debug.Log("1 i = " + i);
                chunks[i] = new Chunks(mapPrefab1, i - mapSize / 2);
            }
            if (i / 4 == 1)
            {
                Debug.Log("2 i = " + i);
                chunks[i] = new Chunks(mapPrefab2, i - mapSize / 2);
            }
            if (i / 4 == 2)
            {
                Debug.Log("3 i = " + i);
                chunks[i] = new Chunks(mapPrefab3, i - mapSize / 2);
            }
            if (i / 4 == 3)
            {
                Debug.Log("4 i = " + i);
                chunks[i] = new Chunks(mapPrefab4, i - mapSize / 2);
            }
        }
    }


    void Update()
    {
        IdentifyChunkToRender();
        RenderChunks(chunksToRender);
    }
    public void IdentifyChunkToRender()
    {
        player1.pXPos = player1.player.transform.position.x;
        player1.curChunk = Mathf.RoundToInt(player1.pXPos / 18f);
        player2.pXPos = player2.player.transform.position.x;
        player2.curChunk = Mathf.RoundToInt(player2.pXPos / 18f);
        if (player1.curChunk - player2.curChunk == 0)
        {
            chunksToRender.Add(player1.curChunk - 1);
            chunksToRender.Add(player1.curChunk);
            chunksToRender.Add(player1.curChunk + 1);
        }

        if (player1.curChunk - player2.curChunk == 1)
        {
            chunksToRender.Add(player1.curChunk - 1);
            chunksToRender.Add(player1.curChunk);
            chunksToRender.Add(player1.curChunk + 1);

            chunksToRender.Add(player2.curChunk - 1);
        }
        if (player1.curChunk - player2.curChunk == -1)
        {
            chunksToRender.Add(player1.curChunk - 1);
            chunksToRender.Add(player1.curChunk);
            chunksToRender.Add(player1.curChunk + 1);

            chunksToRender.Add(player2.curChunk + 1);
        }

        if (player1.curChunk - player2.curChunk == 2)
        {
            chunksToRender.Add(player1.curChunk - 1);
            chunksToRender.Add(player1.curChunk);
            chunksToRender.Add(player1.curChunk + 1);

            chunksToRender.Add(player2.curChunk - 1);
            chunksToRender.Add(player2.curChunk);
        }
        if (player1.curChunk - player2.curChunk == -2)
        {
            chunksToRender.Add(player1.curChunk - 1);
            chunksToRender.Add(player1.curChunk);
            chunksToRender.Add(player1.curChunk + 1);

            chunksToRender.Add(player2.curChunk);
            chunksToRender.Add(player2.curChunk + 1);
        }

        if (player1.curChunk - player2.curChunk > 2)
        {
            chunksToRender.Add(player1.curChunk - 1);
            chunksToRender.Add(player1.curChunk);
            chunksToRender.Add(player1.curChunk + 1);

            chunksToRender.Add(player2.curChunk - 1);
            chunksToRender.Add(player2.curChunk);
            chunksToRender.Add(player2.curChunk + 1);
        }
    }
    public void RenderChunks(List<int> _chunkNum)
    {
        foreach (int chunkNum in _chunkNum)
        {
            Instantiate(chunks[chunkNum].map, new Vector3(chunks[chunkNum].mXPos * 18,0,0) , transform.rotation);
        }
        chunksToRender.Clear();
    }
}
