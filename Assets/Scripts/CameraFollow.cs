using UnityEngine;

public class CameraFollow : MonoBehaviour 
{
    public Vector3 offset = new Vector3(0, 0, -10);
    GameObject player;

    void Start(){ 
        player = GameObject.Find("Player");
    }
    
    void Update() {
        transform.position = player.transform.position + offset;
    }
}
