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
    public Player[] players;
    public Object[] maps;

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

    void Start()
    {
        player1 = new Player(p1, 0f);
        player2 = new Player(p2, 0f);
        for (int i = 0; i < mapSize; i++)
        {
            if (i / 3 == 1)
            {
                maps[i] = mapPrefab1;
            }
            if (i / 3 == 2)
            {
                maps[i] = mapPrefab1;
            }
            if (i / 3 == 3)
            {
                maps[i] = mapPrefab1;
            }
        }
    }


    void Update()
    {

    }

    public void RenderChunks(int[] _chunkNum)
    {
        player1.pXPos = player1.player.transform.position.x;
        player1.curChunk = Mathf.RoundToInt(player1.pXPos / 18f);
        player2.pXPos = player2.player.transform.position.x;
        player2.curChunk = Mathf.RoundToInt(player2.pXPos / 18f);
    }

    public void UpdateCopy(Player _player)
    {
        _player.lastChunk = _player.curChunk;
        _player.pXPos = _player.player.transform.position.x;
        _player.curChunk = Mathf.RoundToInt(_player.pXPos / 18f);





        if (_player.curChunk != _player.lastChunk)
        {
            if (_player.curChunk > _player.lastChunk)
            {
                _player.spawn = new Vector3((_player.curChunk * 18) + 18, 0, 0);
                Destroy(_player.mapL);
                _player.mapL = _player.mapC;
                _player.mapC = _player.mapR;
                _player.mapR = (GameObject)Instantiate(mapPrefab, _player.spawn, transform.rotation);
            }
            if (_player.curChunk < _player.lastChunk)
            {
                _player.spawn = new Vector3((_player.curChunk * 18) - 18, 0, 0);
                Destroy(_player.mapR);
                _player.mapR = _player.mapC;
                _player.mapC = _player.mapL;
                _player.mapL = (GameObject)Instantiate(mapPrefab, _player.spawn, transform.rotation);
            }

        }
    }
}
