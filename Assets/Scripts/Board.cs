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
    private BackgroundTile[,] allTiles;

    public int Width => width;
    public int Height => height;

    private void Start()
    {
        allTiles = new BackgroundTile[width, height];
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
                backgroundTile.transform.parent = this.transform;
                backgroundTile.name = $"({x},{y})";
            }
        }
    }
}
