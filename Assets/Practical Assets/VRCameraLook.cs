using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRCameraLook : MonoBehaviour
{
    public float mouseSensitivity = 1.0f;
    public float touchSensitivity = 1.0f;

    void Update()
    {
        float x = 0.0f;
        float y = 0.0f;

        // if on a mobile device use touch input
        if ((Application.platform == RuntimePlatform.Android ||
            Application.platform == RuntimePlatform.IPhonePlayer) &&
            Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if(touch.phase == TouchPhase.Moved)
            {
                // account for aspect ratio
                float width = Screen.width;
                float height = Screen.height;
                float divisor = height;
                if (height > width)
                {
                    divisor = width;
                }

                // account for touch input in screen coords
                x = (touch.deltaPosition.x / divisor) * touchSensitivity;
                y = (touch.deltaPosition.y / divisor) * touchSensitivity;
            }
        }
        // else use mouse input
        else if (Input.GetMouseButton(0))
        {
            x = Input.GetAxis("Mouse X") * mouseSensitivity;
            y = Input.GetAxis("Mouse Y") * mouseSensitivity;
        }

        // rotate the camera
        transform.Rotate(y, -x, 0.0f);

        // reset the up vector of the camera every frame
        float nx = transform.rotation.eulerAngles.x;
        float ny = transform.rotation.eulerAngles.y;
        transform.rotation = Quaternion.Euler(nx, ny, 0.0f);
    }
}
