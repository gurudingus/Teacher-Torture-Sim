using UnityEngine;
using TMPro;
using System.Collections;

public class EndingItems : MonoBehaviour {

    const int numberOfEndings = 9;

    [SerializeField] private bool automaticallyFindItems = true;
    [SerializeField] private GameObject[] endingObjects = new GameObject[9];
    [SerializeField] private float splashFadeTime = 3f;
    private TextMeshProUGUI textBox;
    private Color textBoxColour;

    private void Awake()
    {
        if (automaticallyFindItems) AttemptFindingObjects();

        SpawnItems();
        Invoke(nameof(NewItemSplash), 1f);
    }

    private void AttemptFindingObjects()
    {
        for (int i = 0; i < numberOfEndings; i++) {
            endingObjects[i] ??= GameObject.Find($"Ending Item {i + 1}"); //Try to set all non-null ending items to an ending item named "Ending Item N"
        }
    }

    private void SpawnItems()
    {
        for (int i = 0; i < numberOfEndings; i++)
        {
            if (!Events.GetEventComplete((GameEvent)i)) Destroy(endingObjects[i]);
        }
    }

    private void NewItemSplash()
    {
        textBox = GameObject.Find("Item Splash").GetComponent<TextMeshProUGUI>(); //Attempt to find the splash text box gameobject

        if (textBox == null) return; //Return if the item splash cannot be found. This essentially disabled the entire system if there is not a splash text box ready.

        int mostRecentEvent = (int)Events.mostRecentEvent;

        if (mostRecentEvent < 0 || mostRecentEvent > numberOfEndings - 1) return;
        Events.mostRecentEvent = GameEvent.None;

        textBoxColour = textBox.color; //Cache the original colour of the text box so it can be brought back whenever needed
        textBox.text = $"New item unlocked: {endingObjects[mostRecentEvent].name}";

        Invoke(nameof(BeginFadeOut), splashFadeTime * 0.5f);
    }

    private void BeginFadeOut() => StartCoroutine(FadeOut());

    private IEnumerator FadeOut()
    {
        while (textBox.color.a > 0f)
        {
            textBox.color = new(textBoxColour.r, textBoxColour.g, textBoxColour.b, textBox.color.a - Time.deltaTime / splashFadeTime * 2f);
            yield return null;
        }
    }
}
