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
    public float volume { private get; set; } = 1f;
    [SerializeField] public float pausedVolume = 0.1f;

    private bool isInPlayRoom = false;

    private static RNG random = new();

    private void Awake()
    {
        GameManager.Subscribe(this);
        
        speaker = GetComponent<AudioSource>();
        speaker.time = speaker.clip.length * (float)random.NextDouble();
        volume = speaker.volume;
    }

    public void OnGameStateChanged(GameState gameState)
    {
        switch (gameState)
        {
            case GameState.Playing:
                if (isInPlayRoom) return; //Protection so that this only runs once

                speaker.loop = false;
                StartCoroutine(FadeOut());
                Invoke(nameof(NextSongRandomStart), fadeOutTime + 0.1f);

                isInPlayRoom = true;
                break;
            case GameState.Cutscene:
                CancelInvoke(nameof(PlayNextSong));
                StartCoroutine(FadeOut());
                Invoke(nameof(DisableSpeaker), fadeOutTime); //I have no idea why this is necessary but it's just here to make sure it actually mutes the speaker once a cutscene is playing
                break;
            case GameState.Paused:
                speaker.volume = pausedVolume;
                break;
        }

        if (gameState != GameState.Paused) speaker.volume = volume; //Easier than having to put this in every switch case that isn't Paused
    }

    private void DisableSpeaker() => speaker.enabled = false;

    IEnumerator FadeOut()
    {
        while (speaker.volume > 0f)
        {
            speaker.volume -= Time.deltaTime / fadeOutTime * volume; //Multiply by volume so that the fadeout takes equally as long no matter what the default volume
            yield return null;
        }
        speaker.volume = 0f; //Make sure the volume is set to 0 after the while loop finishes
    }

    private void NextSongRandomStart() => PlayNextSong((float)random.NextDouble());

    private void PlayNextSong(float proportionComplete)
    {
        StopCoroutine(nameof(QueueSong)); //This will allow the function to also be used to skip songs
        speaker.volume = volume;

        if (queue.Count == 0)
        {
            foreach(AudioClip song in songs) if (song != null) queue.Add(song);
            queue = queue.OrderBy(song => random.Next()).ToList();
        }

        speaker.clip = queue[0];
        queue.RemoveAt(0);

        speaker.Play();
        speaker.time = speaker.clip.length * proportionComplete;

        StartCoroutine(nameof(QueueSong));
    }

    //TODO - I should really make this not a coroutine because this basically just always runs anyway and has extra overhead from needing to use the string invocation since I have to be able to cancel it
    IEnumerator QueueSong() //I would use an invoke with the song length but the invoke is affected by timescale and I want the song to keep playing while the game is paused
    {
        while (speaker.isPlaying) yield return null;
        PlayNextSong(0f);
    }
}