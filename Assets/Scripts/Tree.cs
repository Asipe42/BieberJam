using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    [SerializeField] float shootForce;
    Rigidbody2D rigid;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    [ContextMenu("Shoot")]
    public void Shoot()
    {
        rigid.AddForce(Vector2.right * shootForce, ForceMode2D.Impulse);
    }
}
