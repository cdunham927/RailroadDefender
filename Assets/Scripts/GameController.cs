using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameController : MonoBehaviour
{
    public float dmg = 5;
    public PlayerController2D[] players;
    [SerializeField]
    int curPlayer = 0;
    CameraFollow2D cam;
    UIController ui;

    private void OnEnable()
    {
        ui = FindObjectOfType<UIController>();
        cam = FindObjectOfType<CameraFollow2D>();
        players = FindObjectsOfType<PlayerController2D>();
        curPlayer = 0;
        players[0].canMove = true;
        ui.SwitchUI(players[0]);
        cam.SetTarget(players[0].transform);
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

        if (players.Length > 1 && Input.GetKeyDown(KeyCode.T))
        {
            players[curPlayer].canMove = false;
            curPlayer = (curPlayer + 1) % players.Length;
            ui.SwitchUI(players[curPlayer]);
            cam.SetTarget(players[curPlayer].transform);
            players[curPlayer].canMove = true;
        }
    }
}
