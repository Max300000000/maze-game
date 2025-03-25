using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI timerText;      
    public float timerDuration = 60f; 
    public float realTime = 60f;
    public Menu menu;

    float timeRemaining;

    void Start()
    {
        timeRemaining = timerDuration; 
        UpdateTimerDisplay();          
    }

    void Update()
    {
        if (menu.freeze) {return;}
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime * timerDuration / realTime; 
            UpdateTimerDisplay();          
        }
        else
        {
            timeRemaining = 0; 
            UpdateTimerDisplay(); 
            menu.Lose();

        }
    }

    void UpdateTimerDisplay()
    {
        timerText.text = Mathf.CeilToInt(timeRemaining).ToString();
    }
}
