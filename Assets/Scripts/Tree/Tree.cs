using UnityEngine;
using DG.Tweening;

public class Tree : MonoBehaviour
{
    [SerializeField] float destination;
    [SerializeField] float duration;
    [SerializeField] float waitDestroyTime;

    Rigidbody2D rigid;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    [ContextMenu("Shoot")]
    public void Shoot()
    {
        transform.DOMoveX(destination, duration).SetEase(Ease.OutQuad);

        TreeManager.instance.treeGroup.Dequeue();

        Destroy(this.gameObject, waitDestroyTime);
    }
}
