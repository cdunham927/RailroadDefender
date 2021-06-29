using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController2D : MonoBehaviour
{
    public GameObject spawnPoint;
    public GunController gun;

    PlayerController2D parent;

    private void OnEnable()
    {
        gun = GetComponent<GunController>();
        parent = transform.parent.GetComponent<PlayerController2D>();
    }


    private void Update()
    {
        if (parent.canMove)
        {
            //If you have bullets in the clip and the cooldown is 0 and you press left mouse button
            if (Input.GetMouseButton(0))
            {
                gun.Fire(spawnPoint);
            }
        }
    }
}
