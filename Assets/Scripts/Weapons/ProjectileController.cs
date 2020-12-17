using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    public float dmg;
    Rigidbody bod;

    private void Awake()
    {
        bod = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        
    }
}
