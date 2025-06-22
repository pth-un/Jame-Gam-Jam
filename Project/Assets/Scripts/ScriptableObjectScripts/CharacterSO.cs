using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "CharacterSO", menuName = "Scriptable Objects/Dialugue/CharacterSO")]
public class CharacterSO : ScriptableObject
{
    public string charName;
    public List<Texture2D> ExpresionList;

    public List<AudioClip> speachClips;
}
