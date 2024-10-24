using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour, IInteractable
{
    [SerializeField][Tooltip("Opening time in seconds")] private float openingTime = 0.5f;
    [SerializeField][Tooltip("Opening angle in degrees")] private float openingAngle = 90f;
    private float openingSign = 1f;
    private float openingSpeed = 180f;
    private float defaultAngle = 0;
    private float angle = 0;

    private bool locked = false;
    public bool Locked
    {
        set
        {
            locked = value;

            if (!value) return;

            StopAllCoroutines();
            transform.eulerAngles = new(0f, defaultAngle, 0f);
        }
    }

    private void Awake()
    {
        if (openingAngle < 0f) openingSign = -1f;
        openingAngle = Mathf.Abs(openingAngle);
        openingSpeed = openingAngle / openingTime; //Also make sure it's set properly on awake
        defaultAngle = transform.eulerAngles.y;
    }

    private void OnValidate() => openingSpeed = openingAngle / openingTime; //Make sure this gets updated whenever you change the values in the inspector

    public void Interact()
    {
        if (locked) return; //Cancel all further interaction if the door is locked

        StopAllCoroutines(); //Cancel all coroutines to make sure you don't get a lock halfway through where both coroutines are forever pulling the door open and closed at the same time

        if (angle < openingAngle * 0.5f) StartCoroutine(Open()); //If the door is at least mostly closed, attempt to open it
        if (angle >= openingAngle * 0.5f) StartCoroutine(Close()); //If the door is at least mostly open, attempt to close it
    }

    IEnumerator Open()
    {
        while (angle < openingAngle) //While the angle is under the maximum, move towards the maximum and update the transform
        {
            angle += Time.deltaTime * openingSpeed * Mathf.Sign(openingAngle);
            if (angle > openingAngle) angle = openingAngle; //Guard to make sure it doesn't go past the bounds

            transform.eulerAngles = new(0f, defaultAngle + angle * openingSign, 0f);

            yield return null;
        }
    }

    IEnumerator Close()
    {
        while (angle > 0f) //Exact same thing but with closing
        {
            angle -= Time.deltaTime * openingSpeed * Mathf.Sign(openingAngle);
            if (angle < 0f) angle = 0f;

            transform.eulerAngles = new(0f, defaultAngle + angle * openingSign, 0f);

            yield return null;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 3)
        {
            CutsceneManager.PlayCutscene(GameEvent.Ending3);
        }
    }
}
