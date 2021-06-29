using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    //Which characters we need to spawn in each level
    public PlayerController2D players;
    //Current level/difficulty
    public int level;
    //1 could be easy space, 2, could be nebulas, 3 could be near black holes, 4 could be near a sun or something
    public int levelType;
}