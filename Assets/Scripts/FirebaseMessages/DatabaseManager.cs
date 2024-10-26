using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Extensions;
using UnityEngine.UI;
using TMPro;

public class DatabaseManager : MonoBehaviour
{
    public TMP_InputField inputField; // Asigna el InputField desde el Inspector
    private DatabaseReference databaseReference;

    void Start()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            FirebaseApp app = FirebaseApp.DefaultInstance;
            databaseReference = FirebaseDatabase.DefaultInstance.RootReference;
        });
    }

    public void SendMessageToFirebase()
    {
        string message = inputField.text;
        if (!string.IsNullOrEmpty(message))
        {
            string key = databaseReference.Child("messages").Push().Key;
            databaseReference.Child("messages").Child(key).SetValueAsync(message).ContinueWithOnMainThread(task =>
            {
                if (task.IsCompleted)
                {
                    Debug.Log("Mensaje enviado a Firebase: " + message);
                    inputField.text = "";
                }
                else
                {
                    Debug.LogError("Error al enviar mensaje: " + task.Exception);
                }
            });
        }
    }
}