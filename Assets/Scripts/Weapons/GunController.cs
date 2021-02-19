using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    //Thing to shoot
    public GameObject bullet;
    public float timeBetweenShots = 0.1f;
    public int clipSize;
    public float reloadTime;
    public float accuracy;
    public bool singleReload = false;
    [SerializeField] protected float cools;
    [SerializeField] protected int curClip;
    [SerializeField] protected bool reloading;
    [SerializeField] protected float clipCools;

    public void Initialize(GunController gun)
    {
        bullet = gun.bullet;
        timeBetweenShots = gun.timeBetweenShots;
        clipSize = gun.clipSize;
        reloadTime = gun.reloadTime;
        accuracy = gun.accuracy;
    }
    public virtual void Fire(GameObject spawnPoint) { }
    public virtual void StartReload() { }

    protected void Reload()
    {
        if (singleReload)
        {
            if (curClip < clipSize)
            {
                curClip++;
                clipCools = 0;
                return;
            }
        }

        curClip = clipSize;
        clipCools = 0;
        reloading = false;
    }

    void Awake()
    {
        curClip = clipSize;
    }

    void Update()
    {
        if (reloading)
        {
            if (clipCools >= reloadTime)
            {
                Reload();
            }

            clipCools += Time.deltaTime;
        }

        if (cools > 0) cools -= Time.deltaTime;
    }
}
