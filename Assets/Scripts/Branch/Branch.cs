using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Branch : MonoBehaviour
{
    // Inspector
    [SerializeField] private int _damageHP;

    // Property
    public int damage
    {
        get { return _damageHP; }
    }
}
