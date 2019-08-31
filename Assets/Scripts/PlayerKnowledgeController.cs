using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;

public class PlayerKnowledgeController : MonoBehaviour
{
    public Animator notification;
    public static bool windowOpen;
    public static PlayerKnowledgeController Instance;
    List<Evidence> evidences;

    Dictionary<string,List<string>> suspectsInfo;


    public bool chooseEvidence;
    public class ChooseEvidenceCallback : UnityEvent<Evidence> {}
    [HideInInspector] public ChooseEvidenceCallback OnChooseEvidence ;
    public GameObject EvidencesWindow;
    public Transform evidencesContainer;

    public GameObject EvidenceHUDPrefab;


    public TextMeshProUGUI evidenceDescription;


    private void Start() {
        Instance = this;
        evidences = new List<Evidence>();
        suspectsInfo = new Dictionary<string, List<string>>();
        OnChooseEvidence = new ChooseEvidenceCallback();
    }

    public static void AddEvidence(Evidence evidence){
        Instance.ShowNotification();
        Instance.evidences.Add(evidence);
    }

    void ShowNotification(){
        StartCoroutine("_ShowNotification");
    }

    IEnumerator _ShowNotification(){
        yield return new WaitForSeconds(1.5f);
        notification.SetTrigger("Show");
        notification.GetComponent<AudioSource>().Play();
    }

    public static void AddSuspectInfo(string name, string info){
        List<string> list;
        if(Instance.suspectsInfo.TryGetValue(name, out list)){
            list.Add(info);
        }
        else{
            Instance.suspectsInfo.Add(name, new List<string>());
        }
    }


    public void ShowEvidences(){
        Debug.Log("open evidences window");
        evidenceDescription.SetText("");

        foreach (EvidenceHUD item in evidencesContainer.GetComponentsInChildren<EvidenceHUD>())
        {
            Destroy(item.gameObject);
        }

        foreach (Evidence evidence in evidences)
        {
            GameObject go = Instantiate(EvidenceHUDPrefab, evidencesContainer);

            go.GetComponentInChildren<TextMeshProUGUI>().SetText(evidence.name);
            go.GetComponent<Image>().sprite = evidence.icon;
            go.GetComponent<EvidenceHUD>().evidence = evidence;

        }

        EvidencesWindow.SetActive(true);

    }

    public void CloseEvidences(){
        EvidencesWindow.SetActive(false);
        if(chooseEvidence)
        {
            OnChooseEvidence?.Invoke(null);
            OnChooseEvidence = new ChooseEvidenceCallback();
            chooseEvidence = false;
        }
    }

    public void ShowEvidenceDescription(Evidence evidence){
        evidenceDescription.SetText(evidence.fakeDescription);
    }

    public void HideEvidenceDescription(){
        evidenceDescription.SetText("");
    }

    public void ShowSelectEvidenceWindow(){
        ShowEvidences();
        chooseEvidence = true;
    }

    public void SelectEvidence(Evidence evidence){
        if(chooseEvidence){
            OnChooseEvidence?.Invoke(evidence);
            OnChooseEvidence = new ChooseEvidenceCallback();
            chooseEvidence = false;
            HideEvidenceDescription();
            CloseEvidences();
        }
    }




}
