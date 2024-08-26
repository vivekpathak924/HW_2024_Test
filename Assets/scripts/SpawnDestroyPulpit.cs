using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnDestroyPulpit : MonoBehaviour
{
    private float destroyTime;
    private GameObject lastpulpit;
    void Start(){
        lastpulpit = null;
    }
    public void Initialize(float time)
    {
        destroyTime = time;
        Invoke(nameof(DestroyPulpit), destroyTime);
    }

    void DestroyPulpit()
    {
        GameObject.FindObjectOfType<GameManagerScript>().SpawnPulpit();
        Destroy(gameObject);
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            if(col.gameObject!=lastpulpit){
                GameManagerScript gameManager = FindObjectOfType<GameManagerScript>();
                gameManager.UpdateScore();
                lastpulpit = col.gameObject;
            }
        }
    }
}

