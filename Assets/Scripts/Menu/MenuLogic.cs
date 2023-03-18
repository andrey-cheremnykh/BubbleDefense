using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuLogic : MonoBehaviour
{
    [SerializeField] Image musicIconButton;
    [SerializeField] Image soundIconButton;

    [Space]
    [SerializeField] Sprite musicOn;
    [SerializeField] Sprite musicOff;
    [SerializeField] Sprite soundOn;
    [SerializeField] Sprite soundOff;

    void Start()
    {
        bool muteMusic = GlobalAudioManager.instance.IsMusicMuted();
        bool muteSound = GlobalAudioManager.instance.IsSoundMuted();
        if (muteMusic) musicIconButton.sprite = musicOff;
        if (muteSound) soundIconButton.sprite = soundOff;
    }

    public void OnSoundPressed()
    {
        GlobalAudioManager.instance.ToggleSound();
        bool muteSound = GlobalAudioManager.instance.IsSoundMuted();
        if (muteSound) soundIconButton.sprite = soundOff;
        else soundIconButton.sprite = soundOn;
    }

    public void OnMusicPressed()
    {
        GlobalAudioManager.instance.ToggleMusic();
        bool muteMusic = GlobalAudioManager.instance.IsMusicMuted();
        if (muteMusic) musicIconButton.sprite = musicOff;
        else musicIconButton.sprite = musicOn;
    }

  
}
