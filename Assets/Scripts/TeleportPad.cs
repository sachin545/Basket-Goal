// using UnityEngine;

// public class TeleportPad : MonoBehaviour
// {
//     public Transform Circle;
//     public Transform targetPoint;

//     public float rotationSpeed = 100f; // degrees per second


   
//     void Update()
//     {
//         if (Circle != null)
//         {
//             Circle.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);
         
//         }
//     }
// }


using UnityEngine;

public class TeleportPad : MonoBehaviour
{
    public Transform Circle;
    public Transform targetPoint;

    public float rotationSpeed = 100f; // degrees per second

    private bool isTeleporting = false;

    void Update()
    {
        if (Circle != null)
        {
            Circle.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ball") && !isTeleporting)
        {
            StartCoroutine(Teleport(other.transform));
        }
    }

    private System.Collections.IEnumerator Teleport(Transform ball)
    {
        isTeleporting = true;

        // Move ball to target
        ball.position = targetPoint.position;

        yield return new WaitForSeconds(0.2f);

        isTeleporting = false;
    }
}