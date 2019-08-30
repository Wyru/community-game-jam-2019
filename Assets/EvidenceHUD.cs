using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EvidenceHUD : MonoBehaviour
{
    public Evidence evidence;

    public Image icon;

    public void MouseEnter() {
        PlayerKnowledgeController.Instance.ShowEvidenceDescription(evidence);
    }

    public void MouseExit() {
        PlayerKnowledgeController.Instance.HideEvidenceDescription();
    }


    public void OnClick(){
        PlayerKnowledgeController.Instance.SelectEvidence(evidence);
    }

}
