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

    public override int GetCurrentClipSize()
    {
        return curClip;
    }

    public override void Fire(GameObject spawnPoint)
    {
        if (cools <= 0 && curClip > 0)
        {
            //Debug.Log("Shooting");
            BulletController2D obj = Instantiate(bullet, spawnPoint.transform.position, transform.rotation * Quaternion.Euler(0, 0, Random.Range(-accuracy, accuracy)));
            obj.dmg = dmg;
            Debug.Log("Pistol damage: " + dmg.ToString() + "       Bullet damage: " + obj.dmg.ToString());
            if (Random.value <= critChance) obj.dmg = dmg * 3;
            cools = timeBetweenShots;
            curClip--;
        }
        else if (curClip <= 0)
        {
            //Debug.Log("Reloading");
            StartReload();
        }
    }
}
