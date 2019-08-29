using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


[System.Serializable]
public class Option {
    [Header("Option Base Settings")]

    public string text;
    [Space]
    public UnityEvent OnChoose;

    [Header("Evidence Settings")]
    public bool needEvidence;

    public string evidenceName;

    [Space]
    public UnityEvent OnChooseWrongEvidence;

}


public class DialogueOption : MonoBehaviour
{
    public List<Option> options;

    public void Show(){
        DialogueOptionsController.ShowDialogueOpition(this);
    }
}
