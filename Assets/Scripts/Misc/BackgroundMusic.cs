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
    private void Start() => PlayNextSong();

    public void OnGameStateChanged(GameState gameState)
    {
        if (gameState == GameState.Cutscene) StartCoroutine(FadeOut());
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
