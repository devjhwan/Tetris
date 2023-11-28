using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tetromino : MonoBehaviour
{
    [SerializeField] TetrominoType tetrominoType;
    private GameObject[] _tiles;
    private int _state;

    private enum TetrominoType
    {
        I, L, J, T, O, S, Z
    }

    private void Start()
    {
        _tiles = new GameObject[4];
        for (int i = 0; i < 4; i++)
            _tiles[i] = this.transform.GetChild(i).gameObject;

        _state = 0;
    }

    public void RotateLeft()
    {
        _state = (_state - 1) >= 0 ? _state - 1 : 3;
        TileStateUpdate();
    }

    public void RotateRight()
    {
        _state = (_state + 1) % 4;
        TileStateUpdate();
    }

    private void TileStateUpdate()
    {
        switch (tetrominoType)
        {
            case TetrominoType.I:
                ITileStateUpdate();
                break;
            case TetrominoType.J:
                JTileStateUpdate();
                break;
            case TetrominoType.L:
                LTileStateUpdate();
                break;
            case TetrominoType.T:
                TTileStateUpdate();
                break;
            case TetrominoType.O:
                OTileStateUpdate();
                break;
            case TetrominoType.S:
                STileStateUpdate();
                break;
            case TetrominoType.Z:
                ZTileStateUpdate();
                break;
        }
    }
    private void ITileStateUpdate()
    {
        switch (_state)
        {
            case 0:
            case 2:
                _tiles[0].transform.localPosition = new Vector3(-2, 0, 0);
                _tiles[1].transform.localPosition = new Vector3(-1, 0, 0);
                _tiles[2].transform.localPosition = new Vector3(0, 0, 0);
                _tiles[3].transform.localPosition = new Vector3(1, 0, 0);
                break;
            case 1:
            case 3:
                _tiles[0].transform.localPosition = new Vector3(0, 1, 0);
                _tiles[1].transform.localPosition = new Vector3(0, 0, 0);
                _tiles[2].transform.localPosition = new Vector3(0, -1, 0);
                _tiles[3].transform.localPosition = new Vector3(0, -2, 0);
                break;
            default:
                Debug.LogError("Impossible state: " + _state.ToString(), this);
                break;
        }
    }
    private void JTileStateUpdate()
    {
        switch (_state)
        {
            case 0:
                _tiles[0].transform.localPosition = new Vector3(-1, 0, 0);
                _tiles[1].transform.localPosition = new Vector3(0, 0, 0);
                _tiles[2].transform.localPosition = new Vector3(1, 0, 0);
                _tiles[3].transform.localPosition = new Vector3(1, -1, 0);
                break;
            case 1:
                _tiles[0].transform.localPosition = new Vector3(0, 1, 0);
                _tiles[1].transform.localPosition = new Vector3(0, 0, 0);
                _tiles[2].transform.localPosition = new Vector3(0, -1, 0);
                _tiles[3].transform.localPosition = new Vector3(-1, -1, 0);
                break;
            case 2:
                _tiles[0].transform.localPosition = new Vector3(-1, 0, 0);
                _tiles[1].transform.localPosition = new Vector3(-1, -1, 0);
                _tiles[2].transform.localPosition = new Vector3(0, -1, 0);
                _tiles[3].transform.localPosition = new Vector3(1, -1, 0);
                break;
            case 3:
                _tiles[0].transform.localPosition = new Vector3(0, 1, 0);
                _tiles[1].transform.localPosition = new Vector3(1, 1, 0);
                _tiles[2].transform.localPosition = new Vector3(0, 0, 0);
                _tiles[3].transform.localPosition = new Vector3(0, -1, 0);
                break;
            default:
                Debug.LogError("Impossible state: " + _state.ToString(), this);
                break;
        }
    }
    private void LTileStateUpdate()
    {
        switch (_state)
        {
            case 0:
                _tiles[0].transform.localPosition = new Vector3(-1, 0, 0);
                _tiles[1].transform.localPosition = new Vector3(0, 0, 0);
                _tiles[2].transform.localPosition = new Vector3(1, 0, 0);
                _tiles[3].transform.localPosition = new Vector3(-1, -1, 0);
                break;
            case 1:
                _tiles[0].transform.localPosition = new Vector3(-1, 1, 0);
                _tiles[1].transform.localPosition = new Vector3(0, 1, 0);
                _tiles[2].transform.localPosition = new Vector3(0, 0, 0);
                _tiles[3].transform.localPosition = new Vector3(0, -1, 0);
                break;
            case 2:
                _tiles[0].transform.localPosition = new Vector3(1, 0, 0);
                _tiles[1].transform.localPosition = new Vector3(-1, -1, 0);
                _tiles[2].transform.localPosition = new Vector3(0, -1, 0);
                _tiles[3].transform.localPosition = new Vector3(1, -1, 0);
                break;
            case 3:
                _tiles[0].transform.localPosition = new Vector3(0, 1, 0);
                _tiles[1].transform.localPosition = new Vector3(0, 0, 0);
                _tiles[2].transform.localPosition = new Vector3(0, -1, 0);
                _tiles[3].transform.localPosition = new Vector3(1, -1, 0);
                break;
            default:
                Debug.LogError("Impossible state: " + _state.ToString(), this);
                break;
        }
    }
    private void TTileStateUpdate()
    {
        switch (_state)
        {
            case 0:
                _tiles[0].transform.localPosition = new Vector3(-1, 0, 0);
                _tiles[1].transform.localPosition = new Vector3(0, 0, 0);
                _tiles[2].transform.localPosition = new Vector3(1, 0, 0);
                _tiles[3].transform.localPosition = new Vector3(0, -1, 0);
                break;
            case 1:
                _tiles[0].transform.localPosition = new Vector3(0, 1, 0);
                _tiles[1].transform.localPosition = new Vector3(-1, 0, 0);
                _tiles[2].transform.localPosition = new Vector3(0, 0, 0);
                _tiles[3].transform.localPosition = new Vector3(0, -1, 0);
                break;
            case 2:
                _tiles[0].transform.localPosition = new Vector3(0, 0, 0);
                _tiles[1].transform.localPosition = new Vector3(-1, -1, 0);
                _tiles[2].transform.localPosition = new Vector3(0, -1, 0);
                _tiles[3].transform.localPosition = new Vector3(1, -1, 0);
                break;
            case 3:
                _tiles[0].transform.localPosition = new Vector3(0, 1, 0);
                _tiles[1].transform.localPosition = new Vector3(0, 0, 0);
                _tiles[2].transform.localPosition = new Vector3(1, 0, 0);
                _tiles[3].transform.localPosition = new Vector3(0, -1, 0);
                break;
            default:
                Debug.LogError("Impossible state: " + _state.ToString(), this);
                break;
        }
    }
    private void OTileStateUpdate()
    {
        switch (_state)
        {
            case 0:
            case 1:
            case 2:
            case 3:
                _tiles[0].transform.localPosition = new Vector3(-1, 0, 0);
                _tiles[1].transform.localPosition = new Vector3(0, 0, 0);
                _tiles[2].transform.localPosition = new Vector3(-1, -1, 0);
                _tiles[3].transform.localPosition = new Vector3(0, -1, 0);
                break;
            default:
                Debug.LogError("Impossible state: " + _state.ToString(), this);
                break;
        }
    }
    private void STileStateUpdate()
    {
        switch (_state)
        {
            case 0:
            case 2:
                _tiles[0].transform.localPosition = new Vector3(0, 0, 0);
                _tiles[1].transform.localPosition = new Vector3(1, 0, 0);
                _tiles[2].transform.localPosition = new Vector3(-1, -1, 0);
                _tiles[3].transform.localPosition = new Vector3(0, -1, 0);
                break;
            case 1:
            case 3:
                _tiles[0].transform.localPosition = new Vector3(-1, 1, 0);
                _tiles[1].transform.localPosition = new Vector3(-1, 0, 0);
                _tiles[2].transform.localPosition = new Vector3(0, 0, 0);
                _tiles[3].transform.localPosition = new Vector3(0, -1, 0);
                break;
            default:
                Debug.LogError("Impossible state: " + _state.ToString(), this);
                break;
        }
    }
    private void ZTileStateUpdate()
    {
        switch (_state)
        {
            case 0:
            case 2:
                _tiles[0].transform.localPosition = new Vector3(-1, 0, 0);
                _tiles[1].transform.localPosition = new Vector3(0, 0, 0);
                _tiles[2].transform.localPosition = new Vector3(0, -1, 0);
                _tiles[3].transform.localPosition = new Vector3(1, -1, 0);
                break;
            case 1:
            case 3:
                _tiles[0].transform.localPosition = new Vector3(1, 1, 0);
                _tiles[1].transform.localPosition = new Vector3(0, 0, 0);
                _tiles[2].transform.localPosition = new Vector3(1, 0, 0);
                _tiles[3].transform.localPosition = new Vector3(0, -1, 0);
                break;
            default:
                Debug.LogError("Impossible state: " + _state.ToString(), this);
                break;
        }
    }
}
