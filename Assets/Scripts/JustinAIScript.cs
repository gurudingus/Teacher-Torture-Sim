using UnityEngine;
using System.Collections;

public class JustinAIScript : MonoBehaviour
{
    public Transform target;
    Camera cam;
    public bool sight;
    private int touchCounter;

    void Start()
    {
        cam = GetComponentInChildren<Camera>();
    }

    void Update()
    {

        Vector3 screenPos = cam.WorldToViewportPoint(target.position);
        sight = (screenPos.x >= 0 && screenPos.x <= 1 && screenPos.z >= 0 && screenPos.z <= 1);

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 3)
        {
            if (touchCounter >= 2) //Fixed so that things actually happen on the third strike
            {
                CutsceneManager.PlayCutscene(GameEvent.Ending2);
            }
            else
            {
                touchCounter++;
                #if UNITY_EDITOR
                Debug.Log("Just was touched");
                #endif
            }
        }
    }
}