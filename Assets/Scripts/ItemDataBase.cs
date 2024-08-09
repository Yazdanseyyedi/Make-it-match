using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDataBase : ScriptableObject
{
    [SerializeField] Item[] items;

    public Item[] Items => items;
}
