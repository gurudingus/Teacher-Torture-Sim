using UnityEngine;

public class EndingItems : MonoBehaviour {

    [SerializeField] private GameObject[] endingObjects = new GameObject[9];

    private void Awake()
    {
        SpawnItems();
    }

    public void SpawnItems()
    {
        for (int i = 0; i < 9; i++)
        {
            if (!Events.GetEventComplete((GameEvent)i)) Destroy(endingObjects[i]);
        }
    }
}
