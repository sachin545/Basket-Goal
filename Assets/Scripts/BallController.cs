using System.Collections;
using UnityEngine;

public class BallController : MonoBehaviour
{
    private float bumpForce = 10f; // Adjustable bump force

    private Rigidbody2D rb;


    Vector2 StartPos;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        StartPos = transform.position;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Spring"))
        {
            if (rb != null)
            {
                // Get bump force from spring
                bumpForce = other.gameObject
                    .GetComponent<Drag2DSprite>().BumpForce;

                // Get collision normal (direction away from spring)
                Vector2 bumpDirection = other.contacts[0].normal;

                // Apply force away from the spring
                rb.AddForce(bumpDirection * bumpForce, ForceMode2D.Impulse);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Level complete trigger
        if (collision.gameObject.CompareTag("Net"))
        {
            StartCoroutine(NextWithDelay());
        }

        if (collision.gameObject.CompareTag("Dead"))
        {
            Resettheball();
            GameManager.Instance.AttemptCnt++;
        }

        // Teleport trigger
        if (collision.gameObject.CompareTag("TP"))
        {
            TeleportPad tp = collision.GetComponent<TeleportPad>();

            if (tp != null && tp.targetPoint != null)
            {
                // Store current velocity
                Vector2 currentVelocity = rb.linearVelocity;

                // Teleport position
                transform.position = tp.targetPoint.position;

                // Restore velocity
                rb.linearVelocity = currentVelocity;
            }
        }
    }

    IEnumerator NextWithDelay()
    {
        yield return new WaitForSeconds(.5f);
            GameManager.Instance.LevelComplete();

    }

    private void Resettheball()
    {
        Rigidbody2D rb1 = GetComponent<Rigidbody2D>();
        transform.position=StartPos;
rb1.constraints |= RigidbodyConstraints2D.FreezePositionX;
rb1.constraints |= RigidbodyConstraints2D.FreezePositionY;


    }
}
