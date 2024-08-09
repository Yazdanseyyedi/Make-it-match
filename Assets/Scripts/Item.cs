using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Item")]
public sealed class Item : ScriptableObject
{
    [SerializeField] int value;
    [SerializeField] Sprite sprite;

    public int Value => value;
    public Sprite Sprite => sprite;
}
