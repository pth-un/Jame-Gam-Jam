using System;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "DialogueSO", menuName = "Scriptable Objects/Dialugue/DialogueSO")]
public class DialogueSO : ScriptableObject
{
    public List<DialogueLine> lines;
}

[Serializable]
public class DialogueLine
{
    public string text;
    public CharacterSO character;
    public int expression;
    public bool isLeft;
}
