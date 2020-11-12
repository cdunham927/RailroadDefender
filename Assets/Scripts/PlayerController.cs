using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, IDamageable<float>, IKillable
{
    //Stats
    public FloatVariable maxHp;
    float hp;
    public FloatVariable speed;
    public FloatVariable runSpeed;
    float curSpeed;

    //Input
    Vector2 inp;

    //Components
    //Rigidbody bod;
    Vector3 mousePos;
    CharacterController controller;

    //Movement and rotation
    Vector3 movement;
    float rotationX = 0;
    public float lookXLimit = 45.0f;
    public float jumpHeight = 8f;
    public float grav = 20f;
    public float rotSpd;

    void OnEnable()
    {
        //bod = GetComponent<Rigidbody>();
        controller = GetComponent<CharacterController>();
        hp = maxHp.Value;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        //if (controller == null) controller = GetComponent<CharacterController>();
        mousePos = Input.mousePosition;
        inp = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        //Change run speed if you're holding the run button down
        curSpeed = Input.GetButton("Run") ? runSpeed.Value : speed.Value;

        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);
        float movementY = movement.y;

        movement = (forward * curSpeed * inp.y) + (right * curSpeed * inp.x);

        //Jumping
        if (Input.GetButton("Jump") && controller.isGrounded)
        {
            movement.y = jumpHeight;
        }
        else
        {
            movement.y = movementY;
        }

        if (!controller.isGrounded)
        {
            movement.y -= grav * Time.deltaTime;
        }

        if (controller.enabled) controller.Move(movement * Time.deltaTime);

        //Rotation
        rotationX += -Input.GetAxis("Mouse Y") * rotSpd;
        rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
        Camera.main.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
        transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * rotSpd, 0);
    }

    public void Kill()
    {

    }

    public void Damage(float amt)
    {

    }
}
