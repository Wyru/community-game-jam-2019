using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactible : MonoBehaviour
{
    public UnityEvent OnClick;
    public void Click(){

        if (PlayerKnowledgeController.windowOpen || DialogueOptionsController.Choosing || DialogueController.dialogueOpen)
            return;
        
        OnClick?.Invoke();
    }
}
