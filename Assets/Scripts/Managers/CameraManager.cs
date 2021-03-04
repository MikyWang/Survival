using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] Vector2 yZoomLimits;
    [SerializeField] Vector2 zZoomLimits;
    [SerializeField] Vector2 xZoomMovement;
    [SerializeField] Vector2 zZoomMovement;
    [SerializeField] float cameraSpeed;
    Transform mainCamera;
    Transform zoomObject;
    Vector2 input;
    bool mouseMove = true;
    Vector3 moveInput;

    void Awake()
    {
        mainCamera = Camera.main.transform;
        // transform.LookAt(mainCamera);
        zoomObject = transform.GetChild(0);
    }

    void Update()
    {
        moveInput.Set(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);

#if UNITY_STANDALONE
        if (mouseMove)
        {
            Vector2 mousePos = Input.mousePosition;

            if (mousePos.x > Screen.width * 0.95f && mousePos.x < Screen.width)
                moveInput.x = 1;
            else if (mousePos.x < Screen.width * 0.05f && mousePos.x > 0)
                moveInput.x = -1;

            if (mousePos.y > Screen.height * 0.95f && mousePos.y < Screen.height)
                moveInput.z = 1;
            else if (mousePos.y < Screen.height * 0.05f && mousePos.y > 0)
                moveInput.z = -1;
        }
#endif

        Vector3 movementDirection = mainCamera.TransformDirection(moveInput);
        movementDirection.y = 0;
        var pos = transform.position + movementDirection.normalized * Time.deltaTime * cameraSpeed;
        transform.position = new Vector3(Mathf.Clamp(pos.x, xZoomMovement.x, xZoomMovement.y), pos.y, Mathf.Clamp(pos.z, zZoomMovement.x, zZoomMovement.y));

        zoomObject.localPosition += new Vector3(0, Input.mouseScrollDelta.y, -Input.mouseScrollDelta.y);
        zoomObject.localPosition = new Vector3(zoomObject.localPosition.x, Mathf.Clamp(zoomObject.localPosition.y, yZoomLimits.x, yZoomLimits.y), Mathf.Clamp(zoomObject.localPosition.z, zZoomLimits.x, zZoomLimits.y));

    }
}
