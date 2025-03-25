using System.Linq;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public CharacterData characterData;
    public List<GameObject> menus;
    public bool freeze = false;


    public void SelectCharacter(int cIndex) {characterData.characterIndex = cIndex;}
    public void Red() {SelectCharacter(0);}
    public void Green() {SelectCharacter(1);}
    public void Yellow() {SelectCharacter(2);}
    public void Orange() {SelectCharacter(3);}

    public void PlayGame() {RandomSettings(); freeze = false; if (characterData.u1 == false) {characterData.characterIndex = Random.Range(0,4); SceneManager.LoadScene("Maze");} else {SceneManager.LoadScene("Characters");}}

    public void Play2() {freeze = false; SceneManager.LoadScene("Maze");}

    public void PlayAgain() {RandomSettings(); freeze = false; Time.timeScale = 1f; if (characterData.u1 == false) {characterData.characterIndex = Random.Range(0,4); SceneManager.LoadScene("Maze");} else {SceneManager.LoadScene("Characters");}}

    public void QuitGame() {Application.Quit();}

    public void PauseGame() {if (characterData.paused) {Resume();} else {Pause();}}

    public void Win() {Enable(1); freeze = true;}

    public void Lose() {Enable(2); freeze = true;}

    public void Settings() {Enable(1);}

    public void Disablee() {Disable();}

    public void Tu1() {if (characterData.u1 == false) {characterData.u1 = true;} else {characterData.u1 = false;}}

    public void Tu2() {if (characterData.u2 == false) {characterData.u2 = true;} else {characterData.u2 = false;}}

    public void Tu3() {if (characterData.u3 == false) {characterData.u3 = true;} else {characterData.u3 = false;}}

    public void TRandomSettings() {if (characterData.randomSettings == false) {characterData.randomSettings = true;} else {characterData.randomSettings = false;}}




    void Pause() {Enable(0); characterData.paused = true; Time.timeScale = 0f;}

    void Resume() {Disable(); characterData.paused = false; Time.timeScale = 1f;}

    void Enable(int index){
        foreach(GameObject menu in menus){
            menu.SetActive(false);
        }
        menus[index].SetActive(true);
    }

    void Disable(){
        foreach(GameObject menu in menus){
            menu.SetActive(false);
        }
    }

    void RandomSettings(){
        if (characterData.randomSettings){   //8st
            List<bool> attempt = new List<bool> {};
            bool isEqual = false;
            if (characterData.usedSettings.Count == 8){characterData.usedSettings.Clear();}
            while (true){
                while (attempt.Count < 3){
                    int x = Random.Range(0, 2);
                    if (x == 1) {attempt.Add(true);} 
                    else {attempt.Add(false);}   
                }
                foreach (List<bool> usedAttempt in characterData.usedSettings){
                    if (attempt.SequenceEqual(usedAttempt)){isEqual = true;}
                }
                if (isEqual){attempt.Clear(); isEqual = false;}
                else {break;}
            }
            characterData.usedSettings.Add(attempt);
            characterData.u1 = characterData.usedSettings[characterData.usedSettings.Count - 1][0];
            characterData.u2 = characterData.usedSettings[characterData.usedSettings.Count - 1][1];
            characterData.u3 = characterData.usedSettings[characterData.usedSettings.Count - 1][2];
            Debug.Log(characterData.u1 + " " + characterData.u2 + " " + characterData.u3);
            return;
        }
    }
}
