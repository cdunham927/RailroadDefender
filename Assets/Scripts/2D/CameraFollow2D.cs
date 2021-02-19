using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow2D : MonoBehaviour
{
    Transform target;
    public float lerpSpd;

    private void Awake()
    {
        target = FindObjectOfType<PlayerController2D>().transform;
    }

    public void SetTarget(Transform trans)
    {
        target = trans;
    }

    private void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, new Vector3(target.position.x, target.position.y, -10f), lerpSpd * Time.fixedDeltaTime);
    }
}