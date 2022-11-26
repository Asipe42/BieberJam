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
        
        if (FeverManager.instance.isFever)
        {
            FeverState();
        }
        else
        {
            NormalState();
        }
    }

    void NormalState()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            var targetBranch = TreeManager.instance.treeGroup.ToArray()[1].branch;
            if (targetBranch)
            {
                anim.SetTrigger("Up");
                FeverManager.instance.PlusFeverValue();
                AudioManager.instance.PlaySFX(SFXDefiniton.SFX_RIGHT);
                HP.instance.RecoverByTree();

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
            TreeManager.instance.killCount++;
            var shootTree = TreeManager.instance.treeGroup.Peek();
            shootTree.Shoot();
            _treeSpawner.SpawnTree();
            AudioManager.instance.PlaySFX(SFXDefiniton.SFX_RIGHT);
            anim.SetTrigger("Right");
            FeverManager.instance.PlusFeverValue();

            // branch
            Tree bottomTree;
            TreeManager.instance.treeGroup.TryPeek(out bottomTree);

            if (bottomTree.branch)
            {
                HP.instance.DamageByBranch();
            }
            else
            {
                HP.instance.RecoverByTree();
            }

            var sequence = DOTween.Sequence();
            sequence.Append(Camera.main.DOOrthoSize(4.8f, 0.1f))
                    .Append(Camera.main.DOOrthoSize(5f, 0.2f));

        }
    }

    void FeverState()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W) ||
            Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            HP.instance.RecoverByTree();

            var targetBranch = TreeManager.instance.treeGroup.ToArray()[1].branch;
            if (targetBranch)
            {
                TreeManager.instance.treeGroup.ToArray()[1].branch = null;
                targetBranch.transform.SetParent(_treeSpawner.transform);
                targetBranch.Shoot();
                Destroy(targetBranch.gameObject, 5);
            }

            TreeManager.instance.killCount++;
            var shootTree = TreeManager.instance.treeGroup.Peek();
            shootTree.Shoot();
            _treeSpawner.SpawnTree();
            AudioManager.instance.PlaySFX(SFXDefiniton.SFX_RIGHT);
            anim.SetTrigger("Right");


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
