using System.Collections;
using UnityEngine;
using DG.Tweening;

public class InputReader : MonoBehaviour
{
    // Inspector
    [SerializeField] private TreeSpawner _treeSpawner;

    Animator anim;

    [SerializeField] float stunCooltime;

    bool onStun;

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
        if (!GameManager.onStart)
            return;

        if (onStun)
            return;

        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            var targetBranch = TreeManager.instance.treeGroup.ToArray()[1].branch;
            if (targetBranch)
            {
                anim.SetTrigger("Up");
                Fever.instance.PlusFeverValue();
                AudioManager.instance.PlaySFX(SFXDefiniton.SFX_RIGHT);
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
                anim.SetTrigger("Stun");
                onStun = true;
                StartCoroutine(WaitStun());
            }
        }

        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            // MISS
            var targetBranch = TreeManager.instance.treeGroup.ToArray()[1].branch;
            if (targetBranch)
            {
                var branchChild = targetBranch.transform.GetChild(0);

                targetBranch.GetComponent<Rigidbody2D>().simulated = true;
                branchChild.GetComponent<BoxCollider2D>().enabled = true;
                branchChild.GetComponent<Animator>().SetBool("Crack", true);
                branchChild.GetComponent<SpriteRenderer>().DOFade(0, 1f).SetDelay(0.5f).OnComplete(() => Destroy(targetBranch.gameObject));
                targetBranch.transform.SetParent(null);
            }

            TreeManager.instance.killCount++;
            var shootTree = TreeManager.instance.treeGroup.Peek();
            shootTree.Shoot();
            _treeSpawner.SpawnTree();
            AudioManager.instance.PlaySFX(SFXDefiniton.SFX_RIGHT);
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

    IEnumerator WaitStun()
    {
        yield return new WaitForSeconds(stunCooltime);

        onStun = false;
    }
}
