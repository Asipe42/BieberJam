using System.Collections;
using UnityEngine;
using DG.Tweening;

public class InputReader : MonoBehaviour
{
    // Inspector
    [SerializeField] private TreeSpawner _treeSpawner;
    [SerializeField] GameObject question;

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
                Combo.instance.SyncCombo();
                anim.SetTrigger("Up");
                FeverManager.instance.PlusFeverValue();
                AudioManager.instance.PlaySFX(SFXDefiniton.SFX_UP);
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
                Combo.instance.SyncCombo(true);

                anim.SetTrigger("Stun");
                onStun = true;
                StartCoroutine(WaitStun());

                var sequence = DOTween.Sequence();

                sequence.Append(question.transform.DOScale(1f, 0.5f)).SetEase(Ease.OutBack)
                        .Insert(1.5f, question.transform.DOScale(0f, 0.3f).SetEase(Ease.OutQuad));
            }
        }

        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            // MISS
            var targetBranch = TreeManager.instance.treeGroup.ToArray()[1].branch;
            if (targetBranch)
            {
                Combo.instance.SyncCombo(true);

                var childLeft = targetBranch.transform.GetChild(0);
                var childRight = targetBranch.transform.GetChild(1);

                targetBranch.GetComponent<Rigidbody2D>().simulated = true;
                childLeft.GetComponent<PolygonCollider2D>().enabled = true;
                childRight.GetComponent<PolygonCollider2D>().enabled = true;

                childLeft.transform.DORotate(new Vector3(0, 0, 20), 0.3f).SetEase(Ease.OutBounce);
                childRight.transform.DORotate(new Vector3(0, 0, -20), 0.3f).SetEase(Ease.OutBounce);

                var seq = DOTween.Sequence();

                seq.Append(transform.DOScale(new Vector3(0.9f, 0.45f, 0.9f), 0.3f).SetEase(Ease.OutBack))
                   .Append(transform.DOScale(new Vector3(0.9f, 0.9f, 0.9f), 0.2f).SetEase(Ease.OutBack));

                onStun = true;
                StartCoroutine(WaitStun(0.3f));

                /*
                childLeft.GetComponent<Rigidbody2D>().simulated = true;
                childRight.GetComponent<Rigidbody2D>().simulated = true;
                childLeft.GetComponent<Rigidbody2D>().AddTorque(5f, ForceMode2D.Impulse);
                childRight.GetComponent<Rigidbody2D>().AddTorque(5f, ForceMode2D.Impulse);
                */
                childLeft.GetComponent<SpriteRenderer>().DOFade(0, 1f).SetDelay(0.5f);
                childRight.GetComponent<SpriteRenderer>().DOFade(0, 1f).SetDelay(0.5f).OnComplete(() => Destroy(targetBranch.gameObject));
                targetBranch.transform.SetParent(null);
            }

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
                Combo.instance.SyncCombo(false);
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
            Combo.instance.SyncCombo(false);

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
            anim.SetTrigger("Right");


            var sequence = DOTween.Sequence();
            sequence.Append(Camera.main.DOOrthoSize(4.8f, 0.1f))
                    .Append(Camera.main.DOOrthoSize(5f, 0.2f));
        }
    }

    IEnumerator WaitStun(float cooltime = 0)
    {
        if (cooltime == 0)
        {
            yield return new WaitForSeconds(stunCooltime);
        }
        else
        {
            yield return new WaitForSeconds(0.3f);
        }

        onStun = false;
    }
}
