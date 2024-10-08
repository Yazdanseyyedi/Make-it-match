using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Gem : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] BoardData boardData;
    [SerializeField] BoardEventHandler boardEventHandler;
    private Vector2 firstTouchPosition;
    private Vector2 finalTouchPosition;
    public int column;
    public int row;
    public int previousColumn;
    public int previousRow;
    public int targetX;
    public int targetY;
    public bool isMatched;
    public float SwipAngle = 0;
    private Gem otherGem;
    private Vector2 tempPosition;



    private void Update()
    {
        FindMatches();
        PopGem();
        targetX = 45 + (int)boardData.startPosition.x + 45 * column;
        targetY = 45 + (int)boardData.startPosition.y + 45 * row;
        MoveInXDirection();
        MoveInYDirection();
    }

    private void PopGem()
    {
        if (isMatched)
        {
            Image gemImage = GetComponent<Image>();
            gemImage.color = new Color(0f, 0f, 0f, 0.6f);
            boardEventHandler.RaiseDestroyMatchesAction();
        }
    }

    private void MoveInXDirection()
    {
        if (Mathf.Abs(targetX - transform.position.x) > 0.1f)
        {
            tempPosition = new Vector2(targetX, transform.position.y);
            transform.position = Vector2.Lerp(transform.position, tempPosition, 0.4f);
            if (boardData.allGems[column, row] != this)
            {
                boardData.allGems[column, row] = this;
            }
        }
        else
        {
            tempPosition = new Vector2(targetX, transform.position.y);
            transform.position = tempPosition;
        }
    }

    private void MoveInYDirection()
    {
        if (Mathf.Abs(targetY - transform.position.y) > 0.1f)
        {
            tempPosition = new Vector2(transform.position.x, targetY);
            transform.position = Vector2.Lerp(transform.position, tempPosition, 0.4f);
            if (boardData.allGems[column, row] != this)
            {
                boardData.allGems[column, row] = this;
            }
        }
        else
        {
            tempPosition = new Vector2(transform.position.x, targetY);
            transform.position = tempPosition;
        }
    }
    private void CalculateAngle()
    {
        if (Mathf.Abs(finalTouchPosition.y - firstTouchPosition.y) > 1f || Mathf.Abs(finalTouchPosition.x - firstTouchPosition.x) > 1f)
        {
            SwipAngle = Mathf.Atan2(finalTouchPosition.y - firstTouchPosition.y, finalTouchPosition.x - firstTouchPosition.x) * 180 / Mathf.PI;
            MovePieces();
        }
    }

    private void MovePieces()
    {
        Debug.Log(SwipAngle);
        if ((SwipAngle > -45 && SwipAngle <= 45) && column < boardData.Width - 1)
        {
            otherGem = boardData.allGems[column + 1, row];
            previousRow = row;
            previousColumn = column;
            otherGem.column -= 1;
            column += 1;
        }
        else if ((SwipAngle > 45 && SwipAngle <= 135) && row < boardData.Height - 1)
        {
            otherGem = boardData.allGems[column, row + 1];
            previousRow = row;
            previousColumn = column;
            otherGem.row -= 1;
            row += 1;
        }
        else if ((SwipAngle > 135 || SwipAngle <= -135) && column > 0)
        {
            otherGem = boardData.allGems[column - 1, row];
            previousRow = row;
            previousColumn = column;
            otherGem.column += 1;
            column -= 1;
        }
        else if ((SwipAngle < -45 && SwipAngle >= -135) && row > 0)
        {
            otherGem = boardData.allGems[column, row - 1];
            previousRow = row;
            previousColumn = column;
            otherGem.row += 1;
            row -= 1;
        }
        StartCoroutine(CheckMove());

    }

    private void FindMatches()
    {
        if (column > 0 && column < boardData.Width - 1)
        {
            Gem leftGem = boardData.allGems[column - 1, row];
            Gem rightGem = boardData.allGems[column + 1, row];
            if (leftGem != null && rightGem != null)
            {
                if (leftGem.tag == this.tag && rightGem.tag == this.tag)
                {
                    leftGem.isMatched = true;
                    rightGem.isMatched = true;
                    isMatched = true;
                }
            }

        }
        if (row > 0 && row < boardData.Height - 1)
        {
            Gem downGem = boardData.allGems[column, row - 1];
            Gem upGem = boardData.allGems[column, row + 1];
            if (downGem != null && upGem != null)
            {
                if (downGem.tag == this.tag && upGem.tag == this.tag)
                {
                    downGem.isMatched = true;
                    upGem.isMatched = true;
                    isMatched = true;
                }
            }

        }
    }

    public IEnumerator CheckMove()
    {
        yield return new WaitForSeconds(0.5f);
        if (otherGem != null)
        {
            if (!isMatched && !otherGem.isMatched)
            {
                otherGem.row = row;
                otherGem.column = column;
                row = previousRow;
                column = previousColumn;
            }
            else
                boardEventHandler.RaiseDestroyMatchesAction();
            otherGem = null;
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
    }
}
