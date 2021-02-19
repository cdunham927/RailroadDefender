using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController2D : MonoBehaviour
{
    Rigidbody2D bod;
    public float spd;
    public Vector2 bounds;
    Vector2 input;
    public float aimSpd;

    public bool canMove;

    private void Awake()
    {
        bod = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (canMove)
        {
            //Aim at mouse
            Vector3 dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.AngleAxis(angle, Vector3.forward), Time.fixedDeltaTime * aimSpd);

            //Player movement
            input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

            if (input.x != 0)
            {
                bod.AddForce(Vector2.right * input.x * spd * Time.fixedDeltaTime);
            }
            if (input.y != 0)
            {
                bod.AddForce(Vector2.up * input.y * spd * Time.fixedDeltaTime);
            }
        }
    }
}
