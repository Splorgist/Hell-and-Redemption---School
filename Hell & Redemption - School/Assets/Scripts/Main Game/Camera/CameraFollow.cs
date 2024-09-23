using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private float flipYRotationTime = 0.5f;
    [SerializeField] private float lerpSpeed = 2f;
    private Coroutine turnCoroutine;
    private PlayerMovement playerMovement;
    private float targetOffsetX;

    void Awake()
    {
        playerMovement = playerTransform.GetComponent<PlayerMovement>();
        targetOffsetX = 1f; // Initial offset
        UpdateCameraPosition();
    }

    void Update()
    {
        // Check if camera needs to lerp
        if (!playerMovement._cameraRight)
        {
            targetOffsetX = Mathf.Lerp(targetOffsetX, -1f, Time.deltaTime * lerpSpeed);
        }
        else
        {
            targetOffsetX = Mathf.Lerp(targetOffsetX, 1f, Time.deltaTime * lerpSpeed);
        }

        UpdateCameraPosition();
    }

    private void UpdateCameraPosition()
    {
        transform.position = new Vector3(playerTransform.position.x + targetOffsetX, playerTransform.position.y, playerTransform.position.z);
    }
}
