using System.Collections;
using UnityEngine;

public class CameraTransitionTrigger : MonoBehaviour
{
    public Transform targetTransform;

    private void OnMouseDown()
    {
        StartOrderWindowTransition();
    }

    private void StartOrderWindowTransition()
    {
        Camera.main.GetComponent<CameraTransition>().orderWindowTransition(targetTransform);
    }
}
