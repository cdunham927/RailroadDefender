using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController2D : MonoBehaviour, IDamageable<float>
{
    [Header("Health n shit")]
    public FloatVariable maxHealth;
    [SerializeField]
    FloatVariable health;
    Rigidbody2D bod;
    [SerializeField]
    public FloatVariable spd;
    public FloatVariable walkSpd;
    public FloatVariable runSpd;
    public Vector2 bounds;
    Vector2 input;
    public float aimSpd;
    public FloatVariable maxStamina;
    [SerializeField]
    FloatVariable stamina;
    public float staminaRecoveryRate = 3f;

    public bool canMove;

    private void Awake()
    {
        bod = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        health.Value = maxHealth.Value;
        stamina.Value = maxStamina.Value;
    }

    public FloatVariable GetStamina()
    {
        return stamina;
    }

    public FloatVariable GetHealth()
    {
        return health;
    }

    public void Damage(float amt)
    {
        health.Value -= amt;
    }

    private void FixedUpdate()
    {
        if (canMove)
        {
            //Aim at mouse
            Vector3 dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.AngleAxis(angle, Vector3.forward), Time.fixedDeltaTime * aimSpd);

            spd.Value = (Input.GetButton("Run") && stamina.Value > 0) ? runSpd.Value : walkSpd.Value;
            if (!Input.GetButton("Run") && stamina.Value < maxStamina.Value) stamina.Value += (Time.deltaTime * staminaRecoveryRate);
            if (Input.GetButton("Run") && stamina.Value > 0) stamina.Value -= Time.deltaTime;
            stamina.Value = Mathf.Clamp(stamina.Value, 0, maxStamina.Value);

            //Player movement
            input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

            if (input.x != 0)
            {
                bod.AddForce(Vector2.right * input.x * spd.Value * Time.fixedDeltaTime);
            }
            if (input.y != 0)
            {
                bod.AddForce(Vector2.up * input.y * spd.Value * Time.fixedDeltaTime);
            }
        }
    }
}
