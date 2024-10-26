using UnityEngine;
using System.Collections;

public class JustinAIScript : MonoBehaviour, IInteractable
{
    //setup variables
    public Transform target;
    Camera cam;
    public bool sight;
    private int touchCounter;

    public float cooldownTime = 0.5f;
    private bool isOnCooldown = false;

    public static bool scrollIsHeld = false;

    private AudioSource audioSource;
    public GameObject lectureRoom;
    public GameObject killItemPlane;
    public GameObject torturePoints;

    void Start()
    {
        //sets up camera and audio sources
        cam = GetComponentInChildren<Camera>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        //updates the detection field for seeing the player
        Vector3 screenPos = cam.WorldToViewportPoint(target.position);
        sight = (screenPos.x >= 0 && screenPos.x <= 1 && screenPos.z >= 0 && screenPos.z <= 1);

    }

    private void OnCollisionEnter(Collision collision)
    {
        //checks to see if the player is within the detection zone, sets cooldown if they are
        if (collision.gameObject.layer == 3 && !isOnCooldown)
        {
            if (touchCounter >= 2) //Fixed so that things actually happen on the third strike
            {
                CutsceneManager.PlayCutscene(GameEvent.Ending2);
            }
            else
            {
                touchCounter++;
                #if UNITY_EDITOR
                Debug.Log("Justin was touched");
                #endif
            }

            isOnCooldown = true;
            Invoke(nameof(ResetCooldown), cooldownTime);
        }
    }

    private void ResetCooldown()
    {
        //resets the cooldown on detectinfg the player so it doesn't happen ad infenitum
        isOnCooldown = false;
    }

    public void Interact()
    {
        //enables interaction with justin with the E key
        audioSource.Play();
        if (scrollIsHeld) {
            CutsceneManager.PlayCutscene(GameEvent.Ending6);
            lectureRoom.SetActive(false);
            killItemPlane.SetActive(false);
            torturePoints.SetActive(false);
        }
    }
}