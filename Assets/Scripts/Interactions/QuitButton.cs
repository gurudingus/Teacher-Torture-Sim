using UnityEngine;

public class QuitButton : MonoBehaviour
{
    private void OnMouseDown()
    {
        //Call this method when the object is clicked
        QuitGame();
    }
    private void QuitGame()
    {
        //quits the game
        Application.Quit();

        //if your running the game in the Unity editor will give message. this line will do nothing in a build
        #if UNITY_EDITOR
        Debug.Log("Game is exiting");
        #endif
    }
}
