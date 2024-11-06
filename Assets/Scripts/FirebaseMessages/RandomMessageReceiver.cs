using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Extensions;
using System.Collections.Generic;
using TMPro;
public class RandomMessageReceiver : MonoBehaviour
{
    private DatabaseReference databaseReference;
    public List<TextMeshProUGUI> messageTexts;

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

                for (int i = 0; i < messageTexts.Count; i++)
                {
                    if (i < randomMessages.Count)
                    {
                        messageTexts[i].text = randomMessages[i];
                    }
                    else
                    {
                        messageTexts[i].text = "";
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

        while (randomMessages.Count < count && randomMessages.Count < totalMessages)
        {
            int randomIndex = Random.Range(0, totalMessages);
            string randomMessage = messages[randomIndex];

            if (!randomMessages.Contains(randomMessage))
            {
                randomMessages.Add(randomMessage);
            }
        }
        return randomMessages;
    }
}