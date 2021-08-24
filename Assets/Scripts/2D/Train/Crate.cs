using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crate : MonoBehaviour
{
    public float weight = 1.0f;
    public float price;
    Cargo parent;
    TrainController2D train;
    [SerializeField]
    Vector3 startScale;
    public float scaleSize = 0.5f;
    bool hasHurt = false;
    [HideInInspector]
    public Vector3 startPos;

    private void Awake()
    {
        hasHurt = false;
        startScale = transform.localScale;
        train = FindObjectOfType<TrainController2D>();
    }

    private void OnEnable()
    {
        startPos = transform.position;
    }

    public Cargo GetCargo()
    {
        return parent;
    }

    public void Scale()
    {
        transform.localScale = new Vector3(startScale.x + (scaleSize * weight), startScale.y + (scaleSize * weight));
        //transform.localScale = new Vector3(startScale.x * weight, startScale.y * weight);
    }

    public void SetParent(Cargo par)
    {
        parent = par;
    }

    public void Damage()
    {
        if (!hasHurt)
        {
            train.Damage(weight);
            hasHurt = true;
            //Destroy(gameObject);
            gameObject.SetActive(false);
        }
    }

    public void RestoreCrate()
    {
        parent.RestoreCrate(this);
    }
}