using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManagerScript : MonoBehaviour
{
    public Text CountScore;

    public void UpdateScore(int score)
    {
        CountScore.text = "" + score;
    }
}

