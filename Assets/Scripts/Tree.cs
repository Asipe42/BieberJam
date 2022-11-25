using UnityEngine;
using DG.Tweening;

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
        transform.DOMoveX(5, 1.5f).SetEase(Ease.OutQuad);
    }
}
