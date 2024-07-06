using System.Collections;
using UnityEngine;

public class CloudManager : MonoBehaviour
{
    public CloudMover cloudMover;
    private int windowsDestroyed = 0;

    public void WindowDestroyed()
    {
        windowsDestroyed++;
        Debug.Log("CloudManager: Window destroyed, count = " + windowsDestroyed);

        if (windowsDestroyed >= 1 && cloudMover != null)
        {
            Debug.Log("CloudManager: Starting cloud movement");
            cloudMover.StartMoving();
        }
    }
}