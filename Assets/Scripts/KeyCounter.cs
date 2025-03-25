using UnityEngine;
using TMPro;  // For TextMeshProUGUI

public class KeyCounter : MonoBehaviour
{
    public PlayerCollect playerCollect;
    public TextMeshProUGUI itemCountText;

    void Start()
    {
        UpdateItemCountDisplay();
    }

    void Update(){
        UpdateItemCountDisplay();
    }

    void UpdateItemCountDisplay()
    {
        itemCountText.text = " x " + playerCollect.keysCollected;
    }
}
