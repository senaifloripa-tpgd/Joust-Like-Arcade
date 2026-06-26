using UnityEngine;

public class LimitesManager : MonoBehaviour
{
    private Camera mainCamera;
    private float screenWidth;
    // private float screenHeight;

    public float teleportMargin = 1f;

    void Start()
    {
        mainCamera = Camera.main;
        screenWidth = mainCamera.orthographicSize * mainCamera.aspect;
        // screenHeight = mainCamera.orthographicSize;
    }

    void Update()
    {
        if (transform.position.x > screenWidth + teleportMargin)
        {
            transform.position = new Vector2(-screenWidth - teleportMargin, transform.position.y);
        }
        else if (transform.position.x < -screenWidth - teleportMargin)
        {
            transform.position = new Vector2(screenWidth + teleportMargin, transform.position.y);
        }

        // if (transform.position.y > screenHeight + teleportMargin)
        // {
        //     transform.position = new Vector2(transform.position.x, -screenHeight - teleportMargin);
        // }
        // else if (transform.position.y < -screenHeight - teleportMargin)
        // {
        //     transform.position = new Vector2(transform.position.x, screenHeight + teleportMargin);
        // }
    }
}
