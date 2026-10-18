using UnityEngine;

public class AutoCameraFollow : MonoBehaviour
{
    public string levelTag = "Level";
    public float fixedY = 10f;
    public float fixedZ = -10f;

    void LateUpdate()
    {
        GameObject level = GameObject.FindWithTag(levelTag);

        if (level != null)
        {
            transform.position = new Vector3(
                level.transform.position.x,
                fixedY,
                fixedZ
            );
        }
    }
}