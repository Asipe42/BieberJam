using UnityEngine;
using DG.Tweening;

public class InputReader : MonoBehaviour
{
    // Inspector
    [SerializeField] private TreeSpawner _treeSpawner;

    Animator anim;

    [SerializeField] float cooltime;

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

        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            anim.SetTrigger("Up");
            Fever.instance.PlusFeverValue();
            AudioManager.instance.PlaySFX(SFXDefiniton.SFX_RIGHT);
            HP.instance.RecoverHP();

            var sequence = DOTween.Sequence();
            sequence.Append(Camera.main.DOOrthoSize(4.8f, 0.1f))
                    .Append(Camera.main.DOOrthoSize(5f, 0.2f));

            Destroy(TreeManager.instance.treeGroup.ToArray()[1].branch.gameObject);
        }

        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
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
}
