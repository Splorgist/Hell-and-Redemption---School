using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // [SerializeField] private Transform playerTransform;
    // [SerializeField] private float flipYRotationTime = 0.5f;
    // private Coroutine turnCoroutine;
    // public GameObject player;
    // private bool facingRight;
    // void Awake()
    // {
    //     player = playerTransform.gameObject.GetComponent<Player>();
    //     facingRight = player.facingRight;
    // }

    // void Update()
    // {
    //     transform.position = playerTransform.position;
    // }

    // public void CallTurn()
    // {
    //     turnCoroutine = StartCoroutine(FlipYLerp());
    // }

    // private IEnumerator FlipYLerp()
    // {
    //     float startRotation = transform.localEulerAngles.y;
    //     float endRotationAmount = DetermineEndRotation();
    //     float yRotation = 0f;

    //     float elapseTime = 0f;
    //     while (elapseTime < flipYRotationTime){
    //         elapseTime += Time.deltaTime;

    //         yRotation = Mathf.Lerp(startRotation, endRotationAmount, (elapseTime / flipYRotationTime));
    //         transform.rotation = Quaternion.Euler(0f, yRotation, 0f);

    //         yield return null;
    //     }
    // }

    // private float DetermineEndRotation()
    // {
    //     facingRight = !facingRight;

    //     if (facingRight){
    //         return 180f;
    //     }else{
    //         return 0f;
    //     }
    // }
}
