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
    private int[] biomCounter;
    [SerializeField] private GameObject p1;
    [SerializeField] private GameObject p2;
    [SerializeField] public int mapSize;
    public Player player1;
    public Player player2;
    [SerializeField] public Chunks[] chunks;
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
        public GameObject reference;
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
        biomCounter = new int[4];
        System.Random rnd = new System.Random();
        for (int i = 0; i < mapSize; i++)
        {
            bool valid = false;
            do
            {
                int temp = rnd.Next(0, 4);
                if (temp == 0)
                {
                    valid = true;
                    biomCounter[temp] += 1;
                    chunks[i] = new Chunks(mapPrefab1, i - mapSize / 2);
                }
                if (temp == 1 && biomCounter[temp] < 3 && i > mapSize / 2 - mapSize / 4)
                {
                    valid = true;
                    biomCounter[temp] += 1;
                    chunks[i] = new Chunks(mapPrefab2, i - mapSize / 2);
                }
                if (temp == 2)
                {
                    valid = true;
                    biomCounter[temp] += 1;
                    chunks[i] = new Chunks(mapPrefab3, i - mapSize / 2);
                }
                if (temp == 3 && biomCounter[temp] < 1 && i > mapSize/2 - mapSize / 6)
                {
                    valid = true;
                    biomCounter[temp] += 1;
                    chunks[i] = new Chunks(mapPrefab4, i - mapSize / 2);
                }
            } while (!valid);
        }
    }


    void Update()
    {
        IdentifyChunkToRender();
        if(ChunksChanged())
        {
            oldChunksToDelete = lastVisibleChunks.Except(visibleChunks).ToList();
            DeleteOldChunks(oldChunksToDelete);
            RenderNewChunks(newChunksToRender); 
            lastVisibleChunks = visibleChunks.ToList();
        }
    }

    public void DeleteOldChunks(List<int> _chunkNum)
    {
        //Debug.Log("DeleteOldChunks");
        foreach (int chunkNum in _chunkNum)
        {
            int curChunkNum = chunkNum + mapSize / 2;
            Destroy(chunks[curChunkNum].reference);
        }
    }

    public bool ChunksChanged()
    {
        //Debug.Log("ChunksChanged");
        newChunksToRender = visibleChunks.Except(lastVisibleChunks).ToList();
        if (newChunksToRender.Count > 0)
        {
            //Debug.Log("changed");
            return true;
        }
        else
        {
            //Debug.Log("not changed");
            return false;
        }
    }
    public void IdentifyChunkToRender()
    {
        //Debug.Log("IdentifyChunkToRender");
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
        //Debug.Log("------------------------------------------------------------");
        //Debug.Log(player1.curChunk - player2.curChunk);
        //Debug.Log("------------------------------------------------------------");
    }
    public void RenderNewChunks(List<int> _chunkNum)
    {
        //Debug.Log("RenderNewChunks");
        foreach (int chunkNum in _chunkNum)
        {
            int curChunkNum = chunkNum + mapSize / 2;
            chunks[curChunkNum].reference = (GameObject)Instantiate(chunks[curChunkNum].map, new Vector3(chunks[curChunkNum].mXPos * 18,0,0) , transform.rotation);
        }
    }
}
