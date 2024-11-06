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
    public string mutekey;
    void Start()
    {
        int isMuted = PlayerPrefs.GetInt(mutekey, 0);
        UpdateSpriteAndAudio(isMuted == 1);
    }

    public void ToggleAudioAndSprite()
    {
        bool isCurrentlyMuted = audioSource.mute;

        UpdateSpriteAndAudio(!isCurrentlyMuted);

        PlayerPrefs.SetInt(mutekey, isCurrentlyMuted? 0 : 1);
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
