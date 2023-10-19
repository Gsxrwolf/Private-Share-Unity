using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;
using static worldGenerator;

public class worldGenerator : MonoBehaviour
{
    [SerializeField] private Object mapPrefab;
    [SerializeField] private GameObject p1;
    [SerializeField] private GameObject p2;
    public Player player1;
    public Player player2;
    public Player[] players;

    public class Player
    {
        public GameObject player;
        public float pXPos;
        public GameObject mapL;
        public GameObject mapC;
        public GameObject mapR;
        public GameObject mapTemp;
        public int curChunk;
        public int lastChunk;
        public Vector3 spawn;
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
        StartCopy(player1);
        if (player1.curChunk != player2.curChunk)
        {
            StartCopy(player2);
        }
    }

    public void StartCopy(Player _player)
    {
        _player.pXPos = _player.player.transform.position.x;
        _player.spawn = new Vector2(_player.curChunk * 18, 0);
        _player.mapC = (GameObject)Instantiate(mapPrefab, _player.spawn, transform.rotation);

        _player.curChunk -= 1;
        _player.spawn = new Vector2(_player.curChunk * 18, 0);
        _player.mapL = (GameObject)Instantiate(mapPrefab, _player.spawn, transform.rotation);

        _player.curChunk += 2;
        _player.spawn = new Vector2(_player.curChunk * 18, 0);
        _player.mapR = (GameObject)Instantiate(mapPrefab, _player.spawn, transform.rotation);


        _player.curChunk = Mathf.RoundToInt(_player.pXPos / 18f);
    }

    void Update()
    {
        UpdateCopy(player1);
        if (player1.curChunk != player2.curChunk)
        {
            UpdateCopy(player2);
        }
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
                _player.mapL.transform.position = _player.spawn;

                _player.mapTemp = _player.mapR;
                _player.mapR = _player.mapL;
                _player.mapL = _player.mapC;
                _player.mapC = _player.mapTemp;
            }
            if (_player.curChunk < _player.lastChunk)
            {
                _player.spawn = new Vector3((_player.curChunk * 18) - 18, 0, 0);
                _player.mapR.transform.position = _player.spawn;

                _player.mapTemp = _player.mapL;
                _player.mapL = _player.mapR;
                _player.mapR = _player.mapC;
                _player.mapC = _player.mapTemp;
            }

        }
    }
}
