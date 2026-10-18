using UnityEngine;

public class UICornerMover : MonoBehaviour
{
    public float moveSpeed = 50f; // movement speed in pixels per second

    private RectTransform rectTransform;
    private RectTransform canvasRect;

    private Vector2 targetPosition;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasRect = GetComponentInParent<Canvas>().GetComponent<RectTransform>();

        PickNewCorner();
    }

    void Update()
    {
        MoveToTarget();
    }

    void MoveToTarget()
    {
        rectTransform.anchoredPosition = Vector2.MoveTowards(
            rectTransform.anchoredPosition,
            targetPosition,
            moveSpeed * Time.deltaTime
        );

        // When it reaches the target, choose a new corner
        if (Vector2.Distance(rectTransform.anchoredPosition, targetPosition) < 1f)
        {
            PickNewCorner();
        }
    }

    void PickNewCorner()
    {
        float halfWidth = canvasRect.rect.width / 2f;
        float halfHeight = canvasRect.rect.height / 2f;

        Vector2[] corners = new Vector2[4];

        corners[0] = new Vector2(-halfWidth, -halfHeight); // bottom-left
        corners[1] = new Vector2(halfWidth, -halfHeight);  // bottom-right
        corners[2] = new Vector2(-halfWidth, halfHeight);  // top-left
        corners[3] = new Vector2(halfWidth, halfHeight);   // top-right

        targetPosition = corners[Random.Range(0, corners.Length)];
    }
}
