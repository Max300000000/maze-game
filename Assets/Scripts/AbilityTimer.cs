using UnityEngine;
using TMPro;

public class AbilityTimer : MonoBehaviour
{
    public TextMeshProUGUI timerText;   
    public GameObject menu;
    public float timeRemaining;
    public Menu mnu;


    void Update()
    {
        if (timeRemaining > 0 && !mnu.freeze)
        {
            menu.SetActive(true);
            timeRemaining -= Time.deltaTime; 
            UpdateTimerDisplay();          
        }
        else
        {
            menu.SetActive(false);
        }
    }

    void UpdateTimerDisplay()
    {
        timerText.text = Mathf.CeilToInt(timeRemaining).ToString();
    }
}
