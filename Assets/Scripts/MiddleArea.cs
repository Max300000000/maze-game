using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiddleArea : MonoBehaviour
{
    public PlayerCollect playerCollect;
    public Menu menu;
    public Transform player;


    void Start()
    {
        playerCollect = GameObject.Find("ScriptHolder").GetComponent<PlayerCollect>();
        menu = GameObject.Find("ScriptHolder").GetComponent<Menu>();
        player = GameObject.Find("Player").transform;
    }



    
    void OnTriggerEnter2D(Collider2D other){
        if (other.CompareTag("Player") && playerCollect.keysCollected >= 3){
            menu.Win();
        }
    }
}
