using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public Camera targetCamera; // 조절할 카메라를 에디터에서 할당

    // nearClipPlane 값을 변경하는 함수
    public void SetCameraNearClip(float nearClipValue)
    {
        if (targetCamera != null)
        {
            targetCamera.nearClipPlane = nearClipValue;
            Debug.Log("Camera Near Clip Plane set to: " + nearClipValue);
        }
        else
        {
            Debug.LogWarning("No camera assigned!");
        }
    }
}