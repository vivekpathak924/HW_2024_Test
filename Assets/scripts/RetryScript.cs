using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RetryScript : MonoBehaviour
{
    public GameObject gameOverScreen;
    public Text scoreText;
    public void ShowGameOverScreen(int score)
    {
        scoreText.text = "" + score.ToString();
        gameOverScreen.SetActive(true);
    }
    public void RetryButtonPressed()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

