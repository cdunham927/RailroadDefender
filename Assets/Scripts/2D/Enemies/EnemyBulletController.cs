using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletController : MonoBehaviour
{
    public float lowSpd;
    public float highSpd;
    float spd;
    Rigidbody2D bod;
    public float dmg;

    private void Awake()
    {
        bod = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        Invoke("Disable", 2f);
        spd = Random.Range(lowSpd, highSpd);
        bod.AddForce(transform.up * spd);
    }

    private void OnDisable()
    {
        CancelInvoke();
    }

    void Disable()
    {
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            var ob = collision.GetComponent<IDamageable<float>>();
            ob.Damage(dmg);
            gameObject.SetActive(false);
        }
    }
}
