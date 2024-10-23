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
        /*speaker.time = speaker.clip.length * (float)random.NextDouble(); //Start the music playing at some random point throughout the song so that it gives the illusion of the song playing continuously while not audible
        volume = speaker.volume;*/
    }

    private void Update()
    {
        if (!speaker.isPlaying && speaker.enabled) PlayNextSong(0f); //If the song is finished, play the next song from the beginning
    }

    public void OnGameStateChanged(GameState gameState)
    {
        if (gameState != GameState.Paused) speaker.volume = volume; //Easier than having to put this in every switch case that isn't Paused
        
        switch (gameState)
        {
            case GameState.Playing:
                if (isInPlayRoom) return; //Protection so that this only runs the first time the game state is set to playing

                speaker.loop = false; //Disable looping because now we want different songs to play based on the queue
                StartCoroutine(FadeOut());
                Invoke(nameof(NextSongRandomStart), fadeOutTime + 0.1f); //Start the next song in the queue just after the fadeout finishes, with the song starting at a random point in its duration

                isInPlayRoom = true;
                break;
            case GameState.Cutscene:
                CancelInvoke(nameof(NextSongRandomStart));
                StartCoroutine(FadeOut());
                Invoke(nameof(DisableSpeaker), fadeOutTime); //I have no idea why this is necessary but it's just here to make sure it actually mutes the speaker once a cutscene is playing
                break;
            case GameState.Paused:
                speaker.volume = pausedVolume;
                break;
        }
    }

    private void DisableSpeaker() => speaker.enabled = false; //Quick little function here so that it can be called through and invoke

    IEnumerator FadeOut() //Gradually lower the volume to zero
    {
        while (speaker.volume > 0f)
        {
            speaker.volume -= Time.deltaTime / fadeOutTime * volume; //Multiply by volume so that the fadeout takes equally as long no matter what the default volume
            yield return null;
        }
        speaker.volume = 0f; //Make sure the volume is set to 0 after the while loop finishes
    }

    private void NextSongRandomStart() => PlayNextSong((float)random.NextDouble()); //Start the next song at some random point in its runtime

    private void PlayNextSong(float proportionComplete)
    {
        speaker.volume = volume; //Make sure the volume is correct, since most of the time this function is called, it is after a FadeOut()

        if (queue.Count == 0) //If the queue is empty, add all non-null items in the playlist to the queue and then randomly shuffle the queue
        {
            foreach(AudioClip song in songs) if (song != null) queue.Add(song);
            queue = queue.OrderBy(song => random.Next()).ToList();
        }

        speaker.clip = queue[0]; //Start the first song in the queue
        queue.RemoveAt(0); //Remove the first song from the queue
        
        if (speaker.enabled)
        {
            speaker.Play(); //Actually play the song
            speaker.time = speaker.clip.length * proportionComplete; //If some non-zero value was passed into the function, the song will start some way through
        }
    }
}