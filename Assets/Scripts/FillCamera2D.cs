using UnityEngine;

[RequireComponent(typeof(Camera))]
public class FillCamera2D : MonoBehaviour
{
    void Awake()
    {
        Camera cam = GetComponent<Camera>();

        // Set to Perspective
        cam.orthographic = false;

        // Lock Field Of View
        cam.fieldOfView = 60f;

        // Lock camera position
        transform.position = new Vector3(0f, 0f, -10f);
    }
}