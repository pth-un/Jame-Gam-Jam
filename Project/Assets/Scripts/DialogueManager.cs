using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class DialogueManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI text;
    [SerializeField]
    private GameObject dialogueBox;
    [SerializeField]
    private RawImage rightImage;
    [SerializeField]
    private RawImage leftImage;

    private int currentLine = -1;
    private List<DialogueLine> lines;

    //public DialogueSO test;
    public void Start()
    {
        //StartDialogue(test);
    }

    public void StartDialogue(DialogueSO dialogue)
    {
        dialogueBox.SetActive(true);
        currentLine = -1;
        lines = dialogue.lines;
        NextLine();
    }

    public void NextLine()
    {
        currentLine++;
        if (currentLine >= lines.Count) {
            currentLine = -1;
            EndDialogue();
        }
        text.text = lines[currentLine].text;

        if (lines[currentLine].isLeft)
        {
            rightImage.gameObject.SetActive(false);
            leftImage.texture = lines[currentLine].character.ExpresionList[lines[currentLine].expression];
            leftImage.gameObject.SetActive(true);
        }
        else
        {
            leftImage.gameObject.SetActive(false);
            rightImage.texture = lines[currentLine].character.ExpresionList[lines[currentLine].expression];
            rightImage.gameObject.SetActive(true);
        }
    }

    private void Update()
    {
        if (Input.anyKeyDown && currentLine > -1)
        {
            NextLine();
        }
    }

    private void EndDialogue()
    {
        dialogueBox.gameObject.SetActive(false);
    }
}
