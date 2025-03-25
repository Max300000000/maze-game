using UnityEngine;

public class TAbility : MonoBehaviour
{
    public CharacterData cd;
    
    void Start()
    {
        Ability ability = GetComponent<Ability>();
        if (cd.u2) {ability.enabled = true;}
        else {ability.enabled = false;}
    }
}
