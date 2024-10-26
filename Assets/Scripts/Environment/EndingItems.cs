using System;
using System.Collections;
using UnityEngine;
using TMPro;

[Serializable] public struct ItemAndTrophy
{
    public string itemName;
    public GameObject item;
    public GameObject trophy;
}

public class EndingItems : MonoBehaviour, IResetStatic
{

    const int numberOfEndings = 6;

    [SerializeField] private bool automaticallyFindItems = true;
    [SerializeField] private float splashFadeTime = 3f;
    [SerializeField] private ItemAndTrophy[] endingItems = new ItemAndTrophy[9];
    private TextMeshProUGUI textBox;
    private Color textBoxColour;

    private static EndingItems instance;

    private void Awake() => instance = this; //This will break if multiple EndingItems scripts are in the scene but that's fine; there should never be more than one of these.

    private void Start()
    {
        if (automaticallyFindItems) AttemptFindingObjects();

        SpawnItems();
    }

    public void OnResetStatic() => instance = null;

    private void AttemptFindingObjects()
    {
        for (int i = 0; i < numberOfEndings; i++) {
            endingItems[i].item ??= GameObject.Find($"Ending Item {i + 1}"); //Try to set all non-null ending items to an ending item named "Ending Item N"
            endingItems[i].trophy ??= GameObject.Find($"Ending Trophy {i + 1}");
        }
    }

    public static void UpdateItems() => instance.SpawnItems();
    private void SpawnItems()
    {
        for (int i = 0; i < numberOfEndings; i++)
        {
            if (!Events.GetEventComplete((GameEvent)i)) Destroy(endingItems[i].item!);
            if (!Events.GetEventComplete((GameEvent)i)) Destroy(endingItems[i].trophy!);
        }
    }

    public static void Splash() => instance.NewItemSplash();
    private void NewItemSplash()
    {
        int mostRecentEvent = (int)Events.mostRecentEvent;

        if (mostRecentEvent < 0 || mostRecentEvent > numberOfEndings - 1) return;
        Events.mostRecentEvent = GameEvent.None;

        ItemAndTrophy gameObject = endingItems[mostRecentEvent];

        SplashText($"New item unlocked: {(gameObject.itemName == null ? gameObject.item?.name : gameObject.itemName)}"); //If the itemName field is null, use the name of the gameObject
    }

    private void SplashText(string text)
    {
        textBox = GameObject.Find("Item Splash")?.GetComponent<TextMeshProUGUI>(); //Attempt to find the splash text box gameobject

        if (textBox == null) return; //Return if the item splash cannot be found. This essentially disabled the entire system if there is not a splash text box ready.

        textBoxColour = textBox.color; //Cache the original colour of the text box so it can be brought back whenever needed
        textBox.text = text;

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
