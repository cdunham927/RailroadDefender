using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController2D : MonoBehaviour
{
    public GameObject bullet;
    public GameObject spawnPoint;
    public float timeBetweenShots = 0.1f;
    public int clipSize;
    int curClip;
    float clipCools;
    public float reloadTime;
    float cools;
    bool reloading = false;
    public float accuracy;

    private void Awake()
    {
        curClip = clipSize;
    }

    private void Update()
    {
        //If you have bullets in the clip and the cooldown is 0 and you press left mouse button
        if (Input.GetMouseButton(0))
        {
            if (cools <= 0 && curClip > 0)
            {
                GameObject obj = Instantiate(bullet, spawnPoint.transform.position, transform.rotation * Quaternion.Euler(0, 0, Random.Range(-accuracy, accuracy)));
                cools = timeBetweenShots;
                curClip--;
            }
            if (curClip <= 0)
            {
                reloading = true;
            }
        }

        if (reloading)
        {
            if (clipCools >= reloadTime)
            {
                curClip = clipSize;
                clipCools = 0;
                reloading = false;
            }

            clipCools += Time.deltaTime;
        }

        if (cools > 0) cools -= Time.deltaTime;
    }
}
