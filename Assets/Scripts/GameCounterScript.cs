using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCounterScript : MonoBehaviour
{
    private int count = 0;

    public int AddCount(int addNum = 1)
    {
        count += addNum;
        
        return count;
    }

    public static GameCounterScript operator ++(GameCounterScript gameCounter)
    {
        gameCounter.AddCount();
        return gameCounter;
    }
}
