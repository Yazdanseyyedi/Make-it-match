using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tile : MonoBehaviour
{
    [SerializeField] Image icon;
    [SerializeField] Button button;
    private Item item;
    public int x;
    public int y;

    public Item Item
    {
        get { return item; }
        set
        {
            if (item == value)
                return;
            item = value;
            icon.sprite = item.Sprite;
        }
    }
}
