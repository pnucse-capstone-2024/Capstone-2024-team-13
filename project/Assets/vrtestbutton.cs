using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vrtestbutton : MonoBehaviour
{
    public GameObject StartCanvas;
    public Camera playerCamera;

    public void testbutton()
    {
        StartCanvas.SetActive(false);
        playerCamera.targetDisplay = 0;
    }
}
