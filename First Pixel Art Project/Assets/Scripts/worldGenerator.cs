using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    public List<int> visibleChunks = new List<int>();
    public List<int> lastVisibleChunks = new List<int>();
    public List<int> newChunksToRender = new List<int>();
    public List<int> oldChunksToDelete = new List<int>();
    
    public class Player
    {
        public GameObject player;
        public float pXPos;
        public int curChunk;
        public Player(GameObject _player)
        {
            player = _player;
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
        player1 = new Player(p1);
        player2 = new Player(p2);
        chunks = new Chunks[mapSize]; 
        System.Random rnd = new System.Random();
        for (int i = 0; i < mapSize; i++)
        {
            int temp = rnd.Next(0, 4);
            if (temp == 0)
            {
                Debug.Log("1 i = " + i);
                chunks[i] = new Chunks(mapPrefab1, i - mapSize / 2);
            }
            if (temp == 1)
            {
                Debug.Log("2 i = " + i);
                chunks[i] = new Chunks(mapPrefab2, i - mapSize / 2);
            }
            if (temp == 2)
            {
                Debug.Log("3 i = " + i);
                chunks[i] = new Chunks(mapPrefab3, i - mapSize / 2);
            }
            if (temp == 3)
            {
                Debug.Log("4 i = " + i);
                chunks[i] = new Chunks(mapPrefab4, i - mapSize / 2);
            }
        }
    }


    void Update()
    {
        IdentifyChunkToRender();
        if(ChunksChanged())
        {
            oldChunksToDelete = lastVisibleChunks.Except(visibleChunks).ToList();
            if(oldChunksToDelete.Count != 0 )
            {
                DeleteOldChunks(oldChunksToDelete);
            }
            newChunksToRender = visibleChunks.Except(lastVisibleChunks).ToList();
            RenderNewChunks(newChunksToRender);
            lastVisibleChunks = visibleChunks;
        }
    }

    public void DeleteOldChunks(List<int> _chunkNum)
    {
        Debug.Log("DeleteOldChunks");
        foreach (int chunkNum in _chunkNum)
        {
            int curChunkNum = chunkNum + mapSize / 2;
            Destroy(chunks[curChunkNum].map);
        }
    }

    public bool ChunksChanged()
    {
        Debug.Log("ChunksChanged");
        bool output;
        if(lastVisibleChunks == visibleChunks)
        {
            Debug.Log("not changed");
            output = false;
        }
        else
        {
            Debug.Log("changed");
            output = true;
        }
        return output;
    }
    public void IdentifyChunkToRender()
    {
        Debug.Log("IdentifyChunkToRender");
        visibleChunks.Clear();
        player1.pXPos = player1.player.transform.position.x;
        player1.curChunk = Mathf.RoundToInt(player1.pXPos / 18f);
        player2.pXPos = player2.player.transform.position.x;
        player2.curChunk = Mathf.RoundToInt(player2.pXPos / 18f);
        if (player1.curChunk - player2.curChunk == 0)
        {
            visibleChunks.Add(player1.curChunk - 1);
            visibleChunks.Add(player1.curChunk);
            visibleChunks.Add(player1.curChunk + 1);
        }

        if (player1.curChunk - player2.curChunk == 1)
        {
            visibleChunks.Add(player1.curChunk - 1);
            visibleChunks.Add(player1.curChunk);
            visibleChunks.Add(player1.curChunk + 1);

            visibleChunks.Add(player2.curChunk - 1);
        }
        if (player1.curChunk - player2.curChunk == -1)
        {
            visibleChunks.Add(player1.curChunk - 1);
            visibleChunks.Add(player1.curChunk);
            visibleChunks.Add(player1.curChunk + 1);

            visibleChunks.Add(player2.curChunk + 1);
        }

        if (player1.curChunk - player2.curChunk == 2)
        {
            visibleChunks.Add(player1.curChunk - 1);
            visibleChunks.Add(player1.curChunk);
            visibleChunks.Add(player1.curChunk + 1);

            visibleChunks.Add(player2.curChunk - 1);
            visibleChunks.Add(player2.curChunk);
        }
        if (player1.curChunk - player2.curChunk == -2)
        {
            visibleChunks.Add(player1.curChunk - 1);
            visibleChunks.Add(player1.curChunk);
            visibleChunks.Add(player1.curChunk + 1);

            visibleChunks.Add(player2.curChunk);
            visibleChunks.Add(player2.curChunk + 1);
        }

        if (player1.curChunk - player2.curChunk > 2 || player1.curChunk - player2.curChunk < -2)
        {
            visibleChunks.Add(player1.curChunk - 1);
            visibleChunks.Add(player1.curChunk);
            visibleChunks.Add(player1.curChunk + 1);

            visibleChunks.Add(player2.curChunk - 1);
            visibleChunks.Add(player2.curChunk);
            visibleChunks.Add(player2.curChunk + 1);
        }
        Debug.Log("------------------------------------------------------------");
        Debug.Log(player1.curChunk - player2.curChunk);
        Debug.Log("------------------------------------------------------------");
    }
    public void RenderNewChunks(List<int> _chunkNum)
    {
        Debug.Log("RenderNewChunks");
        foreach (int chunkNum in _chunkNum)
        {
            int curChunkNum = chunkNum + mapSize / 2;
            Instantiate(chunks[curChunkNum].map, new Vector3(chunks[curChunkNum].mXPos * 18,0,0) , transform.rotation);
        }
    }
}
