using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Row : MonoBehaviour
{
    [SerializeField] Tile[] tiles;

    public Tile[] Tiles => tiles;
}
