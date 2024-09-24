using UnityEngine;

[RequireComponent(typeof(Animator))] public class Door : MonoBehaviour, IInteractable {
    [SerializeField] [Tooltip("Opening time in seconds")] private float openingTime = 0.5f;
    private bool isOpen = false;

    private Animator animator;

    private bool locked = false;
    public bool Locked
    {
        set
        {
            locked = value;
            //if (locked) angle = 0;
        }
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
        animator.speed = 1f / openingTime; //Set the opening speed
    }

    private void OnValidate() => GetComponent<Animator>().speed = 1f / openingTime;

    public void Interact(InteractionScript source)
    {
        if (locked) return; //Cancel all further interaction if the door is locked

        isOpen = !isOpen; //Toggle whether or not the door is open
        animator.Play(isOpen ? "Door Close" : "Door Open", 0, 0f); //Play the animation
    }
}
