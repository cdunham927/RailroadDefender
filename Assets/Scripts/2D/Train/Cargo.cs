using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cargo : MonoBehaviour
{
    public bool targeted = false;

    public List<Crate> crates = new List<Crate>();
    public float maxWeight = 10.0f;
    [SerializeField]
    float curWeight = 0.0f;
    public Crate cratePrefab;
    public float minCrateWeight = 1.0f;
    public float maxCrateWeight = 6.0f;
    [SerializeField]
    float curMax;

    TrainController2D train;
    float xPos;
    float yPos;
    public Transform[] spawnPos;
    int curInd = 0;

    GameController cont;

    private void OnEnable()
    {
        targeted = false;
    }

    private void Awake()
    {
        curInd = 0;
        curMax = maxCrateWeight;
        train = FindObjectOfType<TrainController2D>();
        cont = FindObjectOfType<GameController>();

        while (curWeight < maxWeight)
        {
            if (curWeight > maxWeight) break;
            else 
            {
                Crate c = Instantiate(cratePrefab, spawnPos[curInd].position, transform.rotation).GetComponent<Crate>();
                crates.Add(c);

                float w = Mathf.RoundToInt(Random.Range(minCrateWeight, curMax));
                c.weight = w;
                c.Scale();
                c.SetParent(this);
                curWeight += w;
                curMax -= w / 2;
                curMax = Mathf.Clamp(curMax, minCrateWeight, maxWeight);
                if (curMax > maxWeight) curMax = maxWeight;
                curInd++;


                cont.AddTarget(c.transform, cont.targets);
                cont.AddTarget(c.transform, cont.trainTargets);
            }

        }
    }

    public void TakeCrate(Crate c)
    {
        crates.Remove(c);
        train.Damage(c.weight);
    }

    public Crate GetCrate()
    {
        if (crates.Count > 0)
        {
            int index = Random.Range(0, crates.Count);
            Crate c = crates[index];
            crates.Remove(c);
            return c;
        }
        return null;
    }

    public void RestoreCrate(Crate c)
    {
        crates.Add(c);
    }
}
