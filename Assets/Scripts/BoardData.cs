using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BoardData : ScriptableObject
{
    public Vector2 startPosition;
    public Gem[,] allGems;
    [SerializeField] int width;
    [SerializeField] int height;
    [SerializeField] int offset;

    public int Width => width;
    public int Height => height;
    public int Offset => offset;
}
