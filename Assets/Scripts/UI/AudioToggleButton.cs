using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioToggleButton : MonoBehaviour
{
    public Button targetButton;
    public SpriteAudioManager audioManager;
    void Start()
    {
        if (audioManager == null)
        {
            Debug.LogError("No se encontró un SpriteAudioManager en la escena.");
            return;
        }
        targetButton.onClick.AddListener(audioManager.ToggleAudioAndSprite);
    }
}
