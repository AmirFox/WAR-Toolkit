using UnityEngine;
using Zenject;
using WarToolkit.ObjectData;

public class CameraController : MonoBehaviour
{
    public float CameraSpeed = 5.0f;
    public float ScreenEdgeBorder = 10.0f;

    [Inject] private MatchData matchData;

    int width => matchData.mapData.Width;
    int height => matchData.mapData.Height;

    void Start()
    {

        //transform.position = mapData.SpawnPoints[0];
    }

    void Update()
    {
        // Move camera using WASD keys
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        transform.position += new Vector3(horizontal, vertical, 0) * CameraSpeed * Time.deltaTime;

        // Move camera by pushing mouse to edge of screen
        float mouseX = Input.mousePosition.x;
        float mouseY = Input.mousePosition.y;
        float screenWidth = Screen.width;
        float screenHeight = Screen.height;

        if (mouseX < ScreenEdgeBorder)
        {
            if(transform.position.x >= 0)
            {
                transform.position += Vector3.left * CameraSpeed * Time.deltaTime;
            }
        }
        else if (mouseX > screenWidth - ScreenEdgeBorder)
        {
            if(transform.position.x <= width)
            {
                transform.position += Vector3.right * CameraSpeed * Time.deltaTime;
            }
        }

        if (mouseY < ScreenEdgeBorder)
        {
            if(transform.position.y >= 0)
            {
                transform.position += Vector3.down * CameraSpeed * Time.deltaTime;
            }
        }
        else if (mouseY > screenHeight - ScreenEdgeBorder)
        {
            if(transform.position.y <= height)
            {
                transform.position += Vector3.up * CameraSpeed * Time.deltaTime;
            }
        }
    }
}
