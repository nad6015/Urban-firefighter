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
        SceneManager.LoadScene("Level_1");
    }

    public void LoadCredits() {
        SceneManager.LoadScene("CreditsV2");
    }

    public void LoadMainMenu() {
        SceneManager.LoadScene("Main MenuV2");
    }

    public void LoadOptions() {
        SceneManager.LoadScene("OptionsV2");
    }

    public void ExitGame() {
        Application.Quit();
    }
}
