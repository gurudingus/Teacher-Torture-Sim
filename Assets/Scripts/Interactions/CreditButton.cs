using UnityEngine;
using TMPro; 
//Liam's and Rama's code

public class CreditButton : MonoBehaviour, IInteractable
{
    private AudioSource audioSource;

    //Reference to the TMP asset you want to toggle
    public TextMeshProUGUI tmpText;

    private bool isTextVisible = true; //Tracks if the text is visible

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void Interact()
    {
        //Play the button click sound
        audioSource.Play();

        //Toggle the visibility of the TMP text asset
        ToggleTextVisibility();

        //If in the Unity Editor log the button interaction
        #if UNITY_EDITOR
        Debug.Log("Credits Button Clicked");
        #endif
    }

    //Method to toggle the visibility of the TMP text asset
    private void ToggleTextVisibility()
    {
        //Check if the TMP text reference is assigned
        if (tmpText != null)
        {
            //Toggle the visibility state
            isTextVisible = !isTextVisible;

            //Enable or disable the TextMeshPro text game object
            tmpText.gameObject.SetActive(isTextVisible);
        }
        else
        {
            Debug.LogWarning("TMP Text reference is not assigned.");
        }
    }
}