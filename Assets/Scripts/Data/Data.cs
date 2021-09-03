using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Data
{
    public bool easyMode;
    public int sceneNumber;
    //Save info about currently completed levels, weapons unlocked, etc etc
    //player position
    public float[] playerPosition;

    //Might need music and sound volume in here too
    //
    //
    //
    //

    public Data(bool em = true, int sceneNum = 0, float xPos = 0, float yPos = 0)
    {
        //Easy mode on or off
        easyMode = em;
        //Scene number
        sceneNumber = sceneNum;
        //New float array
        playerPosition = new float[2];
        playerPosition[0] = xPos;
        playerPosition[1] = yPos;
    }
}