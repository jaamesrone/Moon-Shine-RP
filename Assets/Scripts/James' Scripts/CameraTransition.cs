using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CameraTransition : MonoBehaviour
{
    public List<GameObject> buttons;


    public float transitionDuration;

    private Transform targetPos;
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
                else if (hit.collider.CompareTag("Backyard"))
                {
                    BackyardTransition(hit.transform);
                }
                else if (hit.collider.CompareTag("Computer"))
                {
                    ComputerTransition(hit.transform);
                }
            }
        }
    }



    public void ComputerTransition(Transform targetTransform)
    {
        // hiding all game objects
        foreach (GameObject obj in buttons)
        {
            obj.SetActive(false);
        }

        targetPos = targetTransform;
        originalPosition = transform.position;
        originalRotation = transform.rotation;

        // only showing game objects 0-2
        for (int i = 0; i <= 2 && i < buttons.Count; i++)
        {
            buttons[i].SetActive(true);
        }

        StopAllCoroutines(); // Stop any ongoing transitions
        StartCoroutine(ComputerTransitionCoroutine());
    }


    private IEnumerator ComputerTransitionCoroutine()
    {
        float elapsedTime = 0f;

        // distance between the camera and the object
        float distanceOffset = -500f; // adjust this value to control the distance

        // target position and rotation for the computer transition
        Vector3 targetPosition = targetPos.position + new Vector3(0f, 0f, distanceOffset);
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



    public void BackyardTransition(Transform targetTransform)
    {
        targetPos = targetTransform;
        originalPosition = transform.position;
        originalRotation = transform.rotation;

        // hide all game objects
        foreach (GameObject obj in buttons)
        {
            obj.SetActive(false);
        }

        // element 0 shows only
        if (buttons.Count > 0) buttons[0].SetActive(true); // Element 0

        StopAllCoroutines();
        StartCoroutine(BackyardTransitionCoroutine());

    }

    private IEnumerator BackyardTransitionCoroutine()
    {
        float elapsedTime = 0f;

        // float target position and rotation for the backyard transition
        float targetXOffset = -500f;
        Vector3 targetPosition = targetPos.position + new Vector3(targetXOffset, 0f, -5f);
        Quaternion targetRotation = Quaternion.Euler(0f, 90f, 0f); 

        while (elapsedTime < transitionDuration)
        {
            float t = elapsedTime / transitionDuration;

            transform.position = Vector3.Lerp(originalPosition, targetPosition, t);
            transform.rotation = Quaternion.Slerp(originalRotation, targetRotation, t);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // final positions and rotation are exact
        transform.position = targetPosition;
        transform.rotation = targetRotation;
    }

    public void orderWindowTransition(Transform targetTransform)
    {
        // hide all game objects
        foreach (GameObject obj in buttons)
        {
            obj.SetActive(false);
        }

        // elements 0 and 5 show only
        if (buttons.Count > 0) buttons[0].SetActive(true); // Element 0
        if (buttons.Count > 5) buttons[5].SetActive(true); // Element 5

        targetPos = targetTransform;
        originalPosition = transform.position;
        originalRotation = transform.rotation;

        StopAllCoroutines(); // stop any ongoing transitions
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
        Vector3 targetPosition = new Vector3(targetPos.position.x + distanceOffset, targetPos.position.y, targetZ);
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

    public void MainAreaTransition()
    {
        // hide all game objects
        foreach (GameObject obj in buttons)
        {
            obj.SetActive(false);
        }

        StopAllCoroutines();
        StartCoroutine(mainAreaTransition());
    }

    public void StartStillTransistion(Transform targetTransform)
    {
        // hide all game objects
        foreach (GameObject obj in buttons)
        {
            obj.SetActive(false);
        }

        // elements 0,3, and 4 show only
        if (buttons.Count > 0) buttons[0].SetActive(true); // Element 0
        if (buttons.Count > 3) buttons[3].SetActive(true); // Element 3
        if (buttons.Count > 4) buttons[4].SetActive(true); // Element 4

        targetPos = targetTransform;
        originalPosition = transform.position;
        originalRotation = transform.rotation;

        StopAllCoroutines(); // stop any ongoing transitions
        StartCoroutine(StillTransition(targetTransform));
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

    private IEnumerator StillTransition(Transform targetTransform)
    {
        float elapsedTime = 0.0f;

        // zdistant offset between camera and object
        float zDistance = -500f;

        // we calculate the target position with the specified distance along the z-axis
        Vector3 targetPosition = targetTransform.position + targetTransform.forward * zDistance;

        // now I save the original position and rotation of the camera
        Vector3 originalPosition = transform.position;
        Quaternion originalRotation = transform.rotation;

        while (elapsedTime < transitionDuration)
        {
            float t = elapsedTime / transitionDuration;

            // smoothly interpolate the position and rotation
            transform.position = Vector3.Lerp(originalPosition, targetPosition, t);
            transform.rotation = Quaternion.Slerp(originalRotation, Quaternion.identity, t);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // making sure the final position and rotation are exact
        transform.position = targetPosition;
        transform.rotation = Quaternion.identity;
    }


}
