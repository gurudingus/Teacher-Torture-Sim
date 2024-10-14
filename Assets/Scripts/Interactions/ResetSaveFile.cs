using UnityEngine;

public class ResetSaveFile : MonoBehaviour, IInteractable
{
    private AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void Interact()
    {
        #if UNITY_EDITOR
        Debug.Log("Reset Save File");
        #endif

        Events.SetEventsComplete(0);
        
        EndingItems.UpdateItems();
        Events.mostRecentEvent = GameEvent.None;

        audioSource.Play();
    }
}
