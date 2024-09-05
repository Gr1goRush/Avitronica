using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public delegate void MovePointerAction(Vector3 offsetFromCenter, float speed);

public class MovePointer : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private RectTransform rectTransform;

    private bool isDragging = false;
    private Vector2 lastPointerPosition;
    private Vector3 startLocalPosition, lastLocalPosition;

    private Camera mainCamera;

    public event MovePointerAction OnWorldOffsetChanged;

    private void Start()
    {
        mainCamera = Camera.main;

        startLocalPosition = rectTransform.localPosition;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isDragging = true;
        lastPointerPosition = ConvertTouchPositionToPointerPosition(GetTouchPosition());
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isDragging = false;
    }

    void Update()
    {
        if(isDragging)
        {
            UpdateDrag();
        }
    }

    private void UpdateDrag()
    {
        Vector2 touchPos = GetTouchPosition();
        Vector2 newPosition = ConvertTouchPositionToPointerPosition(touchPos);
        Vector2 offset = newPosition - lastPointerPosition;
        lastPointerPosition = newPosition;

        rectTransform.localPosition += new Vector3(offset.x, offset.y, 0f);

        Vector3 newCanvasSpacePosition = rectTransform.parent.TransformPoint(rectTransform.localPosition);
        Vector3 startCanvasSpacePosition = rectTransform.parent.TransformPoint(startLocalPosition);

        float speed = Vector3.Distance(newCanvasSpacePosition, rectTransform.parent.TransformPoint(lastLocalPosition));
        lastLocalPosition = rectTransform.localPosition;

        OnWorldOffsetChanged?.Invoke(newCanvasSpacePosition - startCanvasSpacePosition, speed);
    }

    private static Vector2 GetTouchPosition()
    {
        if (Input.touchSupported)
        {
            return Input.GetTouch(0).position;
        }
        else
        {
            return Input.mousePosition;
        }
    }

    public Vector2 ConvertTouchPositionToPointerPosition(Vector2 pos)
    {
        RectTransform targetParent = rectTransform.parent as RectTransform;

        if(RectTransformUtility.ScreenPointToLocalPointInRectangle(targetParent, pos, mainCamera, out Vector2 localPoint))
        {
            return localPoint;
        }

        return pos;
    }
}
