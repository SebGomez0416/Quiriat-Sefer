using System;
using Cinemachine;
using UnityEngine;

public class CameraSystem : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotateSpeed;
    [SerializeField] private int edgeScrollSize;
    [SerializeField] private bool useEdgeScrolling;
    [SerializeField] private CinemachineVirtualCamera virtualCamera;
    [SerializeField] private int fieldOfViewMax;
    [SerializeField] private int fieldOfViewMin;
    [SerializeField] private float zoomSpeed;
    [SerializeField] private Transform player;

    private float targetFieldOfView;

    private void Start()
    {
        targetFieldOfView = fieldOfViewMax;
    }

    private void Update()
    {
       HandleCameraMovement();
       HandleCameraRotation();
       HandleCameraZoom();
       LookPlayer();
    }

    private void HandleCameraMovement()
    {
        Vector3 inputDir = new Vector3(0, 0, 0);

        if (Input.GetKey(KeyCode.W)) inputDir.z = +1f;
        if (Input.GetKey(KeyCode.S)) inputDir.z = -1f;
        if (Input.GetKey(KeyCode.A)) inputDir.x = -1f;
        if (Input.GetKey(KeyCode.D)) inputDir.x = +1f;

        //edgescrolling
        if (useEdgeScrolling)
        {
            if (Input.mousePosition.x < edgeScrollSize) inputDir.x = -1f;
            if (Input.mousePosition.y < edgeScrollSize) inputDir.z = -1f;
            if (Input.mousePosition.x > Screen.width - edgeScrollSize) inputDir.x = +1f;
            if (Input.mousePosition.y > Screen.height - edgeScrollSize) inputDir.z = +1f;
        }

        Vector3 moveDir = transform.forward * inputDir.z + transform.right* inputDir.x;

        transform.position +=moveDir * moveSpeed * Time.deltaTime;

        Vector3 newDir;
        newDir.x = Mathf.Clamp(transform.position.x,  -5f, 318f);
        newDir.z = Mathf.Clamp(transform.position.z,  -73f, 210f);
        newDir.y = 0f;

        transform.position = newDir;
    }

    private void HandleCameraRotation()
    {
        float rotateDir = 0f;
        if (Input.GetKey(KeyCode.Q)) rotateDir = +1f;
        if (Input.GetKey(KeyCode.E)) rotateDir = -1f;

        transform.eulerAngles += new Vector3(0, rotateDir * rotateSpeed*Time.deltaTime, 0);
    }

    private void HandleCameraZoom()
    {
        switch (Input.mouseScrollDelta.y)
        {
            case > 0:
                targetFieldOfView -= 5;
                break;
            case < 0:
                targetFieldOfView += 5;
                break;
        }

        targetFieldOfView = Mathf.Clamp(targetFieldOfView, fieldOfViewMin, fieldOfViewMax);

        virtualCamera.m_Lens.FieldOfView = Mathf.Lerp(virtualCamera.m_Lens.FieldOfView, targetFieldOfView,
            Time.deltaTime * zoomSpeed);
    }

    private void LookPlayer()
    {
        if (Input.GetKey(KeyCode.F))
        {
            targetFieldOfView= fieldOfViewMax/2;
            transform.position = player.position; 

            Vector3 newDir;
            newDir.x = Mathf.Clamp(transform.position.x,  -5f, 318f);
            newDir.z = Mathf.Clamp(transform.position.z,  -73f, 210f);
            newDir.y = 0f;

            transform.position = newDir;
        }
    }
}
