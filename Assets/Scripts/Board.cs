using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public sealed class Board : MonoBehaviour
{
    public static Board Instance { get; private set; }
    public Tile[,] Tiles { get; private set; }

    public Row[] rows;

    [SerializeField] ItemDataBase itemDataBase;
    [SerializeField] int width;
    [SerializeField] int height;

    public int Width => width;
    public int Height => height;

    private List<Tile> selectedTiles = new();

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        SetBoard();
    }

    private void SetBoard()
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

                tile.Item = itemDataBase.Items[Random.Range(0, itemDataBase.Items.Length)];
            }
        }
    }

    public async void Select(Tile tile)
    {
        if (!selectedTiles.Contains(tile))
            selectedTiles.Add(tile);

        if (selectedTiles.Count < 2)
            return;

        Debug.Log($"Tile0:({selectedTiles[0].x} , {selectedTiles[0].y}) , Tile1:({selectedTiles[1].x} , {selectedTiles[1].y})");

        await Swap(selectedTiles[0], selectedTiles[1]);

        if (CanPop())
        {
            Pop();
        }
        else
        {
            await Swap(selectedTiles[0], selectedTiles[1]);
        }
        selectedTiles.Clear();
    }

    public async Task Swap(Tile tile1, Tile tile2)
    {
        var icon1 = tile1.icon;
        var icon2 = tile2.icon;

        var icon1Transform = icon1.transform;
        var icon2Transform = icon2.transform;

        var sequence = DOTween.Sequence();
        sequence.Join(icon1Transform.DOMove(icon2Transform.position, 0.25f));
        sequence.Join(icon2Transform.DOMove(icon1Transform.position, 0.25f));
        await sequence.Play().AsyncWaitForCompletion();

        icon1Transform.SetParent(tile2.transform);
        icon2Transform.SetParent(tile1.transform);

        tile1.icon = icon2;
        tile2.icon = icon1;

        var tile1Item = tile1.Item;
        tile1.Item = tile2.Item;
        tile2.Item = tile1Item;
    }

    private bool CanPop()
    {
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                if (Tiles[x, y].LeftToRightConnectedTiles().Count > 2 || Tiles[x, y].UpToDownConnectedTiles().Count > 2)
                {
                    return true;
                }
            }
        }
        return false;
    }

    private async void Pop()
    {
        var deflateSequence = DOTween.Sequence();

        for (var y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                var tile = Tiles[x, y];

                var connectedUpToDownTiles = tile.UpToDownConnectedTiles();
                var connectedLeftToRightTiles = tile.LeftToRightConnectedTiles();

                if (connectedLeftToRightTiles.Count >= 3)
                {
                    foreach (var connectedTile in connectedLeftToRightTiles)
                    {
                        deflateSequence.Join(connectedTile.icon.transform.DOScale(Vector3.zero, 0.25f));
                    }

                }

                if (connectedUpToDownTiles.Count >= 3)
                {
                    foreach (var connectedTile in connectedUpToDownTiles)
                    {
                        deflateSequence.Join(connectedTile.icon.transform.DOScale(Vector3.zero, 0.25f));
                    }

                }

            }
        }
        await deflateSequence.Play().AsyncWaitForCompletion();
    }
}
