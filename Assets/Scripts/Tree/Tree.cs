using UnityEngine;
using DG.Tweening;

public class Tree : MonoBehaviour
{
    [SerializeField] float destination;
    [SerializeField] float duration;
    [SerializeField] float waitDestroyTime;
    [SerializeField] bool onBranch;

    BoxCollider2D collider;

    Rigidbody2D rigid;


    // Property
    public Branch branch
    {
        get { return _branch; }
        set { _branch = value; }
    }

    // Value
    Branch _branch;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        collider = GetComponent<BoxCollider2D>();
        _branch = null;
    }

    [ContextMenu("Shoot")]
    public void Shoot()
    {
        collider.enabled = false;
        rigid.gravityScale = 0f;

        transform.DOMoveX(destination, duration).SetEase(Ease.OutQuad);

        TreeManager.instance.treeGroup.Dequeue();

        Destroy(this.gameObject, waitDestroyTime);
    }
}
