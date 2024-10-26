using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpriteAudioManager : MonoBehaviour
{
    [Header("Sprites")]
    public Image spriteRenderer;
    public Sprite spriteOn;
    public Sprite spriteOff;
    public Button targetButton;
    [Header("Sounds")]
    public AudioSource audioSource;
    void Start()
    {
        //DontDestroyOnLoad(gameObject);
        int isMuted = PlayerPrefs.GetInt("isAudioMuted", 0);
        UpdateSpriteAndAudio(isMuted == 1);
    }

    public void ToggleAudioAndSprite()
    {
        bool isCurrentlyMuted = audioSource.mute;

        UpdateSpriteAndAudio(!isCurrentlyMuted);

        PlayerPrefs.SetInt("isAudioMuted", isCurrentlyMuted? 0 : 1);
        PlayerPrefs.Save();
    }

    private void UpdateSpriteAndAudio(bool mute)
    {
        Debug.Log("Actualizando sprite y audio: " + (mute ? "Mute" : "On"));
        audioSource.mute = mute;
        targetButton.image.sprite = mute ? spriteOff : spriteOn;
        Debug.Log("Sprite actualizado a: " + spriteRenderer.sprite.name);
    }
}
