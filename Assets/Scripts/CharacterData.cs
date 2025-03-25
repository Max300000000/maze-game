using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "CharacterData", menuName = "Character")]
public class CharacterData : ScriptableObject
{
    public int characterIndex = 0;
    public bool paused = false;
    public bool randomSettings = false;
    public bool u1 = false;   //costomization
    public bool u2 = false;   //gameplay
    public bool u3 = false;   //environment

    public List<List<bool>> usedSettings = new List<List<bool>> {};
}
