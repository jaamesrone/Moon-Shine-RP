using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CameraTransition : MonoBehaviour
{
    public List<GameObject> buttons;
    public List<GameObject> gameObjectsToHide;


    public float transitionDuration;

    private Transform windowTrans;
    private Vector3 originalPosition;
    private Quaternion originalRotation;

    private void Start()
    {
        // Hide all game objects on start
        foreach (GameObject obj in gameObjectsToHide)
        {
            obj.SetActive(false);
        }
    }
    private void Update()
    {
        HandleSceneTransitions();
    }

    private void HandleSceneTransitions()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); //mouse clicks
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
        // hiding all game objects
        foreach (GameObject obj in gameObjectsToHide)
        {
            obj.SetActive(false);
        }

        windowTrans = targetTransform;
        originalPosition = transform.position;
        originalRotation = transform.rotation;

        // only showing game objects 0-2
        for (int i = 0; i <= 2 && i < gameObjectsToHide.Count; i++)
        {
            gameObjectsToHide[i].SetActive(true);
        }

        StopAllCoroutines(); // Stop any ongoing transitions
        StartCoroutine(ComputerTransitionCoroutine());
    }


    private IEnumerator ComputerTransitionCoroutine()
    {
        float elapsedTime = 0f;

        // distance between the camera and the object
        float distanceOffset = -500f; // You can adjust this value to control the distance

        // target position and rotation for the computer transition
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



    public void BackyardTransition(Transform targetTransform)
    {
        windowTrans = targetTransform;
        originalPosition = transform.position;
        originalRotation = transform.rotation;

        // hide all game objects
        foreach (GameObject obj in gameObjectsToHide)
        {
            obj.SetActive(false);
        }

        // element 0 shows only
        if (gameObjectsToHide.Count > 0) gameObjectsToHide[0].SetActive(true); // Element 0

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

        // final positions and rotation are exact
        transform.position = targetPosition;
        transform.rotation = targetRotation;
    }

    public void orderWindowTransition(Transform targetTransform)
    {
        // hide all game objects
        foreach (GameObject obj in gameObjectsToHide)
        {
            obj.SetActive(false);
        }

        // elements 0 and 5 show only
        if (gameObjectsToHide.Count > 0) gameObjectsToHide[0].SetActive(true); // Element 0
        if (gameObjectsToHide.Count > 5) gameObjectsToHide[5].SetActive(true); // Element 5

        windowTrans = targetTransform;
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

    public void MainAreaTransition()
    {
        // hide all game objects
        foreach (GameObject obj in gameObjectsToHide)
        {
            obj.SetActive(false);
        }

        StopAllCoroutines();
        StartCoroutine(mainAreaTransition());
    }

    public void StartStillTransistion(Transform targetTransform)
    {
        // hide all game objects
        foreach (GameObject obj in gameObjectsToHide)
        {
            obj.SetActive(false);
        }

        // elements 0,3, and 4 show only
        if (gameObjectsToHide.Count > 0) gameObjectsToHide[0].SetActive(true); // Element 0
        if (gameObjectsToHide.Count > 3) gameObjectsToHide[3].SetActive(true); // Element 3
        if (gameObjectsToHide.Count > 4) gameObjectsToHide[4].SetActive(true); // Element 4

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
