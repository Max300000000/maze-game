using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    Transform movingObject; 
    Transform stationaryTarget;
    [SerializeField] bool orange;

    PlayerCollect pc;

    void Start(){
        pc = GameObject.Find("ScriptHolder").GetComponent<PlayerCollect>();
        movingObject = GameObject.Find("Player").transform;
        if (orange) {stationaryTarget = FindClosest().transform;}
        else {stationaryTarget = GameObject.Find("KeyCheck").transform;}
    }
        
        
    
    GameObject FindClosest()
    {
        pc = GameObject.Find("ScriptHolder").GetComponent<PlayerCollect>();
        GameObject[] objects = GameObject.FindGameObjectsWithTag("Key");
        Vector3 currentPosition = transform.position;

        List<GameObject> sortedObjects = objects
            .OrderBy(obj => Vector3.Distance(currentPosition, obj.transform.position))
            .ToList();

        if (pc.keysCollected != 3) {return sortedObjects[pc.keysCollected];}
        else {return GameObject.Find("KeyCheck");}
    }
        
        
    
    void Update()
    {
        Vector3 direction = stationaryTarget.position - movingObject.position;
        direction.Normalize(); 
            
        transform.position = movingObject.position + direction * 2;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle - 90); 
    }
}
