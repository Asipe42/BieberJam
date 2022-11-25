using System.Collections;
using UnityEngine;
using DG.Tweening;

public class InputReader : MonoBehaviour
{
    // Inspector
    [SerializeField] private TreeSpawner _treeSpawner;

    Animator anim;

    Vector3 originCameraPosition;

    private void Awake()
    {
        anim = GetComponent<Animator>();

        originCameraPosition = Camera.main.transform.position;
    }

    void Update()
    {
        InputKey();       
    }

    void InputKey()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            Destroy(TreeManager.instance.treeGroup.ToArray()[1].branch.gameObject);
        }

        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            var shootTree = TreeManager.instance.treeGroup.Peek();
            shootTree.Shoot();
            _treeSpawner.SpawnTree();
            AudioManager.instance.PlaySFX(SFXDefiniton.SFX_ATTACK);
            anim.SetTrigger("Attack");
            // Camera.main.DOShakePosition(0.2f);

            // branch
            Tree bottomTree;
            TreeManager.instance.treeGroup.TryPeek(out bottomTree);

            if (bottomTree.branch)
            {
                HP.instance.DamageHP(bottomTree.branch.damage);
            }
            else
            {
                HP.instance.HealHP(shootTree.healHP);
            }

            var sequence = DOTween.Sequence();
            sequence.Append(Camera.main.DOOrthoSize(4.8f, 0.1f))
                    .Append(Camera.main.DOOrthoSize(5f, 0.2f));
                    
        }
    }
}
