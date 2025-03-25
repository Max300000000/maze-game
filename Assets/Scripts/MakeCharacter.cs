using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MakeCharacter : MonoBehaviour
{
    public List<GameObject> characters;
    public CharacterData characterData;

    private void Awake(){
        GameObject player = Instantiate(characters[characterData.characterIndex], transform.position, Quaternion.identity);
        player.name = "Player";
    }
}
