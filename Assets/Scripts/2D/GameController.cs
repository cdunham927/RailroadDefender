using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public float dmg = 5;
    public PlayerController2D[] players;
    [SerializeField]
    int curPlayer = 0;
    CameraFollow2D cam;
    Camera camComp;
    UIController ui;
    public int maxPlayers;
    //[HideInInspector]
    public List<Transform> targets = new List<Transform>();
    //[HideInInspector]
    public List<Transform> playerTargets = new List<Transform>();
    //[HideInInspector]
    public List<Transform> trainTargets = new List<Transform>();

    //Spawning enemies
    public bool canSpawn;
    public GameObject[] enemyArray;
    //public float timeBetweenSpawns;
    //float spawnCools;
    Vector2 bounds;
    Vector3 spawnPoint;
    //public float radius;
    public float sizeX;
    public float sizeY;
    public int curNumEnemies = 0;

    //Wave Spawning
    public float timeBetweenSpawnsLow;
    public float timeBetweenSpawnsHigh;
    public float timeBetweenWavesLow;
    public float timeBetweenWavesHigh;
    public int enemiesPerWaveLow = 2;
    public int enemiesPerWaveHigh = 5;
    public float timeBetweenEnemies = 1f;
    float cools;
    float waveCools;
    int enemiesToSpawn;
    int enemiesThisWave = 0;

    public Text waveText;
    public int wavesThisLevel;
    public int curWave;

    private void OnEnable()
    {
        curNumEnemies = FindObjectsOfType<EnemyController>().Length;
        ui = FindObjectOfType<UIController>();
        cam = FindObjectOfType<CameraFollow2D>();
        camComp = cam.GetComponent<Camera>();
        players = FindObjectsOfType<PlayerController2D>();
        curPlayer = 0;
        players[0].canMove = true;
        ui.SwitchUI(players[0]);
        cam.SetTarget(players[0].transform);
    }

    public void DeadEnemy(int n, GameObject en)
    {
        curNumEnemies += n;
        //Debug.Log("Enemies left: " + curNumEnemies, en);
    }

    public void AddTarget(Transform t, List<Transform> ls)
    {
        if (!ls.Contains(t)) ls.Add(t);
    }

    public Transform GetRandomTarget(List<Transform> ls)
    {
        int ind = Random.Range(0, ls.Count);
        //Debug.Log(ls[ind]);
        return ls[ind];
    }

    public void RemoveTarget(Transform t, List<Transform> ls)
    {
        if (ls.Contains(t)) ls.Remove(t);
    }

    void SpawnEnemy()
    {
        bounds = camComp.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0f));
        int xx = Random.Range(0, 2);
        if (xx == 0)
        {
            //Spawns to the right of the player
            //spawnPoint = new Vector3(bounds.x + Random.Range(xRangeLow, xRangeHigh), Random.Range(-bounds.y + 1f, bounds.y - 1f), 0f);
            //Circle spawn point
            //spawnPoint = Random.insideUnitCircle * radius;
            spawnPoint = new Vector2(-sizeX / 2f - Random.Range(0f, 2f), Random.Range(-sizeY / 2, sizeY / 2));
        }
        else
        {
            //Spawns to the left of the player
            //spawnPoint = new Vector3(-bounds.x - Random.Range(xRangeLow, xRangeHigh), Random.Range(-bounds.y + 1f, bounds.y - 1f), 0f);
            //Circle spawn point
            //spawnPoint = Random.insideUnitCircle * radius;
            spawnPoint = new Vector2(sizeX / 2f + Random.Range(0f, 2f), Random.Range(-sizeY / 2, sizeY / 2));
        }
        //Spawn the enemy
        Instantiate(enemyArray[Random.Range(0, enemyArray.Length)], spawnPoint, Quaternion.identity);
        //We've spawned an enemy
        enemiesThisWave++;
        //Update cooldown
        cools = Random.Range(timeBetweenSpawnsLow, timeBetweenSpawnsHigh);
    }

    private void Update()
    {
        if (Application.isEditor)
        {
            if (Input.GetKeyDown(KeyCode.K))
            {
                var ob = FindObjectsOfType<MonoBehaviour>().OfType<IDamageable<float>>();
                foreach(IDamageable<float> d in ob)
                {
                    d.Damage(dmg);
                }
            }

            if (Input.GetKeyDown(KeyCode.L))
            {
                players[curPlayer].Damage(dmg);
            }
        }

        if (cools > 0) cools -= Time.deltaTime;

        if (cools <= 0 && canSpawn)
        {
            if (enemiesThisWave < enemiesToSpawn && curWave <= wavesThisLevel) SpawnEnemy();
        }

        if (waveCools > 0 && curNumEnemies <= 0) waveCools -= Time.deltaTime;

        if (waveCools <= 0 && curNumEnemies <= 0 && curWave < wavesThisLevel)
        {
            //Reset enemies spawned this wave
            enemiesThisWave = 0;
            curWave++;
            //Set enemies we need to spawn
            enemiesToSpawn = Random.Range(enemiesPerWaveLow, enemiesPerWaveHigh);

            //Set the time to the next wave
            waveCools = Random.Range(timeBetweenWavesLow, timeBetweenWavesHigh);
        }

        if (players.Length > 1 && Input.GetKeyDown(KeyCode.T))
        {
            players[curPlayer].canMove = false;
            curPlayer = (curPlayer + 1) % players.Length;
            ui.SwitchUI(players[curPlayer]);
            cam.SetTarget(players[curPlayer].transform);
            players[curPlayer].canMove = true;
        }

        waveText.text = "Wave " + curWave.ToString() + "\nEnemies left: " + curNumEnemies.ToString();
    }

    private void OnDrawGizmos()
    {
        //Gizmos.color = Color.yellow;
        //Gizmos.DrawWireSphere(Vector3.zero, radius);

        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(new Vector2(-sizeX / 2f, 0), new Vector2(-3f, sizeY));
        Gizmos.DrawWireCube(new Vector2(sizeX / 2f, 0), new Vector2(3f, sizeY));
    }
}
