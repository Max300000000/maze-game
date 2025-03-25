using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyFollow : MonoBehaviour
{
    public float speed = 2;
    public float distance = 2;
    public float extraDistance = 1;
    public PlayerCollect playerCollect;

    Transform target;
    bool collected = false;
    float myDistance;
    

    void Start()
    {
        playerCollect = GameObject.Find("ScriptHolder").GetComponent<PlayerCollect>();
        target = GameObject.Find("Player").transform;
    }

    void Update()
    {
        if (collected) {Follow();}
    }

    void Follow()
    {
        if ((target.position - transform.position).magnitude >= myDistance)
        {
            transform.position = Vector3.Lerp(transform.position, target.position, speed * Time.deltaTime);
            //transform.position = Vector3.SmoothDamp(transform.position, target.position, speed)
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !collected)
        {
            myDistance = distance + extraDistance * playerCollect.keysCollected;
            collected = true;
            playerCollect.keysCollected += 1;
        }
    }
}
