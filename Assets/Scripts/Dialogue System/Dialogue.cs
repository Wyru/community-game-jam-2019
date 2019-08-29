﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class DialogueData{
    public Sprite portrait;
    public string name;

    [TextArea]
    public string text;
}
public class Dialogue : MonoBehaviour
{
    public bool canReplay;
    public bool notCloseAfterEnd;
    public DialogueData settings;

    [Space]
    public bool endEventDelay;

    public UnityEvent OnEndDialogue; 
    public void Play(){
        DialogueController.PlayDialogue(this);
    }

    public void OnEnd(){
        OnEndDialogue?.Invoke();
    }
}