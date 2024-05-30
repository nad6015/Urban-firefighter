using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class EmitBeats : MonoBehaviour
{
    public bool isRandom = true;
    public bool isPlayerTurn = false;

    public AudioClip[] beats;
    public Color[] beatColors;
    public AudioClip musicClip;

    public int beatNum = 10;
    public UICountdown timer;

    public float timeBetweenBeats = 2;

    public ParticleSystem system;

    private AudioSource audioSrc;

    private List<int> beatsToCopy = new List<int>();

    private int nextBeat = 0;
    private int currentBeat = 0;

    void Start()
    {
        audioSrc = GetComponent<AudioSource>();

        for (int i = 0; i < beatNum + 1; i++)
        {
            beatsToCopy.Add(Random.Range(0, beats.Length));
        }
        currentBeat = beatsToCopy[nextBeat];

        var vfx = system.main;
        vfx.startColor = beatColors[currentBeat];
        timer.onCountdownFinished += playerLost;
    }

    private void playerLost()
    {
        throw new System.NotImplementedException();
    }

    void Update()
    {
        if (!isPlayerTurn)
        {
            playBeats();
        }
    }

    public void playBeats()
    {
        if (!system.isPlaying)
        {
            if (nextBeat < beatsToCopy.Count - 1)
            {
                currentBeat = beatsToCopy[++nextBeat];
                audioSrc.PlayOneShot(beats[currentBeat]);
                var vfx = system.main;
                vfx.startColor = beatColors[currentBeat];
                system.Play();
            }
            else
            {
                timer.StartCountdown();
                isPlayerTurn = true;
            }
        }
    }
}
