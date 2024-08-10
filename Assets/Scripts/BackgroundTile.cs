using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundTile : MonoBehaviour
{
    [SerializeField] GameObject[] tiles;
    private void Start()
    {
        Initialize();
    }
    private void Initialize()
    {
        int tileToUse = Random.Range(0, tiles.Length);
        GameObject tile = Instantiate(tiles[tileToUse], transform.position, Quaternion.identity);
        tile.transform.parent = this.transform;
        tile.name = this.gameObject.name;
    }
}
