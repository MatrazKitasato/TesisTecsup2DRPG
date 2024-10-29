using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueIntro : MonoBehaviour
{
    [SerializeField] TMP_Text messageText; // Texto que mostrar� el mensaje
    [SerializeField] GameObject continueButton; // Bot�n para continuar
    [SerializeField, TextArea(4, 6)] string introMessage; // Mensaje inicial
    [SerializeField] float typingSpeed = 0.05f;
    void Start()
    {
        continueButton.SetActive(false); // Desactivar el bot�n al inicio
        StartCoroutine(TypeMessage());
    }
    private IEnumerator TypeMessage()
    {
        messageText.text = "";

        foreach (char letter in introMessage)
        {
            messageText.text += letter; // Agregar letra por letra
            yield return new WaitForSeconds(typingSpeed); // Esperar un tiempo antes de agregar la siguiente letra
        }

        continueButton.SetActive(true); // Activar el bot�n al finalizar el mensaje
    }
}
