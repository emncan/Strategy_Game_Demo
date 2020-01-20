using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private float panSpeed = 20f; // camera pan speed
    [SerializeField]
    private float panBorderThickness = 10f; //camera border thickness
    private float xMax; // map x max position
    private float yMin; // map y max position
    // Update is called once per frame
    void Update()
    {
        GetInput();
    }
    /// <summary>
    /// camera movement
    /// </summary>
    private void GetInput()
    {
        if (Input.GetKey(KeyCode.W) || Input.mousePosition.y >= Screen.height - panBorderThickness)
        {
            transform.Translate(Vector3.up * panSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A) || Input.mousePosition.x <= panBorderThickness)
        {
            transform.Translate(Vector3.left * panSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D) || Input.mousePosition.x >= Screen.width - panBorderThickness)
        {
            transform.Translate(Vector3.right * panSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S) || Input.mousePosition.y <= panBorderThickness)
        {
            transform.Translate(Vector3.down * panSpeed * Time.deltaTime);
        }

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, 0, xMax), Mathf.Clamp(transform.position.y, yMin, 0),-10);
    }
    /// <summary>
    ///  restrict to camera movement
    /// </summary>
    /// <param name="maxTile">posiion of the max tile </param>
    public void SetLimits(Vector3 maxTile)
    {
        Vector3 viewPort = Camera.main.ViewportToWorldPoint(new Vector3(1, 0)); // bottom-right world position of the camera

        xMax = maxTile.x - viewPort.x;
        yMin = maxTile.y - viewPort.y;

    }


}
