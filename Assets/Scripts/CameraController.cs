using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public FloatVariable followSpeed;
    //Might need variable for switch speed

    Vector3 startPos;
    public Transform target;

    private void Awake()
    {
        startPos = transform.position;
    }

    private void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, startPos + target.transform.position, Time.fixedDeltaTime * followSpeed.Value);
    }

    public void SetTarget(Transform tar)
    {
        target = tar;
    }
}
