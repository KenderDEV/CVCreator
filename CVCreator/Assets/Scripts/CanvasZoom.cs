using UnityEngine;
using UnityEngine.EventSystems;

public class CanvasZoom : MonoBehaviour, IScrollHandler
{
    [SerializeField] private float zoomSpeed = 0.1f;
    [SerializeField] private float minZoom = 0.5f;
    [SerializeField] private float maxZoom = 2f;

    [SerializeField] private RectTransform canvas;
    [SerializeField] private Camera cam;

    RectTransform rectTransform;

    void Start()
    {
        if (canvas != null)
        {
            canvas.sizeDelta = new Vector2(cam.pixelWidth, cam.pixelHeight);
            canvas.anchoredPosition = Vector2.zero;
        }
        rectTransform = GetComponent<RectTransform>();
    }

    public void OnScroll(PointerEventData eventData)
    {
        float scale = Mathf.Clamp(rectTransform.localScale.x + eventData.scrollDelta.y * zoomSpeed, minZoom, maxZoom);
        rectTransform.localScale = new Vector3(scale, scale, 1f);
    }
}
