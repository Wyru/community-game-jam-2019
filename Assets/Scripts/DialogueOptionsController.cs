using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueOptionsController : MonoBehaviour
{
    public static bool Choosing;
    static DialogueOptionsController Instance;
    public Animator optionWindowAnimator;
    public Transform optionContainer;
    public GameObject optionPrefab;

    DialogueOption currentOptions;


    // Start is called before the first frame update
    void Start()
    {
        Choosing = false;
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log($"Options Open = {Choosing}");
    }


    public static void ShowDialogueOpition(DialogueOption options)
    {
        if (!Choosing)
        {
            Choosing = true;
            Instance.currentOptions = options;
            Instance.ShowOptions();
        }
    }


    void ShowOptions()
    {
        Debug.Log("Show options");

        if (currentOptions.cannotReplay && currentOptions.played)
        {
            Choosing = false;
            currentOptions.options[currentOptions.lastChoose].OnChoose?.Invoke();
            return;
        }

        foreach (Option option in currentOptions.options)
        {
            GameObject go = Instantiate(optionPrefab, optionContainer);

            Button button = go.GetComponent<Button>();

            button.onClick.AddListener(() =>
            {
                currentOptions.lastChoose = currentOptions.options.IndexOf(option);

                if (option.isShowEvidence && option.evidences.Count > 0)
                {
                    PlayerKnowledgeController.Instance.ShowSelectEvidenceWindow();

                    PlayerKnowledgeController.Instance.OnChooseEvidence.AddListener((Evidence evidence) =>
                    {
                        Choosing = false;
                        ShowEvidence se = option.evidences.Find((ShowEvidence e) =>
                        {
                            return e.evidence == evidence;
                        });
                        if (se != null)
                        {
                            se.OnChoose?.Invoke();
                        }
                        else
                        {
                            Debug.Log("This mean nothing to me");
                            option.OnChoose?.Invoke();
                        }
                    });

                }
                else
                {
                    Choosing = false;
                    option.OnChoose?.Invoke();
                }

                HideOptions();
            });

            TextMeshProUGUI text = go.GetComponentInChildren<TextMeshProUGUI>();
            Image image = go.GetComponentsInChildren<Image>(true)[1];
            if (option.icon != null)
            {
                image.sprite = option.icon;
                image.gameObject.SetActive(true);
            }

            text.SetText(option.text);
        }

        optionWindowAnimator.gameObject.SetActive(true);
    }

    void HideOptions()
    {
        foreach (Button btn in optionContainer.GetComponentsInChildren<Button>())
        {
            Destroy(btn.gameObject);
        }
        currentOptions.played = true;
        optionWindowAnimator.gameObject.SetActive(false);
    }


}
