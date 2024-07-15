using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public AudioClip moveSound;
    public AudioClip selectSound;

    AudioSource mAudioSource;

    void Start()
    {
        mAudioSource = GetComponent<AudioSource>();
    }

    public void PlayMoveSound() {
        mAudioSource.PlayOneShot(moveSound);
    }

    public void PlaySelectSound() {
        mAudioSource.PlayOneShot(selectSound);
    }

    public void PlaySound(AudioClip sound) {
        mAudioSource.PlayOneShot(sound);
    }

    public void StartGame() {
        Debug.Log("Start Game");
    }

    public void LoadCredits() {
        Debug.Log("Load Credits");
    }

    public void LoadOptions() {
        Debug.Log("Load Options");
    }

    public void ExitGame() {
        Debug.Log("Exit Game");
    }
}
