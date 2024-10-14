using UnityEngine;

public class LockAndKillMenu : MonoBehaviour
{
    private void OnTriggerEnter(Collider doorClose)
    {
        if (doorClose.transform.gameObject.layer != 6) return; //Don't do anything if it is not a player colliding with it
        gameObject.SetActive(false); //Disable itself once it is triggered
        GameManager.gameState = GameState.Playing;

        GameObject.Find("Menu Room")?.SetActive(false);
        (GameObject.Find("Door")?.GetComponent<Door>()).Locked = true;
        EndingItems.Splash();
    }
}
