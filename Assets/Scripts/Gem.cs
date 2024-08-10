using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Gem : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private Vector2 firstTouchPosition;
    private Vector2 finalTouchPosition;
    public float SwipAngle = 0;

    private void CalculateAngle()
    {
        SwipAngle = Mathf.Atan2(finalTouchPosition.y - firstTouchPosition.y, finalTouchPosition.x - firstTouchPosition.x) * 180 / Mathf.PI;
        Debug.Log(SwipAngle);
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
