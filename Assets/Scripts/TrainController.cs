using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainController : MonoBehaviour, IDamageable<float>, IKillable
{
    public FloatVariable maxHp;
    float hp;
    float iframes;

    void Awake()
    {
        hp = maxHp.Value;
    }

    void Update()
    {
        if (iframes > 0) iframes -= Time.deltaTime;
    }

    public void Kill()
    {

    }

    public void Damage(float amt)
    {
        if (iframes <= 0)
        {
            hp -= amt;

            if (hp <= 0)
            {
                Kill();
            }

            iframes = 0.01f;
        }
    }
}
