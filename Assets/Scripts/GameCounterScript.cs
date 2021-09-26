using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameCounterScript : MonoBehaviour
{
    [SerializeField] Text text;
    int count = 0;

    public int AddCount(int addNum = 1)
    {
        count += addNum;
        ChangeText();
        return count;
    }
    public static GameCounterScript operator ++(GameCounterScript gameCounter)
    {
        gameCounter.AddCount();
        return gameCounter;
    }

    void ChangeText()
    {
        text.text = count.ToString();
    }

}
