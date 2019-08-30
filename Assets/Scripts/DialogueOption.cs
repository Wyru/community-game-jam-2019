using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class ShowEvidence{
    public Evidence evidence;

    [Space]
    public UnityEvent OnChoose;
}

[System.Serializable]
public class Option {
    [Header("Option Base Settings")]
    public string text;
    [Space]
    public UnityEvent OnChoose;

    public Sprite icon;

    [Header("Evidence Settings")]
    public bool isShowEvidence;

    public List<ShowEvidence> evidences;

}


public class DialogueOption : MonoBehaviour
{
    public bool cannotReplay;
    [HideInInspector] public bool played;

    public List<Option> options;
    
    [HideInInspector] public int lastChoose;

    public void Show(){
        DialogueOptionsController.ShowDialogueOpition(this);
    }
}
