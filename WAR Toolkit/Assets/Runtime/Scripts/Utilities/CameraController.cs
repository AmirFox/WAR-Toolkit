using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float cameraSpeed = 5.0f;
    public float screenEdgeBorder = 10.0f;

    void Update()
    {
        // Move camera using WASD keys
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        transform.position += new Vector3(horizontal, vertical, 0) * cameraSpeed * Time.deltaTime;

        // Move camera by pushing mouse to edge of screen
        float mouseX = Input.mousePosition.x;
        float mouseY = Input.mousePosition.y;
        float screenWidth = Screen.width;
        float screenHeight = Screen.height;

        if (mouseX < screenEdgeBorder)
        {
            transform.position += Vector3.left * cameraSpeed * Time.deltaTime;
        }
        else if (mouseX > screenWidth - screenEdgeBorder)
        {
            transform.position += Vector3.right * cameraSpeed * Time.deltaTime;
        }

        if (mouseY < screenEdgeBorder)
        {
            transform.position += Vector3.down * cameraSpeed * Time.deltaTime;
        }
        else if (mouseY > screenHeight - screenEdgeBorder)
        {
            transform.position += Vector3.up * cameraSpeed * Time.deltaTime;
        }
    }
}
