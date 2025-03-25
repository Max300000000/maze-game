using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Ability : MonoBehaviour
{
    public List<float> durations = new List<float> { 1f, 3f, 5f, 4f };
    public List<float> cooldowns = new List<float> { 5f, 7f, 6f, 8f };

    public CharacterData cD;
    public PlayerMovement movement;

    public AbilityTimer[] timers;
    AbilityTimer cooldown;
    AbilityTimer duration;

    public GameObject arrow;

    Coroutine myRoutine;
    List<Action> allAbilities;
    List<Action> allAbilities2;
    
    float lastUse = -100f;
    float forgiveness = 0.5f;
    

    void Start(){
        allAbilities = new List<Action> { Bomb, Fade, Speed, Reveal };
        allAbilities2 = new List<Action> { DropBomb, RemoveFade, RemoveSpeed, StopReveal };

        timers = GameObject.Find("ScriptHolder").GetComponents<AbilityTimer>();
        cooldown = timers[0];
        duration = timers[1];
    }

    void Update(){
        if (Input.GetKey(KeyCode.Space) && Timer()) {UseAbility();}
        else if (cD.characterIndex == 1 && Input.GetKey(KeyCode.Space) && CanEnd()) {EndAbility();}}

    void UseAbility(){
        lastUse = Time.time;
        allAbilities[cD.characterIndex]();
        duration.timeRemaining = durations[cD.characterIndex];

        myRoutine = StartCoroutine(EndAbility(durations[cD.characterIndex]));
    }

    void EndAbility(){
        StopCoroutine(myRoutine);
        allAbilities2[cD.characterIndex]();
        lastUse -= duration.timeRemaining;
        duration.timeRemaining = 0f;
        cooldown.timeRemaining = cooldowns[cD.characterIndex];
    }
    
    bool Timer(){   //duration + cooldown
        return (Time.time - lastUse >= cooldowns[cD.characterIndex] + durations[cD.characterIndex]);
    }

    bool CanEnd(){
        if (Time.time - lastUse <= forgiveness) {return false;}
        return !(Time.time - lastUse >= durations[cD.characterIndex]);
    }


    //Abilities

    void Bomb(){
        return;
    }

    void Fade(){
        movement.playerCollider.enabled = false;
        SetTransparency(0.5f);

    }
    
    void Speed(){
        int speed = 4;
        movement.moveSpeed += speed;
    }

    void Reveal(){
        Instantiate(arrow, transform.position, transform.rotation);
        return;
    }




    

    IEnumerator EndAbility(float time){
        yield return new WaitForSeconds(time);
        allAbilities2[cD.characterIndex]();
        cooldown.timeRemaining = cooldowns[cD.characterIndex];
    }
    
    void DropBomb(){
        float range = 3;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(movement.tf.position, range);
        foreach (Collider2D collider in colliders){
            if (collider.CompareTag("Wall")) {Destroy(collider.gameObject);}}
    }

    void RemoveFade(){
        movement.playerCollider.enabled = true;
        SetTransparency(1);

    }

    void RemoveSpeed(){
        movement.moveSpeed -= 4;
    }

    void StopReveal(){
        GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag("Arrow");
        foreach (GameObject obj in objectsWithTag)
        {
            Destroy(obj);
        }
    }









    void SetTransparency(float alpha)
    {
        Color currentColor = movement.sr.color;
        
        // Set the alpha value to the desired transparency (0 = fully transparent, 1 = fully opaque)
        currentColor.a = alpha;

        movement.sr.color = currentColor;
    }


}
