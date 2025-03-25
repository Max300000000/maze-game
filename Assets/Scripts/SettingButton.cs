using UnityEngine;
using UnityEngine.UI;

public class SettingButton : MonoBehaviour
{
    public CharacterData characterData;
    [SerializeField] int index;

    Color trueColor = Color.green; 
    Color falseColor = Color.red;   

    Image buttonImage;

    void Start()
    {
        buttonImage = GetComponent<Image>();
    }

    void Update()
    {
        if (index == 1){SetColor(characterData.u1);}
        if (index == 2){SetColor(characterData.u2);}
        if (index == 3){SetColor(characterData.u3);}
        if (index == 4){SetColor(characterData.randomSettings);}
    }

    void SetColor(bool cd){
        if (cd){
            buttonImage.color = trueColor;
        }
        else {buttonImage.color = falseColor;}
    }
}
