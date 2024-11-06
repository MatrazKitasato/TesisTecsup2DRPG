using System.Collections;
using TMPro;
using UnityEngine;

public class DialogueIntro : MonoBehaviour
{
    [SerializeField] TMP_Text messageText;
    [SerializeField] GameObject continueButton;
    [SerializeField, TextArea(4, 6)] string introMessage;
    [SerializeField] float typingSpeed = 0.05f;
    void Start()
    {
        continueButton.SetActive(false);
        StartCoroutine(TypeMessage());
    }
    private IEnumerator TypeMessage()
    {
        messageText.text = "";

        foreach (char letter in introMessage)
        {
            messageText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        continueButton.SetActive(true);
    }
}
