using UnityEngine;

public class ResetSaveFile : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        #if UNITY_EDITOR
        Debug.Log("Reset Save File");
        #endif

        Events.SetEventsComplete(0);
        
        EndingItems.UpdateItems();
        Events.mostRecentEvent = GameEvent.None;
    }
}
