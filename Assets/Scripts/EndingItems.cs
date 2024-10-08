using UnityEngine;
using TMPro;
using System.Collections;

public class EndingItems : MonoBehaviour {

    [SerializeField] private GameObject[] endingObjects = new GameObject[9];
    private TextMeshProUGUI textBox;

    private void Awake()
    {
        SpawnItems();
        NewItemSplash();
    }

    private void SpawnItems()
    {
        for (int i = 0; i < 9; i++)
        {
            if (!Events.GetEventComplete((GameEvent)i)) Destroy(endingObjects[i]);
        }
    }

    private void NewItemSplash()
    {
        int mostRecentEvent = (int)Events.mostRecentEvent;

        if (mostRecentEvent < 0 || mostRecentEvent > 8) return;
        Events.mostRecentEvent = GameEvent.None;
        string itemSplash = $"New item unlocked: {endingObjects[mostRecentEvent].name}";
    }

    private IEnumerable FadeOut()
    {

        yield return null;
    }
}
