using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;

public class CutsceneManager : MonoBehaviour, IResetStatic
{
    private static CutsceneManager instance;

    [SerializeField] private GameObject[] cutsceneCameras = new GameObject[9];

    [SerializeField] private float skipTime = 1f;

    private static bool skippable = false;
    private static float skipHoldStartTime = 0f;
    private static float skipHoldTime => Time.timeSinceLevelLoad - skipHoldStartTime;
    private static TextMeshProUGUI skipText;
    private static Image progressBar;
    private static RawImage fadeToBlack;
    private static AudioSource cutsceneMusic;

    private void Awake()
    {
        instance = this;

        GameObject skipGUI = GameObject.Find("Cutscene Skip");
        skipText = skipGUI.GetComponentInChildren<TextMeshProUGUI>();
        progressBar = skipGUI.GetComponentInChildren<Image>();
        fadeToBlack = skipGUI.GetComponentInChildren<RawImage>();

        skipText.enabled = false;
        progressBar.enabled = false;
        fadeToBlack.enabled = false;
    }

    public static void PlayCutscene(GameEvent gameEvent) //Currently only set up for ending cutscenes
    {
        if ((int)gameEvent < 0 || (int)gameEvent > 8 || GameManager.gameState != GameState.Playing) return; //Don't do anything if it is not a valid ending
        GameManager.gameState = GameState.Cutscene;

        Camera.main.gameObject.SetActive(false);
        instance.cutsceneCameras[(int)gameEvent].SetActive(true);
        cutsceneMusic = instance.cutsceneCameras[(int)gameEvent].GetComponent<AudioSource>();

        skippable = Events.GetEventComplete(gameEvent);
        Events.SetEventComplete(gameEvent);

        if (skippable)
        {
            skipText.enabled = true;
            progressBar.enabled = true;
            fadeToBlack.enabled = true;
        }
    }

    private void OnSkipCutscene(InputValue input)
    {
        if (!skippable || GameManager.gameState != GameState.Cutscene) return; //Don't do anything if the cutscene is unskippable or it's not currently a cutscene

        if (input.isPressed)
        {
            skipHoldStartTime = Time.timeSinceLevelLoad;
            StartCoroutine(CheckSkip());
        }
        else
        {
            progressBar.fillAmount = 0f;
            fadeToBlack.color = Color.clear;
            cutsceneMusic.volume = 1f;
            StopAllCoroutines();
        }
    }

    private IEnumerator CheckSkip()
    {
        while (skipHoldTime < skipTime)
        {
            progressBar.fillAmount = skipHoldTime / skipTime;
            fadeToBlack.color = new(0f, 0f, 0f, (skipHoldTime * 2f - skipTime) / skipTime);
            cutsceneMusic.volume = 1f - (skipHoldTime / skipTime);
            yield return null;
        }
        GameManager.LoadLevel(0);
    }

    public void OnStaticReset() => skippable = false;
}
