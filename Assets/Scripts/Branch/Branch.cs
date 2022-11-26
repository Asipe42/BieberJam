using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Branch : MonoBehaviour
{
    // Inspector
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private Vector2 _force;
    [SerializeField] private Vector2 _randomExtraForce;
    [SerializeField] private Vector2 _forcePosition;

    // Property

    public void Shoot()
    {
        _rigidbody2D.simulated = true;
        var force = new Vector2(_force.x + Random.Range(0, _randomExtraForce.x), _force.y + Random.Range(0, _randomExtraForce.y));
        _rigidbody2D.AddForceAtPosition(force, _forcePosition);
    }
}
