using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Joystick : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] RectTransform thumbStickTrans;
    [SerializeField] RectTransform backgroundTrans;
    [SerializeField] RectTransform centerTrans;

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log($"On Drag {eventData.position}");
        Vector2 touchPos = eventData.position;
        Vector2 centerPos = backgroundTrans.position;

        Vector2 localOffset = Vector2.ClampMagnitude(touchPos - centerPos, backgroundTrans.sizeDelta.x / 2);
        thumbStickTrans.position = centerPos + localOffset;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        backgroundTrans.position = eventData.position;
        thumbStickTrans.position = eventData.position;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        backgroundTrans.position = centerTrans.position;
        thumbStickTrans.position = backgroundTrans.position;
    }

    void Start()
    {
        
    }


    void Update()
    {
        
    }
}
