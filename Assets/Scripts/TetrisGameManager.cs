using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrisGameManager : MonoBehaviour
{
    [SerializeField] GameObject[] _tetrominoes;
    [SerializeField] GameObject _tetrominoSpawnPoint;
    [SerializeField] GameObject _fixedTiles;
    [SerializeField] GameObject _waitingTetrominoPoint;
    private GameObject[][] _gameBoard;
    private List<GameObject> _tetrominoSet;
    private float _currentTime;
    private GameObject _waitingTetromino;
    private int _spawnTetrominoCount;
    private GameObject _movingTetromino;
    private float _moveSpeed;
    private float _movePeriod;
    private float _moveTime;
    private int _score;

    private void Start()
    {
        InitGameBoard();
        InitTetrominoSet();
        ShuffleTetrominoSet();
        _currentTime = 0f;
        _spawnTetrominoCount = 0;
        _moveSpeed = 1f;
        _movePeriod = 1 / _moveSpeed;
        _moveTime = _movePeriod;
        _score = 0;
        SetWaitingTetromino();
    }

    private void Update()
    {
        _currentTime += Time.deltaTime;
        if (_currentTime > 1 && _tetrominoSpawnPoint.transform.childCount == 0)
            SpawnNextTetromino();
        MoveControl();
    }

    private void InitGameBoard()
    {
        _gameBoard = new GameObject[20][];
        for (int i = 0; i < 20; i++)
            _gameBoard[i] = new GameObject[10];
    }

    private void InitTetrominoSet()
    {
        _tetrominoSet = new List<GameObject>();
        foreach (GameObject tetromino in _tetrominoes)
            _tetrominoSet.Add(tetromino);
    }

    private void ShuffleTetrominoSet()
    {
        int rand;
        GameObject tmp;

        for (int i = 0; i < _tetrominoSet.Count; i++)
        {
            rand = Random.Range(0, 7);
            tmp = _tetrominoSet[i];
            _tetrominoSet[i] = _tetrominoSet[rand];
            _tetrominoSet[rand] = tmp;
        }
    }

    private void SetWaitingTetromino()
    {
        GameObject newTetromino;

        newTetromino = GetNextTetromino();
        newTetromino.transform.parent = _waitingTetrominoPoint.transform;
        newTetromino.transform.localPosition = Vector3.zero;
        newTetromino.transform.rotation = Quaternion.Euler(Vector3.zero);
        _waitingTetromino = newTetromino;
    }

    private void SpawnNextTetromino()
    {
        _movingTetromino = _waitingTetromino;
        _waitingTetrominoPoint.transform.DetachChildren();
        _movingTetromino.transform.parent = _tetrominoSpawnPoint.transform;
        _movingTetromino.transform.localPosition = Vector3.zero;
        _movingTetromino.transform.rotation = Quaternion.Euler(Vector3.zero);
        _moveTime = _currentTime + _movePeriod;
        SetWaitingTetromino();
    }

    private GameObject GetNextTetromino()
    {
        GameObject newTetromino;

        newTetromino = Instantiate<GameObject>(_tetrominoSet[_spawnTetrominoCount % 7]);
        newTetromino.SetActive(true);
        _spawnTetrominoCount++;
        return newTetromino;
    }

    private void MoveControl()
    {
        if (_currentTime > _moveTime)
        {
            _moveTime += _movePeriod;
            if (NextIsFloor(_movingTetromino))
            {
                List<int> fullLines;

                AttachTetrominoToFloor(_movingTetromino);
                fullLines = CheckFullLine();
                if (fullLines.Count > 0)
                {
                    ScoreUpdate(fullLines.Count);
                    RemoveLines(fullLines);
                }
                SpawnNextTetromino();
            }
            else
                MoveDownTetromino(_movingTetromino);
        }
        if (Input.GetKeyDown(KeyCode.A))
            MoveLeftTetromino(_movingTetromino);
        if (Input.GetKeyDown(KeyCode.D))
            MoveRightTetromino(_movingTetromino);
        if (Input.GetKeyDown(KeyCode.S))
            MoveDownTetromino(_movingTetromino);
        if (Input.GetKeyDown(KeyCode.Q))
            RotateLeftTetromino(_movingTetromino);
        if (Input.GetKeyDown(KeyCode.E))
            RotateRightTetromino(_movingTetromino);
    }

    private void MoveDownTetromino(GameObject tetromino)
    {
        if (!IsMovementPossible(tetromino, 0, -1))
            return;
        tetromino.transform.localPosition += Vector3.down;
    }

    private void MoveLeftTetromino(GameObject tetromino)
    {
        if (!IsMovementPossible(tetromino, -1, 0))
            return;
        tetromino.transform.localPosition += Vector3.left;
    }

    private void MoveRightTetromino(GameObject tetromino)
    {
        if (!IsMovementPossible(tetromino, 1, 0))
            return;
        tetromino.transform.localPosition += Vector3.right;
    }

    private void RotateLeftTetromino(GameObject tetromino)
    {
        tetromino.GetComponent<Tetromino>().RotateLeft();
    }

    private void RotateRightTetromino(GameObject tetromino)
    {
        tetromino.GetComponent<Tetromino>().RotateRight();
    }

    private bool IsMovementPossible(GameObject tetromino, int xMove, int yMove)
    {
        Transform tile;
        int xNext, yNext;

        for (int i = 0; i < 4; i++)
        {
            tile = tetromino.transform.GetChild(i);
            xNext = Mathf.RoundToInt(tile.position.x) + xMove;
            yNext = Mathf.RoundToInt(tile.position.y) + yMove;
            if (xNext < 0 || xNext >= 10 || yNext < 0 || yNext >= 20)
                return false;
            if (_gameBoard[yNext][xNext] != null)
                return false;
        }
        return true;
    }

    private bool NextIsFloor(GameObject tetromino)
    {
        Transform tile;
        int xPos, yPos;

        for (int i = 0; i < 4; i++)
        {
            tile = tetromino.transform.GetChild(i);
            xPos = Mathf.RoundToInt(tile.position.x);
            yPos = Mathf.RoundToInt(tile.position.y);
            if (yPos - 1 < 0 || _gameBoard[yPos - 1][xPos] != null)
                return true;
        }
        return false;
    }

    private void AttachTetrominoToFloor(GameObject tetromino)
    {
        Transform[] tiles;
        int xPos, yPos;

        tiles = new Transform[4];
        for (int i = 0; i < 4; i++)
            tiles[i] = tetromino.transform.GetChild(i);
        tetromino.transform.DetachChildren();
        Destroy(tetromino);
        foreach (Transform tile in tiles)
        {
            xPos = Mathf.RoundToInt(tile.position.x);
            yPos = Mathf.RoundToInt(tile.position.y);
            tile.parent = _fixedTiles.transform;
            _gameBoard[yPos][xPos] = tile.gameObject;
        }
    }

    private List<int> CheckFullLine()
    {
        int count;
        List<int> fullLines;

        fullLines = new List<int>();
        for (int y = 0; y < 20; y++)
        {
            count = 0;
            for (int x = 0; x < 10; x++)
                if (_gameBoard[y][x] != null)
                    count++;
            if (count == 10)
                fullLines.Add(y);
        }
        return fullLines;
    }

    private void ScoreUpdate(int count)
    {
        _score += count * 1000;
        Debug.Log(_score);
    }

    private void RemoveLines(List<int> linesToRemove)
    {
        foreach(int y in linesToRemove)
        {
            for (int x = 0; x < 10; x++)
            {
                Destroy(_gameBoard[y][x]);
                _gameBoard[y][x] = null;
            }
        }
        RemoveLineGap();
    }

    private void RemoveLineGap()
    {
        int count;
        int row;
        GameObject[][] newGameBoard;

        newGameBoard = new GameObject[20][];
        row = 0;
        for (int y = 0; y < 19; y++)
        {
            count = 0;
            for (int x = 0; x < 10; x++)
                if (_gameBoard[y][x] != null)
                    count++;
            if (count > 0)
            {
                newGameBoard[row] = _gameBoard[y];
                for (int x = 0; x < 10; x++)
                {
                    if (newGameBoard[row][x] != null)
                        newGameBoard[row][x].transform.position = new Vector3(newGameBoard[row][x].transform.position.x,
                                                                              row,
                                                                              newGameBoard[row][x].transform.position.z);
                }
                row++;
            }
        }
        while (row < 20)
            newGameBoard[row++] = new GameObject[10];
        _gameBoard = newGameBoard;
    }
}
