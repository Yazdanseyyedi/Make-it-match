using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public sealed class Board : MonoBehaviour
{
    [SerializeField] ItemDataBase itemDataBase;
    [SerializeField] int Width;
    [SerializeField] int Height;
    public static Board Instance { get; private set; }
    public Tile[,] Tiles { get; private set; }

    public Row[] rows;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Tiles = new Tile[rows.Max(row => row.Tiles.Length), rows.Length];

        for (int x = 0; x < Height; x++)
        {
            for (int y = 0; y < Width; y++)
            {
                var tile = rows[y].Tiles[x];

                tile.x = x;
                tile.y = y;

                Tiles[x, y] = tile;

                tile.Item = itemDataBase.Items[Random.Range(0,itemDataBase.Items.Length)];
            }
        }
    }
}
