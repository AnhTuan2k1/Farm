using UnityEngine;

public class ClickHandler : MonoBehaviour
{
    Camera cam;
    Vector2 startPos;
    float tapThreshold = 20f; // Pixel threshold để phân biệt tap và kéo

    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
#if UNITY_EDITOR || UNITY_STANDALONE
        if (Input.GetMouseButtonDown(0))
        {
            startPos = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0))
        {
            Vector2 endPos = Input.mousePosition;
            if (Vector2.Distance(startPos, endPos) < tapThreshold)
            {
                HandleClick(endPos);
            }
        }

#elif UNITY_ANDROID || UNITY_IOS
        if (Input.touchCount > 0)
        {
            Touch touch = Input.touches[0];

            if (touch.phase == TouchPhase.Began)
            {
                startPos = touch.position;
            }

            if (touch.phase == TouchPhase.Ended)
            {
                Vector2 endPos = touch.position;
                if (Vector2.Distance(startPos, endPos) < tapThreshold)
                {
                    HandleClick(endPos);
                }
            }
        }
#endif
    }

    void HandleClick(Vector3 inputPosition)
    {
        Ray ray = cam.ScreenPointToRay(inputPosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            //Debug.Log("Tapped on: " + hit.collider.name);

            var clickable = hit.collider.GetComponent<IClickable>();
            if (clickable != null)
            {
                clickable.OnClick();
            }
        }
    }
}
