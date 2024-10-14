using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using RNG = System.Random;

[RequireComponent(typeof(AudioSource))] public class BackgroundMusic : MonoBehaviour, IGameState
{
    [SerializeField] private AudioClip[] songs = new AudioClip[1];
    private List<AudioClip> queue = new();
    private AudioSource speaker;

    [SerializeField] private float fadeOutTime = 0.5f;

    private static RNG random = new();

    private void Awake()
    {
        GameManager.Subscribe(this);
        speaker = GetComponent<AudioSource>();
    }

    public void OnGameStateChanged(GameState gameState)
    {
        switch (gameState)
        {
            case GameState.Playing:
                speaker.loop = false;
                StartCoroutine(FadeOut());
                Invoke(nameof(PlayNextSong), fadeOutTime + 0.1f);
                break;
            case GameState.Cutscene:
                CancelInvoke(nameof(PlayNextSong));
                StartCoroutine(FadeOut());
                break;
            case GameState.Paused:
                speaker.volume = 0.25f;
                break;
        }

        if (gameState != GameState.Paused) speaker.volume = 1f; //Easier than having to put this in every switch case that isn't Paused
    }

    IEnumerator FadeOut()
    {
        while (speaker.volume > 0f)
        {
            speaker.volume -= Time.deltaTime / fadeOutTime;
            yield return null;
        }
        speaker.volume = 0f; //Make sure the volume is set to 0 after the while loop finishes
    }

    private void PlayNextSong()
    {
        CancelInvoke(nameof(PlayNextSong)); //This will allow the function to also be used to skip songs
        speaker.volume = 1f;

        if (queue.Count == 0)
        {
            foreach(AudioClip song in songs) if (song != null) queue.Add(song);
            queue = queue.OrderBy(song => random.Next()).ToList();
        }

        speaker.clip = queue[0];
        queue.RemoveAt(0);

        speaker.Play();

        Invoke(nameof(PlayNextSong), speaker.clip.length);
    }
}