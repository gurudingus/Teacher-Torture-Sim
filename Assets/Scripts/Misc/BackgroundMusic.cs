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
    [SerializeField] private float pausedVolume = 0.1f;

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
                speaker.loop = false;
                StartCoroutine(FadeOut());
                Invoke(nameof(NextSongRandomStart), fadeOutTime + 0.1f);
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

    IEnumerator FadeOut()
    {
        while (speaker.volume > 0f)
        {
            speaker.volume -= Time.deltaTime / fadeOutTime * volume; //Multiply by volume so that the fadeout takes equally as long no matter what the default volume
            yield return null;
        }
        speaker.volume = 0f; //Make sure the volume is set to 0 after the while loop finishes
    }

    private void DisableSpeaker() => speaker.enabled = false;

    private void PlayNextSong(float proportionComplete)
    {
        CancelInvoke(nameof(PlayNextSong)); //This will allow the function to also be used to skip songs
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

        Invoke(nameof(PlayNextSong), speaker.clip.length * (1 - proportionComplete));
    }

    private void NextSong() => PlayNextSong(0f);
    private void NextSongRandomStart() => PlayNextSong((float)random.NextDouble());
}