using System;
using UnityEngine;
using Unity.Mathematics;

/// <summary>
/// Kind of like a Transform but it only stores position and rotation, as well as only doing what I need it to. Currently it is tailored towards the object pickup related scripts.
/// </summary>
[Serializable] public class PositionRotation {
    //Fields, accessors and ''''accessors''''
    [SerializeField] private Vector3 position = Vector3.zero;
    public Vector3 Position => position;
    public Vector3 GetPosition(Transform transform) => transform.position + (Vector3)(transform.localToWorldMatrix * position);

    [SerializeField] private Quaternion rotation = Quaternion.identity;
    public Quaternion Rotation => rotation;
    public Quaternion GetRotation(Transform transform) => transform.rotation * rotation;

    //Constructors
    public PositionRotation()
    {
        position = Vector3.zero;
        rotation = Quaternion.identity;
    }

    public PositionRotation(Vector3 _position) => position = _position;
    public PositionRotation(float x, float y, float z) => position = new(x, y, z);

    public PositionRotation(Quaternion _rotation) => rotation = _rotation;

    public PositionRotation(Vector3 _position, Quaternion _rotation)
    {
        position = _position;
        rotation = _rotation;
    }

    //Operators
    /// <summary>
    /// Averages two PositionRotations. I chose the | operator because it sits neatly in the middle of the two values both in text and in function
    /// </summary>
    public static PositionRotation operator |(PositionRotation a, PositionRotation b) => new((a.position + b.position) * 0.5f, math.nlerp(a.rotation, b.rotation, 0.5f));
}