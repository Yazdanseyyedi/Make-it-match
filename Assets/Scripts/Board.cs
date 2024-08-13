using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public sealed class Board : MonoBehaviour
{

    [SerializeField] GameObject tilePrefab;
    [SerializeField] Gem[] tiles;
    [SerializeField] BoardData boardData;
    [SerializeField] Transform tilesHolder;
    private BackgroundTile[,] allTiles;



    private void Awake()
    {
        allTiles = new BackgroundTile[boardData.Width, boardData.Height];
        boardData.allGems = new Gem[boardData.Width, boardData.Height];
        boardData.startPosition = new Vector2(0, 0);
        boardData.startPosition = tilesHolder.position;
    }
    private void Start()
    {
        Setup();
    }

    private void Setup()
    {
        for (int x = 0; x < boardData.Width; x++)
        {
            for (int y = 0; y < boardData.Height; y++)
            {
                Vector2 tempPosition = new Vector2(45 + boardData.startPosition.x + 45 * x, 45 + boardData.startPosition.y + 45 * y);
                GameObject backgroundTile = Instantiate(tilePrefab, tempPosition, Quaternion.identity, tilesHolder);
                backgroundTile.transform.SetParent(this.transform);
                backgroundTile.name = $"({x},{y})";
                int gemToUse = Random.Range(0, tiles.Length);

                while (MatchesAt(x, y, tiles[gemToUse]))
                {
                    gemToUse = Random.Range(0, tiles.Length);
                }
                Gem gem = Instantiate(tiles[gemToUse], backgroundTile.transform.position, Quaternion.identity);
                gem.gameObject.transform.SetParent(this.transform);
                gem.gameObject.name = backgroundTile.gameObject.name;
                gem.column = x;
                gem.row = y;
                gem.previousColumn = x;
                gem.previousRow = y;
                boardData.allGems[x, y] = gem;
            }
        }
    }

    private bool MatchesAt(int column, int row, Gem gem)
    {
        if (column > 1 && row > 1)
        {

            if (boardData.allGems[column - 1, row].tag == gem.tag && boardData.allGems[column - 2, row].tag == gem.tag)
            {
                return true;
            }
            if (boardData.allGems[column, row - 1].tag == gem.tag && boardData.allGems[column, row - 2].tag == gem.tag)
            {
                return true;
            }
        }
        else if(column <= 1 && row <= 1)
        {
            if(row > 1)
            {
                if (boardData.allGems[column, row - 1].tag == gem.tag && boardData.allGems[column, row - 2].tag == gem.tag)
                {
                    return true;
                }
            }
            if(column > 1)
            {
                if (boardData.allGems[column, row - 1].tag == gem.tag && boardData.allGems[column, row - 2].tag == gem.tag)
                {
                    return true;
                }
            }
        }

        return false;
    }
}
