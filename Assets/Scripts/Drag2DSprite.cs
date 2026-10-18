using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Drag2DSprite : MonoBehaviour
{
    public LevelHandler LvlHandler;
    public float BumpForce;
    private Vector3 offset;
    private Camera mainCamera;
    private bool isDragging = false;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        HandleInput();
    }

    void HandleInput()
    {
        // Touch input
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector3 touchPos = GetWorldPosition(touch.position);

            if (touch.phase == TouchPhase.Began)
            {
                TryStartDrag(touchPos);
            }
            else if (touch.phase == TouchPhase.Moved && isDragging)
            {
                Drag(touchPos);
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                isDragging = false;
                      //  LvlHandler.DragCountCheck();
            }
        }
        // Mouse input
        else
        {
            Vector3 mousePos = GetWorldPosition(Input.mousePosition);

            if (Input.GetMouseButtonDown(0))
            {
                TryStartDrag(mousePos);
            }
            else if (Input.GetMouseButton(0) && isDragging)
            {
                Drag(mousePos);
            }
            else if (Input.GetMouseButtonUp(0))
            {
                isDragging = false;
                        //LvlHandler.DragCountCheck();
            }
        }
    }

    Vector3 GetWorldPosition(Vector3 screenPosition)
    {
        screenPosition.z = Mathf.Abs(mainCamera.transform.position.z);
        return mainCamera.ScreenToWorldPoint(screenPosition);
    }

    void TryStartDrag(Vector3 inputPosition)
    {
        Collider2D hit = Physics2D.OverlapPoint(inputPosition);

        if (hit != null && hit.gameObject == gameObject)
        {
            isDragging = true;
            offset = transform.position - inputPosition;
        }
    }

    void Drag(Vector3 inputPosition)
    {
        transform.position = inputPosition + offset;


    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Ball"))
        {
            GetComponent<Animator>().SetTrigger("Bump");
        }
    }
}
