using UnityEngine;
using System.Collections;

public class JustinSightCone : MonoBehaviour
{
    public Transform target;
    Camera cam;
    public bool sight;

    void Start()
    {
        cam = GetComponentInChildren<Camera>();
    }

    void Update()
    {

        Vector3 screenPos = cam.WorldToViewportPoint(target.position);
        sight = (screenPos.x >= 0 && screenPos.x <= 1 && screenPos.z >= 0 && screenPos.z <= 1) ;

        Debug.Log(sight);
    }
}