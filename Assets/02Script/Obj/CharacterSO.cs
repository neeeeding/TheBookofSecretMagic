using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterSO", menuName = "SO/CharacterSO")]
public class CharacterSO : ScriptableObject
{
    public CharacterName name;

    public PlayerJob job;
}

public enum CharacterName
{
    resty,
    chris,
    theo,
    noah,
    nia,
    villain,
    harry,
    danie,
    pio
}
