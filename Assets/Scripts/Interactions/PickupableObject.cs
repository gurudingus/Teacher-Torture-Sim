using System.Collections.Generic;
using UnityEngine;

public class PickupableObject : MonoBehaviour
{
    private static List<PickupableObject> interactableObjects = new();

    [SerializeField] [Tooltip("Weight in kg")] private float mass = 1;

    private void Awake()
    {
        interactableObjects.Add(this);
    }
}