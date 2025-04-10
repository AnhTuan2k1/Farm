using UnityEngine;

public class SmoothCameraDragXY : MonoBehaviour
{
    public float dragSpeed = 1f;
    public float smoothTime = 0.2f;

    public Vector2 minPosition = new Vector2(-3f, -4.5f);
    public Vector2 maxPosition = new Vector2(3f, 4.5f);

    private Vector3 dragOrigin;
    private Vector3 targetPosition;
    private Vector3 velocity = Vector3.zero;
    private bool isDragging = false;

    void Start()
    {
        targetPosition = transform.position;
    }

    void Update()
    {
        if(InventoryCanvas.Instance.gameObject.activeSelf || ShopCanvas.Instance.gameObject.activeSelf)
        {
            return;
        }

#if UNITY_EDITOR || UNITY_STANDALONE
        HandleMouseDrag();
#else
        HandleTouchDrag();
#endif

        // Di chuyển camera mượt tới vị trí đích
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }

    void HandleMouseDrag()
    {
        if (Input.GetMouseButtonDown(0))
        {
            dragOrigin = Input.mousePosition;
            isDragging = true;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }

        if (isDragging)
        {
            Vector3 difference = Input.mousePosition - dragOrigin;
            Vector3 move = new Vector3(-difference.x, -difference.y, 0f) * dragSpeed * Time.deltaTime;
            UpdateTargetPosition(move);
            dragOrigin = Input.mousePosition;
        }
    }

    void HandleTouchDrag()
    {
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                dragOrigin = touch.position;
                isDragging = true;
            }
            else if (touch.phase == TouchPhase.Moved && isDragging)
            {
                Vector3 difference = (Vector3)touch.position - dragOrigin;
                Vector3 move = new Vector3(-difference.x, -difference.y, 0f) * dragSpeed * Time.deltaTime;
                UpdateTargetPosition(move);
                dragOrigin = touch.position;
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                isDragging = false;
            }
        }
    }

    void UpdateTargetPosition(Vector3 move)
    {
        Vector3 newTarget = targetPosition + move;

        newTarget.x = Mathf.Clamp(newTarget.x, minPosition.x, maxPosition.x);
        newTarget.y = Mathf.Clamp(newTarget.y, minPosition.y, maxPosition.y);

        targetPosition = newTarget;
    }
}