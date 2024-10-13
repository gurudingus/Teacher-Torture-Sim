using UnityEngine;

public class LockAndKillMenu : MonoBehaviour
{
    private void OnTriggerEnter(Collider doorClose)
    {
        GameObject.Find("Menu Room")?.SetActive(false);
        (GameObject.Find("Door")?.GetComponent<Door>()).Locked = true;
        EndingItems.Splash();

        gameObject.SetActive(false); //Disable itself once it is triggered
    }
}
