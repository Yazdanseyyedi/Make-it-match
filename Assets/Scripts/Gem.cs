using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Gem : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] BoardData boardData;
    private Vector2 firstTouchPosition;
    private Vector2 finalTouchPosition;
    public int column;
    public int row;
    public int targetX;
    public int targetY;
    public float SwipAngle = 0;
    private Gem otherGem;
    private Vector2 tempPosition;


    private void Start()
    {
        targetX = (int)transform.position.x;
        targetY = (int)transform.position.y;
    }

    private void Update()
    {
        targetX = 45 + (int)boardData.startPosition.x + 45 * column;
        targetY = 45 + (int)boardData.startPosition.y + 45 * row;
        MoveInXDirection();
        MoveInYDirection();
    }
    private void MoveInXDirection()
    {
        if (Mathf.Abs(targetX - transform.position.x) > 0.1f)
        {
            tempPosition = new Vector2(targetX, transform.position.y);
            transform.position = Vector2.Lerp(transform.position, tempPosition, 0.4f);
        }
        else
        {
            tempPosition = new Vector2(targetX, transform.position.y);
            transform.position = tempPosition;
            boardData.allGems[column,row] = this;
        }
    }

    private void MoveInYDirection()
    {
        if (Mathf.Abs(targetY - transform.position.y) > 0.1f)
        {
            tempPosition = new Vector2(transform.position.x, targetY);
            transform.position = Vector2.Lerp(transform.position, tempPosition, 0.4f);
        }
        else
        {
            tempPosition = new Vector2(transform.position.x, targetY);
            transform.position = tempPosition;
            boardData.allGems[column, row] = this;
        }
    }
    private void CalculateAngle()
    {
        SwipAngle = Mathf.Atan2(finalTouchPosition.y - firstTouchPosition.y, finalTouchPosition.x - firstTouchPosition.x) * 180 / Mathf.PI;
    }

    private void MovePieces()
    {
        Debug.Log(SwipAngle);
        if (SwipAngle > -45 && SwipAngle <= 45 && column < boardData.Width)
        {
            //Right Swip
            otherGem = boardData.allGems[column + 1, row];
            otherGem.column -= 1;
            column += 1;
        }
        else if (SwipAngle > 45 && SwipAngle <= 135 && row < boardData.Height)
        {
            otherGem = boardData.allGems[column , row + 1];
            otherGem.row -= 1;
            row += 1;
        }
        else if (SwipAngle > 135 || SwipAngle <= -135 && column > 0)
        {
            otherGem = boardData.allGems[column - 1, row];
            otherGem.column += 1;
            column -= 1;
        }
        else if (SwipAngle < -45 || SwipAngle >= -135 && row > 0)
        {
            otherGem = boardData.allGems[column, row - 1];
            otherGem.row += 1;
            row -= 1;
        }

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        firstTouchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        finalTouchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        CalculateAngle();
        MovePieces();
    }
}
