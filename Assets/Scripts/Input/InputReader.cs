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
            anim.SetTrigger("Up");
            Fever.instance.PlusFeverValue();
            AudioManager.instance.PlaySFX(SFXDefiniton.SFX_ATTACK);
            HP.instance.RecoverHP();

            var sequence = DOTween.Sequence();
            sequence.Append(Camera.main.DOOrthoSize(4.8f, 0.1f))
                    .Append(Camera.main.DOOrthoSize(5f, 0.2f));

        }

        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            TreeManager.instance.treeGroup.Peek().Shoot();
            _treeSpawner.SpawnTree();
            AudioManager.instance.PlaySFX(SFXDefiniton.SFX_ATTACK);
            HP.instance.RecoverHP();
            anim.SetTrigger("Right");
            Fever.instance.PlusFeverValue();

            var sequence = DOTween.Sequence();
            sequence.Append(Camera.main.DOOrthoSize(4.8f, 0.1f))
                    .Append(Camera.main.DOOrthoSize(5f, 0.2f));
                    
        }
    }
}
