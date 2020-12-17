using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateWithCamera : MonoBehaviour
{
    float rotationX = 0f;
    PlayerController player;
    Quaternion startRot;

    private void Awake()
    {
        startRot = transform.localRotation;
        player = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            //Rotation
            rotationX += -Input.GetAxis("Mouse Y") * player.rotSpd;
            rotationX = Mathf.Clamp(rotationX, -player.lookXLimit, player.lookXLimit);
            transform.localRotation = startRot * Quaternion.Euler(-rotationX, 0, 0);
            //transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * player.rotSpd, 0);
        }
    }
}
