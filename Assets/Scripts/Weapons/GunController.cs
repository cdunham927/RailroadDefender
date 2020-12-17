using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    //Thing to shoot
    public GameObject projectile;
    //Cooldown between shots
    float cools;
    public float shotCooldown;
    //How big the guns clip is and how full it currently is
    public int clipSize;
    int curClip;
    //Gun accuracy
    public float accuracy;
    public virtual void Shoot() { }
    public virtual void Reload() { }
    public virtual void Update()
    {
        if (cools > 0) cools -= Time.deltaTime;
    }
}
