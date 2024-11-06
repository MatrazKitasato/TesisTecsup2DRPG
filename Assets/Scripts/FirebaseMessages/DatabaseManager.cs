using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Extensions;
using TMPro;
using UnityEngine.SceneManagement;

public class DatabaseManager : MonoBehaviour
{
    public TMP_InputField inputField;
    public TMP_Text errorMessageText;
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
        string message = inputField.text.Trim();
        errorMessageText.text = "";

        if (!string.IsNullOrEmpty(message))
        {
            string key = databaseReference.Child("messages").Push().Key;
            databaseReference.Child("messages").Child(key).SetValueAsync(message).ContinueWithOnMainThread(task =>
            {
                if (task.IsCompleted)
                {
                    Debug.Log("Message sent to Firebase: " + message);
                    inputField.text = "";
                    ChangeScene();
                }
                else
                {
                    Debug.LogError("Error sending message: " + task.Exception);
                    errorMessageText.text = "Ha ocurrido un error, vuelve a intentarlo.";
                }
            });
        }
        else
        {
            errorMessageText.text = "Debes escribir un mensaje...";
        }
    }
    private void ChangeScene()
    {
        SceneManager.LoadScene("MessageViewer");
    }
}


