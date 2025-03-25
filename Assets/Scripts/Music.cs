using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    AudioSource audioSource;
    
    public CharacterData cd;
    public Menu mnu;
    public AudioClip[] musicTracks;



    void Start(){
        audioSource = GetComponent<AudioSource>(); 
        audioSource.clip = musicTracks[Random.Range(0, musicTracks.Length)];
        audioSource.Play();}
    
    void Update(){
        if (cd.paused || mnu.freeze){
            audioSource.Pause();
        }
        else {audioSource.UnPause();}
    }
}
