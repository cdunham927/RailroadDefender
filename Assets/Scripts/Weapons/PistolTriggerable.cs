using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolTriggerable : GunController
{
    private void Awake()
    {
        curClip = clipSize;
    }

    public override void StartReload()
    {
        reloading = true;
        clipCools = 0;
    }

    public override void Fire(GameObject spawnPoint)
    {
        if (cools <= 0 && curClip > 0)
        {
            //Debug.Log("Shooting");
            GameObject obj = Instantiate(bullet, spawnPoint.transform.position, transform.rotation * Quaternion.Euler(0, 0, Random.Range(-accuracy, accuracy)));
            cools = timeBetweenShots;
            curClip--;
        }
        else
        {
            //Debug.Log("Reloading");
            StartReload();
        }
    }
}
