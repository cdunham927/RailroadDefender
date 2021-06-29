using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainController2D : MonoBehaviour, IDamageable<float>, IKillable
{
    public float maxHealth;
    public float health;
    public Cargo[] cargo;
    public int trainCars;

    [SerializeField]
    Crate testCrate;
    
    void Awake()
    {
        cargo = FindObjectsOfType<Cargo>();
        maxHealth = cargo.Length * cargo[0].maxWeight;
        health = maxHealth;
    }

    void Update()
    {
        if (Application.isEditor)
        {
            if (Input.GetKeyDown(KeyCode.O) && cargo.Length > 0)
            {
                int ranCargo = Random.Range(0, cargo.Length);
                testCrate = cargo[ranCargo].GetCrate();
            }
            if (Input.GetKeyDown(KeyCode.Y))
            {
                if (testCrate != null && testCrate.gameObject.activeInHierarchy)
                {
                    testCrate.Damage();
                    testCrate = null;
                }
            }
            if (Input.GetKeyDown(KeyCode.P))
            {
                if (testCrate != null)
                {
                    testCrate.RestoreCrate();
                    testCrate = null;
                }
            }
        }
    }

    public void Damage(float amt)
    {
        health -= amt;

        if (health <= 0) Die();
    }

    public void Die()
    {
        Debug.Log("Train lost all cargo");
    }
}
