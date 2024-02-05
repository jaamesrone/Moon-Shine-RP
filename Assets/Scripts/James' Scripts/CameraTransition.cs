using System.Collections;
using UnityEngine;

public class CameraTransition : MonoBehaviour
{
    public float transitionDuration;

    private Transform windowTrans;
    private Vector3 originalPosition;
    private Quaternion originalRotation;

    private void Update()
    {
        HandleSceneTransitions();
    }

    private void HandleSceneTransitions()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                if (hit.collider.CompareTag("OrderWindow"))
                {
                    orderWindowTransition(hit.transform);
                }
                else if (hit.collider.CompareTag("MainArea"))
                {
                    MainAreaTransition();
                }
                else if (hit.collider.CompareTag("Still"))
                {
                    StartStillTransistion(hit.transform);
                }
            }
        }
    }

    public void orderWindowTransition(Transform targetTransform)
    {
        windowTrans = targetTransform;
        originalPosition = transform.position;
        originalRotation = transform.rotation;

        StopAllCoroutines(); 
        StartCoroutine(startOrderWindowTransition());
    }

    private void MainAreaTransition()
    {
        StopAllCoroutines(); 
        StartCoroutine(mainAreaTransition());
    }

    private void StartStillTransistion(Transform targetTransform)
    {
        windowTrans = targetTransform;
        originalPosition = transform.position;
        originalRotation = transform.rotation;

        StopAllCoroutines(); // Stop any ongoing transitions
        StartCoroutine(StillTransition());
    }

    private IEnumerator startOrderWindowTransition()
    {
        float elapsedTime = 0f;

        // distance between the camera and window
        float targetXOffset = 500f;

        Vector3 targetPosition = windowTrans.position + new Vector3(targetXOffset, 0f, -5f); // Adjust the Z offset based on your scene setup

        while (elapsedTime < transitionDuration)
        {
            float t = elapsedTime / transitionDuration;

            // Use a smoothstep function to smoothly transitions the x from the original pov of the camera to the window scene
            float smoothStep = Mathf.SmoothStep(originalPosition.x, targetPosition.x, t);

            transform.position = new Vector3(smoothStep, Mathf.Lerp(originalPosition.y, targetPosition.y, t), Mathf.Lerp(originalPosition.z, targetPosition.z, t));

            transform.rotation = Quaternion.Slerp(originalRotation, Quaternion.Euler(0f, -90f, 0f), t);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // making sure the final positions and rotation are exact because interpolation can be difficult
        transform.position = targetPosition;
        transform.rotation = Quaternion.Euler(0f, -90f, 0f);
    }

    private IEnumerator mainAreaTransition()
    {
        float elapsedTime = 0f;
        Vector3 originalCameraPosition = transform.position;
        Quaternion originalCameraRotation = transform.rotation;

        while (elapsedTime < transitionDuration)
        {
            float t = elapsedTime / transitionDuration;

            transform.position = Vector3.Lerp(originalCameraPosition, Vector3.zero, t); //interpolates camera's pov back to it's orignal pov upon starting the game
            transform.rotation = Quaternion.Slerp(originalCameraRotation, Quaternion.identity, t);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // making sure the final positions and rotation are exact because interpolation can be difficult
        transform.position = Vector3.zero;
        transform.rotation = Quaternion.identity;
    }

    private IEnumerator StillTransition()
    {
        float elapsedTime = 0.0f;

        // distance between camera to still cube
        float zOffset = -350f;

        Vector3 targetPosition = windowTrans.position + new Vector3(0f, 0f, zOffset);

        while (elapsedTime < transitionDuration)
        {
            float t = elapsedTime / transitionDuration;

            // smoothstep interpolates between a min(origPos) and max(targetPosition)
            float smoothStepX = Mathf.SmoothStep(originalPosition.x, targetPosition.x, t);
            float smoothStepZ = Mathf.SmoothStep(originalPosition.z, targetPosition.z, t);

            //update the camera position using the interpolated X and Z coordinates
            transform.position = new Vector3(smoothStepX, originalPosition.y, smoothStepZ);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // making sure the final positions and rotation are exact because interpolation can be difficult
        transform.position = new Vector3(targetPosition.x, originalPosition.y, targetPosition.z);
    }



}
