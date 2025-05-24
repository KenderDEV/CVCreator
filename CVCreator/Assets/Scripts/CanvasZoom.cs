using UnityEngine;
using UnityEngine.EventSystems;

public class CanvasZoom : MonoBehaviour, IScrollHandler
{
    [SerializeField] private float zoomSpeed = 0.1f;
    [SerializeField] private float minZoom = 0.5f;
    [SerializeField] private float maxZoom = 2f;

    RectTransform rectTransform;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void OnScroll(PointerEventData eventData)
    {
        float scale = Mathf.Clamp(rectTransform.localScale.x + eventData.scrollDelta.y * zoomSpeed, minZoom, maxZoom);
        rectTransform.localScale = new Vector3(scale, scale, 1f);
    }
}
