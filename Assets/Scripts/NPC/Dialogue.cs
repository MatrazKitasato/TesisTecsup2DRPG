using System.Collections;
using TMPro;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    [SerializeField] bool isPlayerInRange;
    [SerializeField] bool didDialogueStart;
    [SerializeField] int lineIndex;
    [SerializeField] GameObject exclamationMark;
    [SerializeField] GameObject dialoguePanel;
    [SerializeField] TMP_Text dialogueText;
    [SerializeField] float typingTime = 0.05f;
    [SerializeField] Player pj;
    [SerializeField] Door door;
    [SerializeField] bool isFinalNPC = false;
    [SerializeField, TextArea(4, 8)] string[] dialogueLines;
    void Start()
    {
        pj = FindObjectOfType<Player>().GetComponent<Player>();
        door = FindObjectOfType<Door>().GetComponent<Door>();
    }
    void Update()
    {
        if(isPlayerInRange && Input.GetButtonDown("Fire1"))
        {
            if (!didDialogueStart)
            {
                StartDialogue();
            }
            else if(dialogueText.text == dialogueLines[lineIndex])
            {
                NextDialogueLine();
            }
            else
            {
                StopAllCoroutines();
                dialogueText.text = dialogueLines[lineIndex];
            }
        }
    }
    private void StartDialogue()
    {
        didDialogueStart = true;
        exclamationMark.SetActive(false);
        dialoguePanel.SetActive(true);
        lineIndex = 0;
        pj.isTalking = true;
        StartCoroutine(ShowLine());
    }
    private void NextDialogueLine()
    {
        lineIndex++;
        if(lineIndex < dialogueLines.Length)
        {
            StartCoroutine(ShowLine());
        }
        else
        {
            didDialogueStart = false;
            dialoguePanel.SetActive(false);
            exclamationMark?.SetActive(true);
            pj.isTalking = false;
            if(isFinalNPC)
            {
                door.ConditionFullfilled();
            }
        }
    }
    private IEnumerator ShowLine()
    {
        dialogueText.text = string.Empty;

        foreach (char ch in dialogueLines[lineIndex])
        {
            dialogueText.text += ch;
            yield return new WaitForSecondsRealtime(typingTime);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        isPlayerInRange = true;
        exclamationMark.SetActive(true);
        Debug.Log("Se puede iniciar dialogo");
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        isPlayerInRange = false;
        exclamationMark.SetActive(false);
        Debug.Log("No se puede iniciar dialogo");
    }
}
