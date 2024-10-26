using UnityEngine;
using UnityEngine.UI; // Asegúrate de incluir esto si usas UI Text
using Firebase;
using Firebase.Database;
using Firebase.Extensions;
using System.Collections.Generic;
using TMPro;

public class RandomMessageReceiver : MonoBehaviour
{
    private DatabaseReference databaseReference;

    // Lista para almacenar los objetos de texto
    public List<TextMeshProUGUI> messageTexts; // Asigna los objetos de texto desde el Inspector

    void Start()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            FirebaseApp app = FirebaseApp.DefaultInstance;
            databaseReference = FirebaseDatabase.DefaultInstance.RootReference;
            RetrieveRandomMessages();
        });
    }

    void RetrieveRandomMessages()
    {
        databaseReference.Child("messages").GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                List<string> messages = new List<string>();

                foreach (var child in snapshot.Children)
                {
                    messages.Add(child.Value.ToString());
                }

                List<string> randomMessages = GetRandomMessages(messages, messageTexts.Count);

                // Asignar mensajes a los objetos de texto
                for (int i = 0; i < messageTexts.Count; i++)
                {
                    if (i < randomMessages.Count)
                    {
                        messageTexts[i].text = randomMessages[i];
                    }
                    else
                    {
                        messageTexts[i].text = ""; // Limpiar el texto si no hay más mensajes
                    }
                }
            }
            else
            {
                Debug.LogError("Error al recuperar mensajes: " + task.Exception);
            }
        });
    }

    List<string> GetRandomMessages(List<string> messages, int count)
    {
        List<string> randomMessages = new List<string>();
        int totalMessages = messages.Count;

        if (totalMessages == 0) return randomMessages;

        // Selecciona mensajes aleatorios
        while (randomMessages.Count < count && randomMessages.Count < totalMessages)
        {
            int randomIndex = Random.Range(0, totalMessages);
            string randomMessage = messages[randomIndex];

            // Asegurarse de que no se repita el mensaje
            if (!randomMessages.Contains(randomMessage))
            {
                randomMessages.Add(randomMessage);
            }
        }

        return randomMessages;
    }
}