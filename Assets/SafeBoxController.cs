using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SafeBoxController : MonoBehaviour
{
    public GameObject safeBoxWindow;
    public GameObject safeBox;
    public GameObject openSafeBox;
    public TextMeshProUGUI text;
    public string correctPass;
    public AudioClip buttonSound;
    public AudioClip wrongSound;
    public AudioClip openSound;

    public AudioSource audioSource;
    int length;
    bool isOpen;
    string currentPass = "";

    // Start is called before the first frame update
    void Start()
    {
        length = correctPass.Length * 2;
        text.SetText(currentPass);
    }

    public void Button(string value)
    {
        audioSource.PlayOneShot(buttonSound);
        currentPass += (value + " ");
        if (currentPass.Length == length)
        {
            if (currentPass.Replace(" ", "") == correctPass)
            {
                isOpen = true;
                audioSource.PlayOneShot(openSound);

                openSafeBox.SetActive(true);
                safeBox.SetActive(false);
            }
            else
            {
                currentPass = "";
                audioSource.PlayOneShot(wrongSound);
            }
        }

        text.SetText(currentPass);
    }

    public void Show()
    {
        safeBoxWindow.SetActive(true);
    }

    public void Hide()
    {
        safeBoxWindow.SetActive(false);
    }
}
