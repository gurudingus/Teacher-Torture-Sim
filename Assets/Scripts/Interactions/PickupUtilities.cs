using UnityEngine;
//Liam Script
public class PickupUtilities
{
    /// <summary>
    /// Draws the rotation rays around a position ball of specified colour
    /// </summary>
    public static void DrawGizmos(Vector3 centre, Quaternion rotation, Color sphereColour) //Just some gizmos to help visualise the positions
    {
        Gizmos.color = sphereColour;
        Gizmos.DrawSphere(centre, 0.2f);

        Gizmos.color = Color.green;
        Gizmos.DrawRay(centre, rotation * Vector3.up);
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(centre, rotation * Vector3.forward);
        Gizmos.color = Color.red;
        Gizmos.DrawRay(centre, rotation * Vector3.right);
    }

    /// <summary>
    /// Draws the rotation rays around a yellow position ball
    /// </summary>
    public static void DrawGizmos(Vector3 centre, Quaternion rotation) //Just some gizmos to help visualise the positions
    {
        Gizmos.color = Color.gray;
        Gizmos.DrawSphere(centre, 0.2f);

        Gizmos.color = Color.green;
        Gizmos.DrawRay(centre, rotation * Vector3.up);
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(centre, rotation * Vector3.forward);
        Gizmos.color = Color.red;
        Gizmos.DrawRay(centre, rotation * Vector3.right);
    }
}