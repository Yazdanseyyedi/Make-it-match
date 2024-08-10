using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public sealed class Board : MonoBehaviour
{
    [SerializeField] int width;
    [SerializeField] int height;
    [SerializeField] GameObject tilePrefab;
    [SerializeField] Transform tilesHolder;
    [SerializeField] GameObject[] tiles;
    private BackgroundTile[,] allTiles;
    public GameObject[,] allGems;

    public int Width => width;
    public int Height => height;

    private void Start()
    {
        allTiles = new BackgroundTile[width, height];
        allGems = new GameObject[width, height];
        Setup();
    }

    private void Setup()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Vector2 tempPosition = new Vector2(22+ tilesHolder.position.x + 45 * x,22 + tilesHolder.position.y + 45 * y);
                GameObject backgroundTile = Instantiate(tilePrefab, tempPosition, Quaternion.identity,tilesHolder);
                backgroundTile.transform.SetParent(this.transform);
                backgroundTile.name = $"({x},{y})";
                int gemToUse = Random.Range(0, tiles.Length);
                GameObject gem = Instantiate(tiles[gemToUse], backgroundTile.transform.position, Quaternion.identity);
                gem.transform.SetParent(this.transform);
                gem.name = backgroundTile.gameObject.name;
                allGems[x, y] = gem;
            }
        }
    }
}
