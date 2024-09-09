using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnDestroyPulpit : MonoBehaviour
{
    private float destroyTime;
    private GameObject lastpulpit;
    void Start(){
        lastpulpit = null;
    }
    // public void Initialize(float time)
    // {
    //     destroyTime = time;
    //     Invoke(nameof(DestroyPulpit), destroyTime);
        
    // }

    // void DestroyPulpit()
    // {
    //     FindObjectOfType<GameManagerScript>().SpawnPulpit();
    // }

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

