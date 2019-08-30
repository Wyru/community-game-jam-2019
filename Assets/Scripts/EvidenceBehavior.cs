using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvidenceBehavior : MonoBehaviour
{
    public Evidence evidence;
    bool alreadyAdded;

    public void Inspect()
    {
        if (PlayerKnowledgeController.windowOpen || DialogueOptionsController.Choosing)
        {
            return;
        }
        DialogueController.ShowMessage(evidence.description);
    }

    public void Memorize()
    {
        if (PlayerKnowledgeController.windowOpen || DialogueOptionsController.Choosing)
        {
            return;
        }

        if (!alreadyAdded)
        {
            alreadyAdded = true;
            PlayerKnowledgeController.AddEvidence(evidence);
        }
    }
}
