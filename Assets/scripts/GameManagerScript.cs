using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerScript : MonoBehaviour
{
    public GameObject pulpitPrefab;
    public GameObject doofusPlayer;
    public Transform pulpitParent;

    [SerializeField]private AudioSource scoreIncreasedFX;
    [SerializeField]private AudioSource musicGame;
    public RetryScript scriptRetry;
    public UIManagerScript uiManager;

    private int score = 0;
    public Vector3 lastPulpitPosition;
    private float pulpitSpawnTime;
    private float minPulpitDestroyTime;
    private float maxPulpitDestroyTime;

    private int rndCountDir = 0;
    private int direction = -1;
    private GameObject currentPulpit;
    
    void Start()
    {
        musicGame.PlayDelayed(1);
        LoadGameSettings();
        InitializeFirstPulpit();
        StartCoroutine(ManagePulpits());
    }

    void LoadGameSettings()
    {
        pulpitSpawnTime = 2.5f;
        minPulpitDestroyTime = 4f;
        maxPulpitDestroyTime = 5f;

    }
    void InitializeFirstPulpit()
    {
        currentPulpit = GameObject.FindWithTag("Pulpit");
        if(currentPulpit!=null){
            lastPulpitPosition = currentPulpit.transform.position;
            float destroyTime = Random.Range(minPulpitDestroyTime, maxPulpitDestroyTime);

            Text FirstSecondsText = currentPulpit.transform.Find("Canvas/sec").GetComponent<Text>();
            Text FirstMilliText = currentPulpit.transform.Find("Canvas/milli").GetComponent<Text>();            
            
            StartCoroutine(UpdateTimer(FirstSecondsText, FirstMilliText, destroyTime));
            StartCoroutine(DestroyPulpitAfterTime(currentPulpit, destroyTime));
        }
    }

    IEnumerator ManagePulpits()
    {
        while (true)
        {
            yield return new WaitForSeconds(pulpitSpawnTime);
            SpawnPulpit();
        }
    }

    public void SpawnPulpit()
    {
        Vector3 spawnPosition = lastPulpitPosition;
        if(rndCountDir == 0){
            direction = Random.Range(0, 4); // 0 = North, 1 = East, 2 = South, 3 = West
            
            switch (direction)
            {
                case 0: // North
                    spawnPosition += new Vector3(0, 0, 9);
                    break;
                case 1: // East
                    spawnPosition += new Vector3(9, 0, 0);
                    break;
                case 2: //South
                    spawnPosition += new Vector3(0, 0, -9);
                    break;
                case 3: // West
                    spawnPosition += new Vector3(-9, 0, 0);
                    break;
            }
            
            lastPulpitPosition = spawnPosition;

            GameObject pulpit_ = Instantiate(pulpitPrefab, spawnPosition, Quaternion.identity, pulpitParent);

            Text secondsText_ = pulpit_.transform.Find("Canvas/sec").GetComponent<Text>();
            Text milliText_ = pulpit_.transform.Find("Canvas/milli").GetComponent<Text>();

            currentPulpit = pulpit_;
            float destroyTime_ = Random.Range(minPulpitDestroyTime, maxPulpitDestroyTime);
            StartCoroutine(UpdateTimer(secondsText_, milliText_, destroyTime_));
            StartCoroutine(DestroyPulpitAfterTime(pulpit_, destroyTime_));
            rndCountDir = 1;
            return;
        }

        int previousDirection = direction;
        
        List<int> possibleDirections = new List<int> { 0, 1, 2, 3 };
        if (previousDirection != -1)
        {
            possibleDirections.Remove(previousDirection);
        }

        int newDirection = possibleDirections[Random.Range(0, possibleDirections.Count)];

        switch (newDirection)
        {
            case 0: // North
                spawnPosition += new Vector3(0, 0, 9);
                break;
            case 1: // East
                spawnPosition += new Vector3(9, 0, 0);
                break;
            case 2: // South
                spawnPosition += new Vector3(0, 0, -9);
                break;
            case 3: // West
                spawnPosition += new Vector3(-9, 0, 0);
                break;
        }
        direction = newDirection;
        lastPulpitPosition = spawnPosition;

        GameObject pulpit = Instantiate(pulpitPrefab, spawnPosition, Quaternion.identity, pulpitParent);
        currentPulpit = pulpit;

        Text secondsText = pulpit.transform.Find("Canvas/sec").GetComponent<Text>();
        Text milliText = pulpit.transform.Find("Canvas/milli").GetComponent<Text>();

        float destroyTime = Random.Range(minPulpitDestroyTime, maxPulpitDestroyTime);
        StartCoroutine(UpdateTimer(secondsText, milliText, destroyTime));
        StartCoroutine(DestroyPulpitAfterTime(pulpit, destroyTime));
    }

    private IEnumerator UpdateTimer(Text secondsText, Text milliText, float destroyTime)
    {
        float timeRemaining = destroyTime;

        while (timeRemaining > 0)
        {
            if (secondsText == null || milliText == null)
            {
                // If the Text components are null, exit the coroutine early
                yield break;
            }

            timeRemaining -= Time.deltaTime;

            int seconds = Mathf.FloorToInt(timeRemaining);
            int milliseconds = Mathf.FloorToInt((timeRemaining - seconds) * 100);

            secondsText.text = seconds.ToString() + ":";
            milliText.text = milliseconds.ToString();

            yield return null;
        }

        if (secondsText != null)
        {
            secondsText.text = "0:";
        }

        if (milliText != null)
        {
            milliText.text = "00";
        }
    }
    
    IEnumerator DestroyPulpitAfterTime(GameObject pulpit, float time)
    {
        yield return new WaitForSeconds(time);
        if (pulpit != null)
        {
            Destroy(pulpit);
        }
    }

    public void UpdateScore()
    {
        score+=1;
        uiManager.UpdateScore(score);

        if(scoreIncreasedFX!=null){
            scoreIncreasedFX.Play();
        }

        if (score >= 50)
        {
            GameOver();
        }
    }

    public void GameOver()
    {
        int CurrScore = score;
        musicGame.Stop();
        scriptRetry.ShowGameOverScreen(CurrScore);
    }
}

