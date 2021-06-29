using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunTriggerable : GunController
{
    public int shotCount = 10;
    [Range(0.1f, 3f)]
    public float spread = 1f;

    private void Awake()
    {
        curClip = clipSize;
    }

    public override int GetCurrentClipSize()
    {
        return curClip;
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
            for (int i = 0; i < shotCount; i++)
            {
                BulletController2D obj = Instantiate(bullet, spawnPoint.transform.position, transform.rotation * Quaternion.Euler(0, 0, -(spread * shotCount / 2) + spread * i) * Quaternion.Euler(0, 0, Random.Range(-accuracy, accuracy)));
                obj.dmg = dmg;
                if (Random.value <= critChance) obj.dmg = dmg * 3;
            }
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
