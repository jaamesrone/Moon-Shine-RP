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
                    GameObject[] stillCubes = GameObject.FindGameObjectsWithTag("OrderWindow");
                    if (stillCubes.Length > 0)
                    {
                        orderWindowTransition(stillCubes[0].transform);
                    }
                }
                else if (hit.collider.CompareTag("MainArea"))
                {
                    MainAreaTransition();
                }
                else if (hit.collider.CompareTag("Still"))
                {
                    // Find all cubes with the "Still" tag and use the last one as the target...etc
                    GameObject[] stillCubes = GameObject.FindGameObjectsWithTag("Still");
                    if (stillCubes.Length > 0)
                    {
                        StartStillTransistion(stillCubes[stillCubes.Length - 1].transform);
                    }
                }
                else if (hit.collider.CompareTag("Backyard"))
                {
                    GameObject[] stillCubes = GameObject.FindGameObjectsWithTag("Backyard");
                    if (stillCubes.Length > 0)
                    {
                        BackyardTransition(stillCubes[0].transform);
                    }
                }
                else if (hit.collider.CompareTag("Computer"))
                {
                    GameObject[] stillCubes = GameObject.FindGameObjectsWithTag("Computer");
                    if (stillCubes.Length > 0)
                    {
                        ComputerTransition(stillCubes[0].transform);
                    }
                }
            }
        }
    }

    public void ComputerTransition(Transform targetTransform)
    {
        windowTrans = targetTransform;
        originalPosition = transform.position;
        originalRotation = transform.rotation;

        StopAllCoroutines(); // Stop any ongoing transitions
        StartCoroutine(ComputerTransitionCoroutine());
    }

    private IEnumerator ComputerTransitionCoroutine()
    {
        float elapsedTime = 0f;

        // Adjust the distance between the camera and the object
        float distanceOffset = -500f; // You can adjust this value to control the distance

        // Adjust the target position and rotation for the computer transition
        Vector3 targetPosition = windowTrans.position + new Vector3(0f, 0f, distanceOffset);
        Quaternion targetRotation = Quaternion.Euler(0f, 0f, 0f); //rotate fixed

        while (elapsedTime < transitionDuration)
        {
            float t = elapsedTime / transitionDuration;

            transform.position = Vector3.Lerp(originalPosition, targetPosition, t);
            transform.rotation = Quaternion.Slerp(originalRotation, targetRotation, t);

            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

    private void BackyardTransition(Transform targetTransform)
    {
        windowTrans = targetTransform;
        originalPosition = transform.position;
        originalRotation = transform.rotation;

        StopAllCoroutines(); 
        StartCoroutine(BackyardTransitionCoroutine());
    }

    private IEnumerator BackyardTransitionCoroutine()
    {
        float elapsedTime = 0f;

        // float target position and rotation for the backyard transition
        float targetXOffset = -500f;
        Vector3 targetPosition = windowTrans.position + new Vector3(targetXOffset, 0f, -5f);
        Quaternion targetRotation = Quaternion.Euler(0f, 90f, 0f); 

        while (elapsedTime < transitionDuration)
        {
            float t = elapsedTime / transitionDuration;

            transform.position = Vector3.Lerp(originalPosition, targetPosition, t);
            transform.rotation = Quaternion.Slerp(originalRotation, targetRotation, t);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure final positions and rotation are exact
        transform.position = targetPosition;
        transform.rotation = targetRotation;
    }

        public void orderWindowTransition(Transform targetTransform)
    {
        windowTrans = targetTransform;
        originalPosition = transform.position;
        originalRotation = transform.rotation;

        StopAllCoroutines();
        StartCoroutine(startOrderWindowTransition()); 
    }

    private IEnumerator startOrderWindowTransition()
    {
        float elapsedTime = 0f;

        //fixed Z value for the transition
        float targetZ = 500f;

        // distance between the camera and the object
        float distanceOffset = 500; // You can adjust this value to control the distance

        // adjust the target position and rotation for the transition
        Vector3 targetPosition = new Vector3(windowTrans.position.x + distanceOffset, windowTrans.position.y, targetZ);
        Quaternion targetRotation = Quaternion.Euler(0f, -90f, 0f); // Adjust the rotation as needed

        while (elapsedTime < transitionDuration)
        {
            float t = elapsedTime / transitionDuration;

            // Use Lerp to smoothly transition the position and rotation
            transform.position = Vector3.Lerp(originalPosition, targetPosition, t);
            transform.rotation = Quaternion.Slerp(originalRotation, targetRotation, t);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPosition;
        transform.rotation = targetRotation;
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

        StopAllCoroutines(); // stop any ongoing transitions
        StartCoroutine(StillTransition());
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
