using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

enum DialogueState
{
    NotRunning,
    Typing,

    SpeedUp,

    FinishTyping
}

public class DialogueController : MonoBehaviour
{
    public static bool dialogueOpen;
    static DialogueController Instance;
    public float timeBtwLetters;
    public Animator dialogueBoxAnimator;
    public TextMeshProUGUI dialogueBoxText;
    public TextMeshProUGUI dialogueBoxName;
    public Image dialoguePortrait;

    Dialogue currentDialogue;
    DialogueState state;

    new string name = "";
    string textToType = "";


    void Start()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (state == DialogueState.Typing)
            {
                state = DialogueState.SpeedUp;
            }
            else if (state == DialogueState.FinishTyping)
            {
                StartCoroutine("EndDialogue");
            }
        }
    }

    public static void PlayDialogue(Dialogue dialogue)
    {
        dialogueOpen = true;

        Instance.currentDialogue = dialogue;
        Instance.name = dialogue.settings.name;
        Instance.textToType = dialogue.settings.text;
        Instance.dialoguePortrait.sprite = dialogue.settings.portrait;
        Instance.dialoguePortrait.gameObject.SetActive(Instance.dialoguePortrait.sprite != null);
        Instance.StartDialogue();
    }

    public static void ShowMessage(string text)
    {
        dialogueOpen = true;

        Instance.name = "";
        Instance.textToType = text;
        Instance.dialoguePortrait.gameObject.SetActive(false);

        Instance.StartDialogue();
    }
    public void ShowMessageNotStatic(string text)
    {
        dialogueOpen = true;

        name = "";
        textToType = text;
        dialoguePortrait.gameObject.SetActive(false);

        StartDialogue();
    }

    void StartDialogue()
    {
        StartCoroutine("InnerStartDialogue");
    }

    IEnumerator InnerStartDialogue()
    {
        if (state == DialogueState.NotRunning && !DialogueOptionsController.Choosing)
        {
            if (currentDialogue!=null && currentDialogue.cannotReplay && currentDialogue.played)
            {
                StartCoroutine("EndDialogue");
            }
            else
            {
                dialogueBoxName.SetText(name);
                ShowDialogueBox();
                yield return new WaitForSeconds(.5f);
                StartCoroutine("TypeText");
            }
        }
    }

    IEnumerator EndDialogue()
    {
        if (currentDialogue != null && currentDialogue.cannotReplay && currentDialogue.played)
        {
            currentDialogue.OnEnd();
        }
        else
        {
            if (currentDialogue == null || !currentDialogue.notCloseAfterEnd)
                HideDialogueBox();

            yield return new WaitForSeconds(.3f);

            state = DialogueState.NotRunning;

            if (currentDialogue == null || !currentDialogue.notCloseAfterEnd)
                dialogueBoxText.SetText("");

            if (currentDialogue != null)
                currentDialogue.played = true;
        }
        dialogueOpen = false;
        currentDialogue?.OnEnd();
    }

    IEnumerator TypeText()
    {
        state = DialogueState.Typing;

        char[] textArray = textToType.ToCharArray();

        string text = "";

        for (int i = 0; i < textArray.Length; i++)
        {
            text += textArray[i];
            dialogueBoxText.SetText(text);

            if (state == DialogueState.SpeedUp)
            {
                for (int j = i+1; j < textArray.Length; j++)
                {
                    text += textArray[j];
                    dialogueBoxText.SetText(text);
                }

                break;
            }
            else
                yield return new WaitForSeconds(timeBtwLetters);

        }

        state = DialogueState.FinishTyping;
    }

    void ShowDialogueBox()
    {
        dialogueBoxAnimator.SetBool("Show", true);
    }

    void HideDialogueBox()
    {
        dialogueBoxAnimator.SetBool("Show", false);
    }


}
