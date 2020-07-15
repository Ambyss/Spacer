using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveGame : MonoBehaviour
{
    public void SaveScore(int score)
    {
        PlayerPrefs.SetInt("Score", score);
    }

    public int GetScore()
    {
        return PlayerPrefs.GetInt("Score");
    }


}
