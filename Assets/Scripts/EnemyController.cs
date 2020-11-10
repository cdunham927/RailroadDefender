using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour, IDamageable<float>, IKillable
{
    public FloatVariable maxHp;
    float hp;

    void OnEnable()
    {
        hp = maxHp.Value;
    }

    public void Kill()
    {

    }

    public void Damage(float amt)
    {

    }
}
