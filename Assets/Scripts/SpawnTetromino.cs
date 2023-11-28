using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTetromino : MonoBehaviour
{
    [SerializeField] GameObject tetromino;
    [SerializeField] GameObject tetrominoSpawnPoint;
    private float _currentTime;
    private float _spawnPeriod;
    private float _spawnTime;

    private void Start()
    {
        _currentTime = 0;
        _spawnPeriod = 2;
        _spawnTime = _spawnPeriod;
    }

    private void Update()
    {
        _currentTime += Time.deltaTime;
        if (_currentTime > _spawnTime)
        {
            GameObject newTetromino;

            _spawnTime += _spawnPeriod;
            newTetromino = Instantiate<GameObject>(tetromino);
            newTetromino.transform.parent = this.transform;
            newTetromino.transform.localPosition = tetrominoSpawnPoint.transform.localPosition;
            newTetromino.transform.rotation = Quaternion.Euler(Vector3.zero);
            newTetromino.SetActive(true);
        }
    }
}
