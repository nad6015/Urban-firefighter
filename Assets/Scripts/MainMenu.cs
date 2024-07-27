using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        SceneManager.LoadScene("Credits");
    }

    public void LoadMainMenu() {
        SceneManager.LoadScene("Main Menu");
    }

    public void LoadOptions() {
        SceneManager.LoadScene("Options");
    }

    public void ExitGame() {
        Application.Quit();
    }
}
