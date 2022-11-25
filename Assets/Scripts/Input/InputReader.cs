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

        }

        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            TreeManager.instance.treeGroup.Peek().Shoot();
            _treeSpawner.SpawnTree();
            AudioManager.instance.PlaySFX(SFXDefiniton.SFX_ATTACK);
            HP.instance.RecoverHP();
            anim.SetTrigger("Attack");
            // Camera.main.DOShakePosition(0.2f);

            var sequence = DOTween.Sequence();
            sequence.Append(Camera.main.DOOrthoSize(4.8f, 0.1f))
                    .Append(Camera.main.DOOrthoSize(5f, 0.2f));
                    
        }
    }
}
