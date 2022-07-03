using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Drag : MonoBehaviour, IDragHandler, IPointerDownHandler
{
    private Vector2 _dragOffSet;
    private Camera _camera;
    
    void Awake()
    {
        _camera = Camera.main;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        _dragOffSet = (Vector2)transform.position - GetTouchPositon(eventData);
    }
    
    public void OnDrag(PointerEventData eventData)
    {
        Vector2 fingPos = GetTouchPositon(eventData);
        transform.position = Vector2.MoveTowards(transform.position, fingPos + _dragOffSet, Mathf.Infinity);
    }

    // Update is called once per frame
    Vector2 GetTouchPositon(PointerEventData eventData)
    {
        Vector2 touchPos = _camera.ScreenToWorldPoint(eventData.position);
        return touchPos;
    }
}
