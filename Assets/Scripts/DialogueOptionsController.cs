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

    }


    public static void ShowDialogueOpition(DialogueOption options)
    {
        Choosing = true;
        Instance.currentOptions = options;
        Instance.ShowOptions();
    }


    void ShowOptions()
    {

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
                Choosing = false;
                option.OnChoose?.Invoke();
                HideOptions();
            });

            TextMeshProUGUI text = go.GetComponentInChildren<TextMeshProUGUI>();

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
