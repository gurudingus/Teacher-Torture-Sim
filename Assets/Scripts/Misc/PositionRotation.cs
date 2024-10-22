using System;
using UnityEngine;
using Unity.Mathematics;

/// <summary>
/// Kind of like a Transform but it only stores position and rotation, as well as only doing what I need it to. Currently it is tailored towards the object pickup related scripts.
/// </summary>
[Serializable] public class PositionRotation
{
    //Fields, accessors and ''''accessors''''
    [SerializeField] private Vector3 position = Vector3.zero;
    public Vector3 Position => position; //Gets the local position
    public Vector3 GetPosition(Transform transform) => transform.position + (Vector3)(transform.localToWorldMatrix * position); //Gets the world space position

    [SerializeField] private Quaternion rotation = Quaternion.identity;
    public Quaternion Rotation => rotation; //Gets the local rotation
    public Quaternion GetRotation(Transform transform) => transform.rotation * rotation; //Gets the global rotation

    //Constructors
    public PositionRotation() //Blank constructor with 0,0,0 position and identity rotation
    {
        position = Vector3.zero;
        rotation = Quaternion.identity;
    }

    public PositionRotation(Vector3 _position) => position = _position; //Constructor with identity rotation and position
    public PositionRotation(float x, float y, float z) => position = new(x, y, z); //Constructor with identity rotation and position passed in as individual x,y and z

    public PositionRotation(Quaternion _rotation) => rotation = _rotation; //Constructor with 0,0,0 position and the rotation

    public PositionRotation(Vector3 _position, Quaternion _rotation) //Constructor with both position and rotation
    {
        position = _position;
        rotation = _rotation;
    }

    //Operators
    /// <summary>
    /// Averages two PositionRotations. I chose the | operator because it sits neatly in the middle of the two values both in text and in function
    /// </summary>
    public static PositionRotation operator |(PositionRotation a, PositionRotation b) => new((a.position + b.position) * 0.5f, math.nlerp(a.rotation, b.rotation, 0.5f));

    /// <summary>
    /// Returns the PositionRotation with the Vector3 subtracted from the position
    /// </summary>
    public static PositionRotation operator -(PositionRotation a, Vector3 b) => new(a.position - a.rotation * b, a.rotation);

    /// <summary>
    /// Multiples the rotation of the PositionRotation by the Quaternion
    /// </summary>
    public static PositionRotation operator *(Quaternion a, PositionRotation b) => new(b.position, a * b.rotation);
}