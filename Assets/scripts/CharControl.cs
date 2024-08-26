using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharControl : MonoBehaviour
{
    public float setSpeed = 3f;
    private float fallThreshold = -1f;
    private GameManagerScript _letsManageGame;
    void Start()
    {
        _letsManageGame = FindObjectOfType<GameManagerScript>();
    }

    void Update()
    {
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"),0.0f,Input.GetAxis("Vertical"));
        transform.Translate(movement * setSpeed * Time.deltaTime,Space.World);

        if(transform.position.y < fallThreshold){
            _letsManageGame.GameOver();
        }
    }

}
