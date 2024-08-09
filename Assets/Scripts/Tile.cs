using UnityEngine;
using UnityEngine.UI;

public class Tile : MonoBehaviour
{
    public Image icon;
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

    public void OnClick()
    {
        Board.Instance.Select(this);
    }
}
