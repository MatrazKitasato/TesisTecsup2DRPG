using System.Collections;
using TMPro;
using UnityEngine;
public class DialogueRandom : MonoBehaviour
{
    [SerializeField] private bool isPlayerInRange;
    [SerializeField] private bool didDialogueStart;
    [SerializeField] private GameObject exclamationMark;
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField, TextArea(4, 6)] private string[] dialogueLines;
    [SerializeField] private float typingTime = 0.05f;

    private int currentDialogueIndex;
    private bool isTyping;
    private Coroutine typingCoroutine;

    void Update()
    {
        if (isPlayerInRange && Input.GetButtonDown("Fire1"))
        {
            if (!didDialogueStart)
            {
                StartDialogue();
            }
            else if (isTyping)
            {
                FinishTyping();
            }
            else
            {
                EndDialogue();
            }
        }
    }

    private void StartDialogue()
    {
        didDialogueStart = true;
        exclamationMark.SetActive(false);
        dialoguePanel.SetActive(true);
        Time.timeScale = 0f;
        currentDialogueIndex = Random.Range(0, dialogueLines.Length);
        typingCoroutine = StartCoroutine(ShowLine(currentDialogueIndex));
    }
    private void EndDialogue()
    {
        didDialogueStart = false;
        dialoguePanel.SetActive(false);
        exclamationMark?.SetActive(true);
        Time.timeScale = 1f;
    }

    private IEnumerator ShowLine(int index)
    {
        dialogueText.text = string.Empty;
        isTyping = true;

        foreach (char ch in dialogueLines[index])
        {
            dialogueText.text += ch;
            yield return new WaitForSecondsRealtime(typingTime);
        }

        isTyping = false;
    }

    private void FinishTyping()
    {
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }
        dialogueText.text = dialogueLines[currentDialogueIndex];
        isTyping = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInRange = true;
            exclamationMark.SetActive(true);
            Debug.Log("Se puede iniciar diálogo");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInRange = false;
            exclamationMark.SetActive(false);
            Debug.Log("No se puede iniciar diálogo");
        }
    }
}
