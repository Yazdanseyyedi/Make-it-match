using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
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

    public Tile Left => x > 0 ? Board.Instance.Tiles[x - 1, y] : null;
    public Tile Right => x < Board.Instance.Width - 1 ? Board.Instance.Tiles[x + 1, y] : null;
    public Tile Top => y > 0 ? Board.Instance.Tiles[x, y - 1] : null;
    public Tile Bottom => y < Board.Instance.Height - 1 ? Board.Instance.Tiles[x, y + 1] : null;

    public List<Tile> LeftToRightConnectedTiles()
    {
        List<Tile> result = new();
        var leftConnectedTile = GetLeftConnectedTile();
        var rightConnectedTile = GetRightConnectedTile();
        result = rightConnectedTile;
        foreach (var item in leftConnectedTile)
        {
            if(!rightConnectedTile.Contains(item))
                result.Add(item);
        }
        return result;
    }

    public List<Tile> UpToDownConnectedTiles()
    {
        List<Tile> result = new();
        var topConnectedTile = GetTopConnectedTile();
        var bottomConnectedTile = GetBottomConnectedTile();
        result = topConnectedTile;
        foreach (var item in bottomConnectedTile)
        {
            if (!topConnectedTile.Contains(item))
                result.Add(item);
        }
        return result;
    }

    public List<Tile> GetConnectedTiles()
    {
        List<Tile> result = new();
        result.AddRange(GetLeftConnectedTile());
        result.AddRange(GetRightConnectedTile());
        result.AddRange(GetTopConnectedTile());
        result.AddRange(GetBottomConnectedTile());
        return result;
    }

    public List<Tile> GetLeftConnectedTile(List<Tile> exclude = null)
    {
        if (exclude == null)
            exclude = new List<Tile> { this };
        else
            exclude.Add(this);

        if(Left == null || exclude.Contains(Left) || Left.Item != Item)
            return exclude;
        else
        {
            exclude.AddRange(Left.GetLeftConnectedTile(exclude));
            return exclude;
        }
    }

    public List<Tile> GetRightConnectedTile(List<Tile> exclude = null)
    {
        if (exclude == null)
            exclude = new List<Tile> { this };
        else
            exclude.Add(this);

        if (Right == null || exclude.Contains(Right) || Right.Item != Item)
            return exclude;
        else
        {
            exclude.AddRange(Right.GetRightConnectedTile(exclude));
            return exclude;
        }
    }

    public List<Tile> GetTopConnectedTile(List<Tile> exclude = null)
    {
        if (exclude == null)
            exclude = new List<Tile> { this };
        else
            exclude.Add(this);

        if (Top == null || exclude.Contains(Top) || Top.Item != Item)
            return exclude;
        else
        {
            exclude.AddRange(Top.GetTopConnectedTile(exclude));
            return exclude;
        }
    }

    public List<Tile> GetBottomConnectedTile(List<Tile> exclude = null)
    {
        if (exclude == null)
            exclude = new List<Tile> { this };
        else
            exclude.Add(this);

        if (Bottom == null || exclude.Contains(Bottom) || Bottom.Item != Item)
            return exclude;
        else
        {
            exclude.AddRange(Bottom.GetBottomConnectedTile(exclude));
            return exclude;
        }
    }
}
