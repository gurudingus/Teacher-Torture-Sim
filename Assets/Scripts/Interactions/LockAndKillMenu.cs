using UnityEngine;
//Rama's and Liam's code

public class LockAndKillMenu : MonoBehaviour
{
    private void OnTriggerEnter(Collider doorClose)
    {
        if (doorClose.transform.gameObject.layer != 6) return; //Don't do anything if it is not a player colliding with it
        gameObject.SetActive(false); //Disable itself once it is triggered
        GameManager.gameState = GameState.Playing;

        GameObject.Find("Menu Room")?.SetActive(false); //Makes the Menu Room go POOF
        (GameObject.Find("Door")?.GetComponent<Door>()).Locked = true; //Closes the door behind the player
        EndingItems.Splash(); //Notification of new item in the room

        //Reset the Torture Points to 0
        if (ScoreManager.Instance != null)
        {
            ScoreManager.Instance.ResetScore();
        }
    }
}
