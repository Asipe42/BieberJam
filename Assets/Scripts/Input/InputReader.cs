using System.Collections;
using UnityEngine;
using DG.Tweening;

public class InputReader : MonoBehaviour
{
    // Inspector
    [SerializeField] private TreeSpawner _treeSpawner;

    Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        InputKey();       
    }

    void InputKey()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            var targetBranch = TreeManager.instance.treeGroup.ToArray()[1].branch;
            if (targetBranch)
            {
                anim.SetTrigger("Up");
                Fever.instance.PlusFeverValue();
                AudioManager.instance.PlaySFX(SFXDefiniton.SFX_ATTACK);
                HP.instance.RecoverHP();

                var sequence = DOTween.Sequence();
                sequence.Append(Camera.main.DOOrthoSize(4.8f, 0.1f))
                        .Append(Camera.main.DOOrthoSize(5f, 0.2f));


                TreeManager.instance.treeGroup.ToArray()[1].branch = null;
                targetBranch.transform.SetParent(_treeSpawner.transform);
                targetBranch.Shoot();
                Destroy(targetBranch.gameObject, 5);
            }
            else
            {

            }
        }

        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            var shootTree = TreeManager.instance.treeGroup.Peek();
            shootTree.Shoot();
            _treeSpawner.SpawnTree();
            AudioManager.instance.PlaySFX(SFXDefiniton.SFX_ATTACK);
            anim.SetTrigger("Right");
            Fever.instance.PlusFeverValue();

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
