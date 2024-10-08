using UnityEngine;

public class QuitButton : MonoBehaviour
{
    private void OnMouseDown()
    {
        //call this method when the object is clicked
        QuitGame();
    }
    private void QuitGame()
    {
        //quits the game
        Application.Quit();

        //if your in the Unity editor will give message this line does nothing in build
        #if UNITY_EDITOR
        Debug.Log("Game is exiting");
        #endif
    }
}
