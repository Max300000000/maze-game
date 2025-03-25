using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Message2 : MonoBehaviour
{
    public CharacterData cd;
    public PlayerCollect pc;
    public GameObject canuse;
    public GameObject getback;
    public GameObject collect;
    public GameObject cancel;
    public GameObject arrow;

    public GameObject timer1;
    public GameObject timer2;


    
    void Update()
    {
        if (cd.u2 == true && !timer1.activeSelf && !timer2.activeSelf){
            canuse.SetActive(true);
        }
        else {canuse.SetActive(false);}

        if (pc.keysCollected >= 3){
            getback.SetActive(true);
            arrow.SetActive(true);
            collect.SetActive(false);
        }
        else {getback.SetActive(false); arrow.SetActive(false); collect.SetActive(true);}

        if (cd.characterIndex == 1){
            cancel.SetActive(true);
        }
        else {cancel.SetActive(false);}
    }
}
